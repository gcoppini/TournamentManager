using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems.TournamentManager.Domain
{
    public class FightStageTourment : IStageTourment
    {
        public string Name { get; set; }

        public List<IStageCriteria> StageCriteria { get; set; }

        public List<IMatchCriteria> MatchCriteria { get; set; }


        public bool isRanking { get; set; }

        public int rankingWinFactor { get; set; }
        public int rankingLoseFactor { get; set; }

        
        

    }
}
