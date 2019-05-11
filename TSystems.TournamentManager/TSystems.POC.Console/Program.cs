using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Linq;

namespace TSystems
{
    class Program
    {
         private static readonly HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            var repositories = ProcessRepositories().Result;
            /*
            foreach (var repo in repositories)
            {
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Idade);
                Console.WriteLine(repo.Lutas);
                Console.WriteLine(repo.Derrotas);
                Console.WriteLine(repo.Vitorias);
                Console.WriteLine(repo.ArtesMarciais);
                Console.WriteLine();
            }
             */

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


            //Competidores
            var competitors = new List<ICompetitor>();
            
            foreach (var repo in repositories)
            { /*
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Idade);
                Console.WriteLine(repo.Lutas);
                Console.WriteLine(repo.Derrotas);
                Console.WriteLine(repo.Vitorias);
                Console.WriteLine(repo.ArtesMarciais);
                Console.WriteLine();
                */
                competitors.Add(new FightCompetitor(){ Name=repo.Name,Age=repo.Idade,Losses=repo.Derrotas,Wins=repo.Vitorias,TotalFights=repo.Lutas,MartialArts=repo.ArtesMarciais});
            }
            
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
            var stages = new List<IStageTourment>();
            stages.Add(new FightStageTourment(){Name="Inicio"});
            stages.Add(new FightStageTourment(){Name="Grupos", StageCriteria=stageGroupCriterias,MatchCriteria=matchCriteria });
            stages.Add(new FightStageTourment(){Name="Quartas de final",StageCriteria=stage4thsCriterias,MatchCriteria=matchCriteria });
            stages.Add(new FightStageTourment(){Name="Semifinal",StageCriteria=stage2thsCriterias,MatchCriteria=matchCriteria});
            stages.Add(new FightStageTourment(){Name="Final",StageCriteria=stage2thsCriterias,MatchCriteria=matchCriteria,isRanking=true, rankingWinFactor=3,rankingLoseFactor=0});

            //Torneio
            var tournament = new MultiStageTourment();
            tournament.Groups = groups;
            tournament.Stages = stages;
            tournament.Competitors = competitors;

            //Gerenciador Torneio
            var manager = new TournamentManager(tournament);
            manager.ProcessMatches();


            Console.WriteLine("Hello World!");
        }
    
        private static async Task<List<Repository>> ProcessRepositories()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<Repository>));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            //http://177.36.237.87/lutadores/api/competidores
            //https://api.github.com/orgs/dotnet/repos
            var streamTask = client.GetStreamAsync("http://177.36.237.87/lutadores/api/competidores");
            var repositories = serializer.ReadObject(await streamTask) as List<Repository>;
            return repositories;
        }
    
    }
}
