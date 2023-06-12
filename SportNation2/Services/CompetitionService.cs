using Microsoft.EntityFrameworkCore;
using SportNation2.Data;
using SportNation2.Data.Models;
using SportNation2.Infrastructure;
using SportNation2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("SportNation2.Tests")]
namespace SportNation2.Services
{
    public class CompetitionService : ICompetitionService
    {
        private readonly AppDbContext _dbContext;

        public CompetitionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CreateCompetitionAsync(int sportId, string name, DateTime startDate)
        {
            if (!CreateCompetitionDataIsValid(sportId, name, startDate))
            {
                throw new InvalidDataException("Données invalides");
            }

            var item = new Competition()
            {
                Date = startDate,
                Name = name,
                SportId = sportId
            };

            var entry = await _dbContext.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return entry.Entity.Id;
        }

        internal bool CreateCompetitionDataIsValid(int sportId, string name, DateTime startDate)
        {
            //valider id du sport
            if (sportId <= 0)
            {
                return false;
            }
            if (!_dbContext.Sports.Any(s => s.Id == sportId))
            {
                return false;
            }

            //valider name
            if (string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            //valioder startdate
            if (startDate < DateTime.Now.AddDays(1))
            {
                return false;
            }

            return true;

        }

        public Task CreateCompetitionEvent(int competitionId, CompetitionEventArguments args)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CompetitionListViewModel>> GetNextCompetitionsAsync()
        {
            var items = await _dbContext.Competitions
                .Include(c => c.Sport)
                .Where(c => c.Date.Date > DateTime.Now.Date)
                .ToListAsync();

            var result = items.Select(c => new CompetitionListViewModel()
            {
                Id = c.Id,
                Name = c.Name,
                Date = c.Date,
                SportName = c.Sport.SportName
            });

            return result;
        }

        /// <summary>
        /// Retourne les sports depuis la BDD sous forme de Tuple(int, string)
        /// </summary>
        /// <returns></returns>
        public async Task<List<(int Id, string Name)>> GetSportsAsync()
        {
            var sports = await _dbContext.Sports.ToListAsync();

            var result = sports
                .Select(s => (s.Id, s.SportName)).ToList();


            return result;
        }

        /// <summary>
        /// Retourne l'ID, le nom et les épreuves d'une compétition par son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CompetitionDto?> GetCompetitionsAsync(int id)
        {
            CompetitionDto? result = default;
            var competition = await _dbContext
                .Competitions
                .Include(c => c.CompetitionEvents)
                    .ThenInclude(e => e.Participations)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (competition != null)
                result = new CompetitionDto
                {
                    Id = competition.Id,
                    Name = competition.Name,
                    Events = competition.CompetitionEvents.Select(e => $"{e.Name}, {e.Genre}, (de {e.MinimumAge} à {e.MaximumAge} ans) : {e.Participations.Count} inscrits").ToList()
                };

            return result;
        }

        /// <summary>
        /// Transforme l'enumération CompetitionGenre en tableau de string
        /// </summary>
        /// <returns></returns>
        public string[] GetGenres()
        {
            return Enum.GetNames(typeof(Enumerations.CompetitionGenre));
        }

        /// <summary>
        /// Creation d'une épreuve en BDD
        /// </summary>
        /// <param name="competitionId"></param>
        /// <param name="name"></param>
        /// <param name="minAge"></param>
        /// <param name="maxAge"></param>
        /// <param name="maxParticipants"></param>
        /// <param name="genre"></param>
        /// <returns></returns>
        public async Task CreateEventAsync(int competitionId, string name, int minAge, int maxAge, int maxParticipants, string genre)
        {
            CompetitionEvent ce = new CompetitionEvent
            {
                CompetitionId = competitionId,
                Name = name,
                MinimumAge = minAge,
                MaximumAge = maxAge,
                MaxParticipants = maxParticipants,
                Genre = Enum.Parse<Enumerations.CompetitionGenre>(genre)
            };

            await _dbContext.CompetitionEvents.AddAsync(ce);
            await _dbContext.SaveChangesAsync();
        }
    }
}
