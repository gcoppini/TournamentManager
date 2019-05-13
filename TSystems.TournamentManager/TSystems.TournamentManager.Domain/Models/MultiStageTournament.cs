using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems.TournamentManager.Domain
{
    public class MultiStageTournament : ITournament
    {
        public enumTournamentType TournamentType { get; set; }

        public List<IGroupTournament> Groups { get; set; }

        public List<IStageTournament> Stages { get; set; }

        public List<ICompetitor> Competitors { get; set; }

        public List<IFightMatch> Matches { get; set; }
         
        public List<ICompetitor> Ranking { get; set; }

        public MultiStageTournament()
        {
            TournamentType = enumTournamentType.MultiStage;
            Matches = new List<IFightMatch>();
        }

    }
}
