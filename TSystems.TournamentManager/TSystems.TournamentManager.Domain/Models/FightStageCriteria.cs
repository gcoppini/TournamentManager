using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TSystems.TournamentManager.Domain
{
    public abstract class FightStageCriteria
    {
        public int Order { get; set; }
        public abstract List<IFightMatch> Apply(List<ICompetitor> list);
        
    }

    public class AllversusAllCriteria : FightStageCriteria, IStageCriteria
    {

        private const int CONST_MATCH_COMPETITOR_QTY = 2;
        public override List<IFightMatch> Apply(List<ICompetitor> list)
        {
            var retorno = new List<IFightMatch>();

            var groups =  list
                            .Cast<FightCompetitor>()
                            .Select(x => x.Group)
                            .Distinct()
                            .ToList();

            foreach(IGroupTournament group in groups)
            {
                var competitorsGroup =  list
                                            .Cast<FightCompetitor>()
                                            .Where(x=> x.Group.Code == group.Code)
                                            .ToList();

                var matches = GetMatches(competitorsGroup.Cast<ICompetitor>().ToList());
                retorno.AddRange(matches);
            }
            return retorno;
        }
        private List<IFightMatch> GetMatches(List<ICompetitor> list)
        {
            var result = GetPermutations(list, CONST_MATCH_COMPETITOR_QTY);
            var retorno = new List<IFightMatch>();
            foreach (var perm in result)
            {
                FightMatch m = new FightMatch();
                m.Competitors = perm.ToList();
                retorno.Add(m);
            }
            return retorno;
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> items, int count)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (count == 1)
                    yield return new T[] { item };
                else
                {
                    foreach (var result in GetPermutations(items.Skip(i + 1), count - 1))
                        yield return new T[] { item }.Concat(result);
                }

                ++i;
            }
        }
    }

    public class FirstSecondExchange : FightStageCriteria, IStageCriteria
    {
        //(2A x 1B, 1C x 2D e 2C x 1D)
        public override List<IFightMatch> Apply(List<ICompetitor> competitors)
        {
            var result = new List<IFightMatch>();

            //hard-coded for demonstration purposes
            var groupA = competitors.Cast<FightCompetitor>().Where(x => x.Group.Code=="A").OrderByDescending(x => x.Points).ToList();
            var groupB = competitors.Cast<FightCompetitor>().Where(x => x.Group.Code=="B").OrderByDescending(x => x.Points).ToList();
            var groupC = competitors.Cast<FightCompetitor>().Where(x => x.Group.Code=="C").OrderByDescending(x => x.Points).ToList();
            var groupD = competitors.Cast<FightCompetitor>().Where(x => x.Group.Code=="D").OrderByDescending(x => x.Points).ToList();

            var cga1 = groupA.ElementAt(0);
            var cga2 = groupA.ElementAt(1);

            var cgb1 = groupB.ElementAt(0);
            var cgb2 = groupB.ElementAt(1);
            
            var cgc1 = groupC.ElementAt(0);
            var cgc2 = groupC.ElementAt(1);

            var cgd1 = groupD.ElementAt(0);
            var cgd2 = groupD.ElementAt(1);
            
            result.Add(new FightMatch(){Competitors = new List<ICompetitor>{cga1,cgb2}});
            result.Add(new FightMatch(){Competitors = new List<ICompetitor>{cga2,cgb1}});

            result.Add(new FightMatch(){Competitors = new List<ICompetitor>{cgc1,cgd2}});
            result.Add(new FightMatch(){Competitors = new List<ICompetitor>{cgc2,cgd1}});
  
            return result;
        }
    }

    public class FirstVerusFirst : FightStageCriteria, IStageCriteria
    {
        public override List<IFightMatch> Apply(List<ICompetitor> competitors)
        {
            var result = new List<IFightMatch>();
            var  co =  competitors.Cast<FightCompetitor>().OrderByDescending(x => x.Points).ToList();

            var cwa = co.Cast<FightCompetitor>().ElementAt(0);
            var cwb = co.Cast<FightCompetitor>().ElementAt(1);
            var cwc = co.Cast<FightCompetitor>().ElementAt(2);
            var cwd = co.Cast<FightCompetitor>().ElementAt(3);

            result.Add(new FightMatch(){Competitors = new List<ICompetitor>{cwa,cwb}});
            result.Add(new FightMatch(){Competitors = new List<ICompetitor>{cwc,cwd}});

            return result;
        } 

    }
}
