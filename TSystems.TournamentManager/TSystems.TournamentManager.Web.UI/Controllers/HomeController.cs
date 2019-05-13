using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TSystems.TournamentManager.Web.UI.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using TSystems.TournamentManager.Domain;
using TSystems.TournamentManager.Services;


namespace TSystems.TournamentManager.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private FightTournamentService fightTournamentService = new FightTournamentService();

        public IActionResult Index()
        {
            HomeViewModel viewModel = new HomeViewModel();
            viewModel.Competitors = fightTournamentService.GetAllCompetitors();
            return View(viewModel);
        }

        public IActionResult Result(HomeViewModel model)
        {
            ResultViewModel viewModel = new ResultViewModel();

            if (model.SelectedCompetitors == null || model.SelectedCompetitors.Count != 20)
            {
                ModelState.AddModelError("SelectedCompetitors", "Por favor selecione 20 participantes.");
                return View(viewModel);
            }

            
            var result = fightTournamentService.Run(model.SelectedCompetitors); 
            viewModel.Competitors = result;
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
