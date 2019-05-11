using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems.TournamentManager.Domain
{
    public class MatchEvaluation
    {
        private IMatchCriteria _matchCriteria;
 
        public void SetStageCriteria(IMatchCriteria matchCriteria)
        {
            this._matchCriteria = matchCriteria;
        }

        public List<IFightMatch> EvaluateMatches(List<IFightMatch> Matches)
        {
            return _matchCriteria.Apply(Matches);
        }
    }
}
