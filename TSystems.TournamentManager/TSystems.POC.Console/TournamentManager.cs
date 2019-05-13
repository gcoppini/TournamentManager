using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TSystems
{
    public class TournamentManager
    {
        private ITournament Tournament;
        public TournamentManager(ITournament _Tournament)
        {
            Tournament = _Tournament;
        }
 
        //Tournament orchestration
        public void ProcessMatches()
        {
            switch (Tournament.TournamentType)
            {
                case enumTournamentType.MultiStage:
                    Console.Write("Multi-Stage Tournament identified\n");
                    ProcessMultiStage();
                break;

                default:
                    Console.Write("Ooops wrong Tournament type");
                break;
            }
        }

        private void ProcessMultiStage()
        {
            var stages = ((MultiStageTournament)Tournament).Stages;
            var groups = ((MultiStageTournament)Tournament).Groups;
            var competitors = ((MultiStageTournament)Tournament).Competitors;
            var matchesHistory = ((MultiStageTournament)Tournament).Matches;

            foreach(IStageTournament stage in stages)
            {
                Console.Write("*** Processing stage: "+stage.Name+" ****\n");

                var TournamentStage = (FightStageTournament)stage;
                var stageCriterias = ((FightStageTournament)stage).StageCriteria;
                var matchCriterias = ((FightStageTournament)stage).MatchCriteria;

                if(stageCriterias==null)
                    continue;

                foreach(IStageCriteria stageCriteria in stageCriterias)
                {
                    var stageMatches = GenerateMatches(competitors, stageCriteria);
                    EvaluateMatches(stageMatches, matchCriterias);
                    ComputePoints(TournamentStage, stageMatches);
                    ShowMatchs(stageMatches);

                    matchesHistory.AddRange(stageMatches);
                    
                }
            }
            ShowResults();
        }

        //Gero as partidas com critério da fase
        private List<IFightMatch> GenerateMatches(List<ICompetitor> competitors, IStageCriteria criteria)
        {
            var matchbuilder = new FightMatchBuilder();
            matchbuilder.SetStageCriteria(criteria);
            var matches = matchbuilder.BuildMatches(competitors);
            
            return matches;
        }

        private void EvaluateMatches(List<IFightMatch> matches,List<IMatchCriteria> criteria)
        {
            var matchEvaluate = new MatchEvaluation();
            matchEvaluate.SetStageCriteria(criteria.First());
            var matchResults = matchEvaluate.EvaluateMatches(matches);
        }

        private void ComputePoints(FightStageTournament stage, List<IFightMatch> matches)
        {
            var competitors = ((MultiStageTournament)Tournament).Competitors;
            


            int i = 1;
            foreach(FightMatch currentMatch in matches)
            {

                var winner = competitors.Cast<FightCompetitor>()
                                        .Where(x=>x.Name==currentMatch.Winner.Name)
                                        .First();


                var loser = competitors.Cast<FightCompetitor>()
                                        .Where(x=>x.Name==currentMatch.Loser.Name)
                                        .First();
                
                

                winner.AddWin();
                
                loser.AddLose();


                if(stage.isRanking) 
                { 
                   winner.Ranking = i++;
                   loser.Ranking = i++;
                }

            

            }

            

        }

        private void ShowResults()
        {
            var competitors = (((MultiStageTournament)Tournament).Competitors);
            var co  = competitors
                        .Cast<FightCompetitor>()
                        .OrderByDescending(x=>x.Ranking);
                        //.OrderByDescending(x=>x.Points);
                        
            foreach (var item in co)
            {
                Console.WriteLine(item.Ranking +" | "+ item.Points +" | " + item.ToString());
            }
        }
        
        private void ShowMatchs(List<IFightMatch> matches)
        {
            var co  = matches
                        .Cast<FightMatch>()
                        .OrderByDescending(x=>x.Date);
                        
            foreach (var item in co)
            {
                if(item.Winner != null && item.Loser!=null)
                    Console.WriteLine(((FightCompetitor)item.Winner).Group.Name +  " - Winner: "+ item.Winner.ToString() +" | " +((FightCompetitor)item.Loser).Group.Name + " Looser:" + item.Loser.ToString());
            }
        }
    }
}
