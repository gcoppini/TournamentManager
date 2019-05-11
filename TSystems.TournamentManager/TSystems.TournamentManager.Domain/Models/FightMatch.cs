using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems.TournamentManager.Domain
{
    public class FightMatch : IFightMatch
    {
        public ICompetitor Winner { get; set; }

        public ICompetitor Loser { get; set; }

        public DateTime Date { get; set; }
        
        //Validar 2 no max
        public List<ICompetitor> Competitors { get; set; }

        public FightMatch()
        {
            Date = DateTime.Now;
        }
    }
}
