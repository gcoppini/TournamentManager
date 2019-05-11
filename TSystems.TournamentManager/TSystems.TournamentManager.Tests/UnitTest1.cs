using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TSystems.TournamentManager.Domain;
using TSystems.TournamentManager.Services;


namespace TSystems.TournamentManager.Tests
{
    [TestClass]
    public class TourmentServiceTest
    {
        private FightTourmentService _fightTourmentService;
        
        public TourmentServiceTest()
        {
             _fightTourmentService = new FightTourmentService();
        }

        [TestMethod]
        public void TestMethod1()
        {
        
        }

        //ArgumentException
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "A lista de participantes do campeonato não pode estar vazia")]
        public void EmptyCompetitorListNotAllowed()
        {
            var selected = new List<string>();
            var result = _fightTourmentService.Run(selected);
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(10)]
        [DataRow(18)]
        [ExpectedException(typeof(InvalidOperationException), "A lista de participantes deve ter 20 elementos")]
        public void ReturnExceptionWhenCompetitorQtyDifferent20(int value)
        {
            var selected = new List<string>();
            var result = _fightTourmentService.Run(selected);
            for(var i=0;i<value;i++)
            {
                selected.Add((new Random().Next()*10000).ToString());
            }
            //Assert.IsFalse(result, $"{value} should not be prime");
        }

    }
}
