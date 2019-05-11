using System;
using System.Collections.Generic;
using TSystems.TournamentManager.Domain;

namespace TSystems.TournamentManager.Web.UI.Models
{
    public class HomeViewModel
    {
        public List<FightCompetitor> Competitors { get; set; }
        public List<string> SelectedCompetitors { get; set; }
    }
}