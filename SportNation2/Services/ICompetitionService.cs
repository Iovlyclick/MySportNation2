using SportNation2.Data.Models;
using SportNation2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportNation2.Services
{
    public interface ICompetitionService
    {
        /// <summary>
        /// Création de compétition
        /// </summary>
        /// <param name="sport">le sport choisi</param>
        /// <param name="startDate">date de la compétition</param>
        /// <returns>ID de la nouvelle compétition</returns>
        Task<int> CreateCompetitionAsync(int sportId, string name, DateTime startDate);


        Task<IEnumerable<CompetitionListViewModel>> GetNextCompetitionsAsync();




        Task CreateCompetitionEvent(int competitionId, CompetitionEventArguments args);
        Task<List<(int Id, string Name)>> GetSportsAsync();
        Task<CompetitionDto?> GetCompetitionsAsync(int id);
        string[] GetGenres();
        Task CreateEventAsync(int competitionId, string name, int minAge, int maxAge, int maxParticipants, string genre);
    }
}
