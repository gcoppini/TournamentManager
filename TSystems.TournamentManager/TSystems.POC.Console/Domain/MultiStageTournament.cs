using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems
{
    public class MultiStageTourment : ITournament
    {
        public enumTourmentType TourmentType { get; set; }

        public List<IGroupTournament> Groups { get; set; }

        public List<IStageTourment> Stages { get; set; }

        public List<ICompetitor> Competitors { get; set; }

        public List<IFightMatch> Matches { get; set; }
         
        public List<ICompetitor> Ranking { get; set; }

        public MultiStageTourment()
        {
            TourmentType = enumTourmentType.MultiStage;
            Matches = new List<IFightMatch>();
        }

    }
}
