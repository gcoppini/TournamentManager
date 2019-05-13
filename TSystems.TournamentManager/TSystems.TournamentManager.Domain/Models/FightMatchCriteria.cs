using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TSystems.TournamentManager.Domain
{
    public abstract class FightMatchCriteria : IMatchCriteria 
    {
        public int Order { get; set; }
        public abstract List<IFightMatch> Apply(List<IFightMatch> list);
    }

    public class WinPercentageCriteria : FightMatchCriteria, IMatchCriteria
    {
        public override List<IFightMatch> Apply(List<IFightMatch> list)
        {
            foreach (var match in list)
            {
                var fightMatch = (FightMatch)match;
                var competitor = (FightCompetitor)fightMatch.Competitors.ElementAt(0);
                var otherCompetitor = (FightCompetitor)fightMatch.Competitors.ElementAt(1);

                var isDraw = competitor.WinPercentage == otherCompetitor.WinPercentage;
                
                if(!isDraw)
                {
                    if(competitor.WinPercentage > otherCompetitor.WinPercentage)
                    {
                        fightMatch.Winner = competitor;
                        fightMatch.Loser = otherCompetitor;
                    }
                    else
                    {
                        fightMatch.Winner = otherCompetitor;
                        fightMatch.Loser = competitor;
                    }
                }
                else
                {
                    //Console.WriteLine("% Win Draw!");
                    var isMartialartDraw = competitor.MartialArts.Count == otherCompetitor.MartialArts.Count;
                    if(!isMartialartDraw)
                    {
                        if(competitor.MartialArts.Count > otherCompetitor.MartialArts.Count)
                        {
                            fightMatch.Winner = competitor;
                            fightMatch.Loser = otherCompetitor;
                        }
                        else
                        {
                            fightMatch.Winner = otherCompetitor;
                            fightMatch.Loser = competitor;
                        }
                    }else
                    {
                        //Console.WriteLine("Martial Arts Draw!");
                        if(competitor.TotalFights > otherCompetitor.TotalFights)
                        {
                            fightMatch.Winner = competitor;
                            fightMatch.Loser = otherCompetitor;
                        }
                        else
                        {
                            fightMatch.Winner = otherCompetitor;
                            fightMatch.Loser = competitor;
                        }
                    }
                }

                //Console.WriteLine("Result - Winner:"+fightMatch.Winner.ToString() + " - Loser: "+fightMatch.Loser.ToString());
            }
        
            return list;
        }
    }
}