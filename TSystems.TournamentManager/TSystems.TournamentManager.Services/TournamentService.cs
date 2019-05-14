using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TSystems.TournamentManager.Domain;
using System.Diagnostics;

namespace TSystems.TournamentManager.Services
{
    //Tournament orchestration
    public class TournamentService
    {
        private ITournament _tournament;
        public TournamentService(ITournament tournament)
        {
            _tournament = tournament;
        }

        public void ProcessMatches()
        {
            switch (_tournament.TournamentType)
            {
                case enumTournamentType.MultiStage:
                    Debug.Print("Multi-Stage Tournament identified\n");
                    ProcessMultiStage();
                break;

                default:
                    Debug.Print("Ooops wrong Tournament type");
                    throw new InvalidOperationException("Invalid tournament type");
            }
        }

        public List<FightCompetitor> GetResults()
        {
            var competitors = (((MultiStageTournament)_tournament).Competitors);
            var co  = competitors
                        .Cast<FightCompetitor>()
                        .OrderByDescending(x=>x.Ranking)
                        .ToList();
                        //.OrderByDescending(x=>x.Points);
                        
            foreach (var item in co)
            {
                Debug.Print(item.Ranking +" | "+ item.Points +" | " + item.ToString());
            }
            return co;
        }

        private void ProcessMultiStage()
        {
            var stages = ((MultiStageTournament)_tournament).Stages;
            var groups = ((MultiStageTournament)_tournament).Groups;
            var competitors = ((MultiStageTournament)_tournament).Competitors;
            var matchesHistory = ((MultiStageTournament)_tournament).Matches;

            foreach(IStageTournament stage in stages)
            {
                Debug.Print("*** Processing stage: "+stage.Name+" ****\n");

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
            //ShowResults();
        }

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
            var competitors = ((MultiStageTournament)_tournament).Competitors;
    
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

        private void ShowMatchs(List<IFightMatch> matches)
        {
            var co  = matches
                        .Cast<FightMatch>()
                        .OrderByDescending(x=>x.Date);
                        
            foreach (var item in co)
            {
                if(item.Winner != null && item.Loser!=null)
                    Debug.Print(((FightCompetitor)item.Winner).Group.Name +  " - Winner: "+ item.Winner.ToString() +" | " +((FightCompetitor)item.Loser).Group.Name + " Looser:" + item.Loser.ToString());
            }
        }
    }
}