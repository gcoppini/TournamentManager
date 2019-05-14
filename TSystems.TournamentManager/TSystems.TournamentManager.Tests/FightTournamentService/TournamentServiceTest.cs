using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TSystems.TournamentManager.Domain;
using TSystems.TournamentManager.Services;
using TSystems.TournamentManager.Data;
using TSystems.TournamentManager.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TSystems.TournamentManager.Tests
{
    [TestClass]
    public class TournamentServiceTest
    {
        private FightTournamentService _fightTournamentService;
        private List<FightCompetitorDataModel> _competitor;
        
        public TournamentServiceTest()
        {
             _fightTournamentService = new FightTournamentService();
             _competitor = new List<FightCompetitorDataModel>();
        }
        
        private List<string> selectRandom20Competitors(int qty=20)
        {
            var selected = new List<string>();
            int j = 0;

            while(j < qty)
            {
                var random = new Random();

                var name = _competitor
                .OrderBy(i => random.Next())
                .Take(1)
                .OrderBy(i => i)
                .First().Name;

                if(!selected.Contains(name))
                {
                    selected.Add(name);
                    j++;
                }
            }

            return selected;
        }

        [TestInitialize]
        public void Initialize() 
        {
             using (StreamReader r = new StreamReader(@"../../../Mock/data.json"))
            {
                string json = r.ReadToEnd();
                _competitor = JsonConvert.DeserializeObject<List<FightCompetitorDataModel>>(json);
            }
        }

        [TestMethod]
        public void ListOfRankedWinnerShouldBeReturn()
        {
            var selected = selectRandom20Competitors();
            var result = _fightTournamentService.Run(selected);
            Assert.IsTrue(result.Count > 0, $"Ao processar resultado deve ser retornada a lista com vencedores");
        }

        //ArgumentException
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "A lista de participantes do campeonato não pode estar vazia")]
        public void EmptyCompetitorListNotAllowed()
        {
            var selected = new List<string>();
            var result = _fightTournamentService.Run(selected);
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(10)]
        [DataRow(18)]
        [ExpectedException(typeof(InvalidOperationException), "A lista de participantes deve ter 20 elementos")]
        public void ReturnExceptionWhenCompetitorQtyDifferent20(int value)
        {
            var selected = new List<string>();
            
            for(var i=0;i<value;i++)
            {
                selected.Add((new Random().Next()*10000).ToString());
            }
            var result = _fightTournamentService.Run(selected);
            
            //Assert.IsFalse(result, $"{value} should not be prime");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "A lista participantes deve conter elementos distintos")]
        public void SameCompetitorMultipleTimesNotAllowed()
        {
            var selected = new List<string>();
            var competitor = selectRandom20Competitors(1).First();
            for(var i=0;i<20;i++)
            {
                selected.Add(competitor);
            }
            var result = _fightTournamentService.Run(selected);
        }

    }
}
