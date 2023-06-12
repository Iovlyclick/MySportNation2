using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportNation2.Models;
using SportNation2.Services;

namespace SportNation2.Controllers
{
    public class CompetitionController : Controller
    {
        private readonly ICompetitionService competitionService;

        public CompetitionController(ICompetitionService competitionService)
        {
            this.competitionService = competitionService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<CompetitionListViewModel> result
                = await competitionService.GetNextCompetitionsAsync();

            return View(result);
        }


        //GET
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create()
        {
            var model = new CreateCompetitionViewModel()
            {
                Date = DateTime.Now,
                Sports = await competitionService.GetSportsAsync()
            };
            return View(model);
        }

        //POST
        [Authorize(Policy = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(string name, DateTime date, int sport)
        {
            try
            {
                //creation de la competition
                await competitionService.CreateCompetitionAsync(sport, name, date);

                //redirection vers la liste des competitions
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                //méthode erreur 400
                //return BadRequest(e.Message);

                //méthode validation du formulaire
                ModelState.AddModelError("", e.Message);
                var model = new CreateCompetitionViewModel()
                {
                    Name = name,
                    Date = date,
                    Sports = await competitionService.GetSportsAsync()
                };
                return View(model);
            }
        }

        //Get
        [HttpGet("{id:int}/CreateEvent")]
        public async Task<IActionResult> CreateEvent(int id)
        {
            CompetitionDto? c = await competitionService.GetCompetitionsAsync(id);
            if (c is null)
            {
                return NotFound();
            }
            var genres = competitionService.GetGenres();

            var model = new AddCompetitionEventViewModel
            {
                CompetitionId = c.Id,
                CompetitionEvents = c.Events,
                CompetitionName = c.Name,
                Genres = genres
            };

            return View(model);
        }



        //Post
        [HttpPost("{id:int}/CreateEvent")]
        public async Task<IActionResult> CreateEvent(AddCompetitionEventViewModel model, string genre)
        {
            await competitionService.CreateEventAsync(model.CompetitionId, model.Name, model.MinAge, model.MaxAge, model.MaxParticipants, genre);


            return RedirectToAction(nameof(CreateEvent));
        }

    }
}
