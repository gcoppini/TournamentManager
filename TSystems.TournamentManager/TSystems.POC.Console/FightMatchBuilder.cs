using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems
{
    public class FightMatchBuilder
    {
        private IStageCriteria _stageCriteria;
 
        public void SetStageCriteria(IStageCriteria stageCriteria)
        {
            this._stageCriteria = stageCriteria;
        }

        public List<IFightMatch> BuildMatches(List<ICompetitor> Competitors)
        {
          return _stageCriteria.Apply(Competitors);
        }
 
    }
}
