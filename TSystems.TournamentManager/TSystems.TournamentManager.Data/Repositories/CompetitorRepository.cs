using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Linq;
using TSystems.TournamentManager.Domain;
using TSystems.TournamentManager.Data.Models;

namespace TSystems.TournamentManager.Data.Repository
{
    public class CompetitorRepository
    {
        private readonly HttpClient client = new HttpClient();

        public List<FightCompetitor> GetAll()
        {
                var listDataobjects = GetAllHttp().Result;
                var result = Map(listDataobjects);
                return result;
        }

        private async Task<List<FightCompetitorDataModel>> GetAllHttp()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<FightCompetitorDataModel>));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            //http://177.36.237.87/lutadores/api/competidores
            //https://api.github.com/orgs/dotnet/repos
            var streamTask = client.GetStreamAsync("http://177.36.237.87/lutadores/api/competidores");
            var repositories = serializer.ReadObject(await streamTask) as List<FightCompetitorDataModel>;
            return repositories;
        }


        //DTO Pattern - Mapping 
        private List<FightCompetitor> Map(List<FightCompetitorDataModel> list)
        {
            var competitors = new List<FightCompetitor>();
            foreach (var repo in list)
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
            return competitors;
        }

    }
}
