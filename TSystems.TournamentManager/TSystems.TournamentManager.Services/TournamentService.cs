using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TSystems.TournamentManager.Domain
{
    public class TournamentService
    {
        private ITournament tourment;
        public TournamentService(ITournament _tourment)
        {
            tourment = _tourment;
        }
 
        //Tourment orchestration
        public void ProcessMatches()
        {
            switch (tourment.TourmentType)
            {
                case enumTourmentType.MultiStage:
                    Console.Write("Multi-Stage tourment identified\n");
                    ProcessMultiStage();
                break;

                default:
                    Console.Write("Ooops wrong tourment type");
                break;
            }
        }

        private void ProcessMultiStage()
        {
            var stages = ((MultiStageTourment)tourment).Stages;
            var groups = ((MultiStageTourment)tourment).Groups;
            var competitors = ((MultiStageTourment)tourment).Competitors;
            var matchesHistory = ((MultiStageTourment)tourment).Matches;

            foreach(IStageTourment stage in stages)
            {
                Console.Write("*** Processing stage: "+stage.Name+" ****\n");

                var tourmentStage = (FightStageTourment)stage;
                var stageCriterias = ((FightStageTourment)stage).StageCriteria;
                var matchCriterias = ((FightStageTourment)stage).MatchCriteria;

                if(stageCriterias==null)
                    continue;

                foreach(IStageCriteria stageCriteria in stageCriterias)
                {
                    var stageMatches = GenerateMatches(competitors, stageCriteria);
                    EvaluateMatches(stageMatches, matchCriterias);
                    ComputePoints(tourmentStage, stageMatches);
                    ShowMatchs(stageMatches);

                    matchesHistory.AddRange(stageMatches);
                    
                }
            }
            //ShowResults();
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

        private void ComputePoints(FightStageTourment stage, List<IFightMatch> matches)
        {
            var competitors = ((MultiStageTourment)tourment).Competitors;
            


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

        public List<FightCompetitor> GetResults()
        {
            var competitors = (((MultiStageTourment)tourment).Competitors);
            var co  = competitors
                        .Cast<FightCompetitor>()
                        .OrderByDescending(x=>x.Ranking)
                        .ToList();
                        //.OrderByDescending(x=>x.Points);
                        
            foreach (var item in co)
            {
                Console.WriteLine(item.Ranking +" | "+ item.Points +" | " + item.ToString());
            }
            return co;
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
