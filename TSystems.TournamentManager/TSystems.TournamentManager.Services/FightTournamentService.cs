using System;
using System.Linq;
using System.Collections.Generic;
using TSystems.TournamentManager.Domain;
using TSystems.TournamentManager.Data.Repository;

namespace TSystems.TournamentManager.Services
{
    public class FightTournamentService
    {
        CompetitorRepository competitorRepository = new CompetitorRepository();

        public List<FightCompetitor> GetAllCompetitors()
        {
            return competitorRepository.GetAll();
        }

        public List<FightCompetitor> Run(List<string> selected)
        {

            List<FightCompetitor> comp = GetAllCompetitors();
            
            var competitors = (from r in comp
                         where (from p in selected select p).Contains(r.Name)
                         select r).ToList();

            if(competitors.Count != 20)
                throw new InvalidOperationException("Necessário selecionar 20 participantes");

             //Grupos
            var groups = new List<IGroupTournament>();
            var grupoA = new GroupTournament(){Name="Grupo 1", Code="A"};
            var grupoB = new GroupTournament(){Name="Grupo 2", Code="B"};
            var grupoC = new GroupTournament(){Name="Grupo 3", Code="C"};
            var grupoD = new GroupTournament(){Name="Grupo 4", Code="D"};
            
            groups.Add(grupoA);
            groups.Add(grupoB);
            groups.Add(grupoC);
            groups.Add(grupoD);

            

            //Divisão em grupos conforme idade
            int i = 0;
            int j = 0;
            foreach(var c in competitors.OrderBy(x=> x.Age))
            {
                if(i>=20) {
                    competitors.Remove(c);
                    continue;
                }
                j = (int)i/5;
                ((FightCompetitor)c).Group = groups.ElementAt(j);
                i++;
            }


            //Critérios das fases - Como as partidas são organizadas
            var stageGroupCriterias = new List<IStageCriteria>(); 
            stageGroupCriterias.Add(new AllversusAllCriteria(){ Order=1 });

            var stage4thsCriterias = new List<IStageCriteria>(); 
            stage4thsCriterias.Add(new FirstSecondExchange(){ Order=1 });

            var stage2thsCriterias = new List<IStageCriteria>(); 
            stage2thsCriterias.Add(new FirstVerusFirst(){ Order=1 });


            //Criterios da disputa - Como as partidas são definidas
            var matchCriteria = new List<IMatchCriteria>();
            matchCriteria.Add(new WinPercentageCriteria(){ Order=1});

            //Fases do campeonato
            var stages = new List<IStageTournament>();
            stages.Add(new FightStageTournament(){Name="Inicio"});
            stages.Add(new FightStageTournament(){Name="Grupos", StageCriteria=stageGroupCriterias,MatchCriteria=matchCriteria });
            stages.Add(new FightStageTournament(){Name="Quartas de final",StageCriteria=stage4thsCriterias,MatchCriteria=matchCriteria });
            stages.Add(new FightStageTournament(){Name="Semifinal",StageCriteria=stage2thsCriterias,MatchCriteria=matchCriteria});
            stages.Add(new FightStageTournament(){Name="Final",StageCriteria=stage2thsCriterias,MatchCriteria=matchCriteria,isRanking=true, rankingWinFactor=3,rankingLoseFactor=0});

            //Torneio
            var tournament = new MultiStageTournament();
            tournament.Groups = groups;
            tournament.Stages = stages;
            tournament.Competitors = competitors.Cast<ICompetitor>().ToList();

            //Gerenciador Torneio
            var manager = new TournamentService(tournament);
            manager.ProcessMatches();
            var results = manager.GetResults();

            return results
                        .Where(x => x.Ranking.HasValue==true)
                        .OrderBy(y=>y.Ranking).ToList();
        }

    }
}
