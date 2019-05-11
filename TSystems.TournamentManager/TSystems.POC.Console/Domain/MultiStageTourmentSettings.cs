using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems
{
    public class MultiStageTourmentSettings : ITournamentSettings
    {
        public enumTourmentType TourmentType { get; set; }

        public const int MIN_COMPETITOR_QTY = 20;
        public const int MIN_GROUP_QTY = 4;
        public const int MIN_GROUP_COMPETITOR_QTY = 5;



    }
}
