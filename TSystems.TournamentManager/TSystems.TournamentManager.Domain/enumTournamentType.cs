using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems.TournamentManager.Domain
{
        public enum enumTournamentType
        {
            MultiStage = 0,
            PromotionRelegation = 1,
            Bridge = 2
        }


        public enum enumMatchResult
        {
            WinnerLooser = 0,
            Draw = 1,
            
        }

        public enum enumMatchScope
        {
            Group = 0,
            All = 1
        }
}