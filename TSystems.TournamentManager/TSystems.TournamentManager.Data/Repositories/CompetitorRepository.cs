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
    public class CompetitorRepository : BaseRepository<FightCompetitor>
    {

        private readonly HttpClient client = new HttpClient();

        private readonly string REPOSITORY_ENDPOINT_URL = "http://177.36.237.87/lutadores/api/competidores";

        public List<FightCompetitor> GetAll()
        {
                var listDataobjects = GetAllHttp().Result;
                var result = Map(listDataobjects);
                return result;
        }

        private async Task<List<FightCompetitorDataModel>> GetAllHttp()
        {
            var result = new List<FightCompetitorDataModel>();
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(List<FightCompetitorDataModel>));

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
                client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
                var streamTask = client.GetStreamAsync(REPOSITORY_ENDPOINT_URL);
                result = serializer.ReadObject(await streamTask) as List<FightCompetitorDataModel>;
            }
            catch(Exception ex)
            {
                   /*
                    [{"nome":"Muhammad Ali","idade":74,"artesMarciais":["Boxe"],"lutas":61,"derrotas":5,"vitorias":56},{"nome":"Chuck Liddell","idade":49,"artesMarciais":["Wrestling","Kempo","Kickboxing"],"lutas":30,"derrotas":9,"vitorias":21},{"nome":"Sugar Ray Robinson","idade":67,"artesMarciais":["Boxe"],"lutas":200,"derrotas":19,"vitorias":173},{"nome":"Anderson Silva","idade":43,"artesMarciais":["Boxe","Muay Thai","Jiu-Jitsu","Taekwondo","Capoeira"],"lutas":44,"derrotas":9,"vitorias":34},{"nome":"George Foreman","idade":70,"artesMarciais":["Boxe"],"lutas":81,"derrotas":5,"vitorias":76},{"nome":"Sugar Ray Leonard","idade":62,"artesMarciais":["Boxe"],"lutas":40,"derrotas":3,"vitorias":36},{"nome":"Jon Jones","idade":31,"artesMarciais":["Wrestling","Jiu-Jitsu"],"lutas":25,"derrotas":1,"vitorias":23},{"nome":"Rocky Marciano","idade":45,"artesMarciais":["Boxe"],"lutas":49,"derrotas":0,"vitorias":49},{"nome":"Vitor Belfort","idade":41,"artesMarciais":["Boxe","Jiu-Jitsu","Muay Thai","Judô"],"lutas":40,"derrotas":14,"vitorias":26},{"nome":"Floyd MayweatherJr.","idade":41,"artesMarciais":["Boxe"],"lutas":50,"derrotas":0,"vitorias":50},{"nome":"Lyoto Machida","idade":40,"artesMarciais":["Karatê","Shotokan","Jiu-Jitsu","Capoeira"],"lutas":33,"derrotas":8,"vitorias":25},{"nome":"Cain Velasquez","idade":36,"artesMarciais":["Boxe","Jiu-jítsu","Kickboxing","Muay Thai"],"lutas":17,"derrotas":3,"vitorias":14},{"nome":"Cigano","idade":35,"artesMarciais":["Boxe","Jiu-jítsu"],"lutas":25,"derrotas":5,"vitorias":20},{"nome":"Jacaré","idade":39,"artesMarciais":["Jiu-jítsu"],"lutas":32,"derrotas":6,"vitorias":26},{"nome":"Chael Sonnen","idade":41,"artesMarciais":["Greco-romana"],"lutas":47,"derrotas":16,"vitorias":30},{"nome":"George Saint Pierre","idade":37,"artesMarciais":["Boxe","Jiu-jítsu","Kyokushin","Muay thai","Jiu-jitsu"],"lutas":28,"derrotas":2,"vitorias":26},{"nome":"Matt Hughes","idade":45,"artesMarciais":["Artes marciais mistas"],"lutas":54,"derrotas":9,"vitorias":45},{"nome":"Demetrious Johnson","idade":32,"artesMarciais":["Olímpica estilo livre"],"lutas":31,"derrotas":3,"vitorias":27},{"nome":"Evander Holyfield","idade":56,"artesMarciais":["Boxe"],"lutas":57,"derrotas":10,"vitorias":44},{"nome":"Mike Tyson","idade":52,"artesMarciais":["Boxe"],"lutas":58,"derrotas":6,"vitorias":50},{"nome":"Manny Pacquiao","idade":40,"artesMarciais":["Boxe"],"lutas":70,"derrotas":7,"vitorias":61},{"nome":"Eder Jofre","idade":82,"artesMarciais":["Boxe"],"lutas":81,"derrotas":2,"vitorias":77},{"nome":"Acelino Popó Freitas","idade":43,"artesMarciais":["Boxe"],"lutas":43,"derrotas":2,"vitorias":41},{"nome":"Micky Ward","idade":53,"artesMarciais":["Boxe"],"lutas":51,"derrotas":13,"vitorias":38},{"nome":"Joe Louis","idade":66,"artesMarciais":["Boxe"],"lutas":69,"derrotas":3,"vitorias":66},{"nome":"Roberto Duran","idade":67,"artesMarciais":["Boxe"],"lutas":119,"derrotas":16,"vitorias":103},{"nome":"Julio Cesar Chavez","idade":56,"artesMarciais":["Boxe"],"lutas":115,"derrotas":6,"vitorias":107},{"nome":"Wanderlei Silva","idade":42,"artesMarciais":["Muay Thay","Jiu-Jitsu"],"lutas":50,"derrotas":13,"vitorias":35},{"nome":"José Aldo","idade":32,"artesMarciais":["Muay Thai","Jiu-Jitsu"],"lutas":32,"derrotas":4,"vitorias":28},{"nome":"Conor McGregor","idade":30,"artesMarciais":["Boxe","Jiu-jítsu","Kickboxing","Capoeira","Karatê","Taekwondo"],"lutas":25,"derrotas":4,"vitorias":21},{"nome":"Rafael dos Anjos","idade":34,"artesMarciais":["Boxe","Jiu-jítsu","Muay thai"],"lutas":39,"derrotas":11,"vitorias":28},{"nome":"Thiago Marreta","idade":35,"artesMarciais":["Muay thai","Capoeira"],"lutas":26,"derrotas":5,"vitorias":21},{"nome":"Henry Cejudo","idade":32,"artesMarciais":["Olímpica estilo livre"],"lutas":16,"derrotas":2,"vitorias":14},{"nome":"Tyron Woodley","idade":36,"artesMarciais":["Wrestling","KickBoxing","Boxe"],"lutas":23,"derrotas":3,"vitorias":19},{"nome":"Rocky Balboa","idade":42,"artesMarciais":["Boxe"],"lutas":81,"derrotas":23,"vitorias":57},{"nome":"Apollo Creed","idade":43,"artesMarciais":["Boxe"],"lutas":47,"derrotas":1,"vitorias":46},{"nome":"Adonis Creed","idade":32,"artesMarciais":["Boxe"],"lutas":26,"derrotas":1,"vitorias":25}]
                    */
            }
            return result;
        }


        //DTO Pattern - Mapping | + ou - :o)
        private List<FightCompetitor> Map(List<FightCompetitorDataModel> dataModelList)
        {
            var competitors = new List<FightCompetitor>();
            foreach (var item in dataModelList)
            { /*
                Console.WriteLine(repo.Name);
                Console.WriteLine(repo.Idade);
                Console.WriteLine(repo.Lutas);
                Console.WriteLine(repo.Derrotas);
                Console.WriteLine(repo.Vitorias);
                Console.WriteLine(repo.ArtesMarciais);
                Console.WriteLine();
                */
                competitors.Add(new FightCompetitor(){ Name=item.Name,Age=item.Idade,Losses=item.Derrotas,Wins=item.Vitorias,TotalFights=item.Lutas,MartialArts=item.ArtesMarciais});
            }
            return competitors;
        }

    }
}
