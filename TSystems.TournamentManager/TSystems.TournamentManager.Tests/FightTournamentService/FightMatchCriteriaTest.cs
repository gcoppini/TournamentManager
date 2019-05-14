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
    public class FightMatchCriteriaTest
    {
        public FightMatchCriteriaTest()
        {
        }

         [TestInitialize]
        public void Initialize() 
        {
        }

        [TestMethod]
        public void HigherWinPercentageShouldWin()
        {
            var competitors = new List<ICompetitor>();   
            var matches = new List<IFightMatch>();
            var match = new FightMatch();

            var winner = new FightCompetitor();
            winner.Name = "Vencedor";
            winner.Wins = 10;
            winner.Losses = 0;
            winner.TotalFights = 10;
            winner.MartialArts = new List<string>(){"a", "b", "c"};

            var loser = new FightCompetitor();
            loser.Name = "Perdedor";
            loser.Wins = 0;
            loser.Losses = 10;
            loser.TotalFights = 10;
            loser.MartialArts = new List<string>(){"a"};
            
            competitors.Add(winner);
            competitors.Add(loser);
           
            match.Competitors = competitors;
            matches.Add(match);
            
            WinPercentageCriteria criteria = new WinPercentageCriteria();
            
            var results = criteria.Apply(matches);
            var matchResult = (FightMatch)results.First();

            Assert.IsTrue(matchResult.Winner == winner, $"Pelo critério da maior porcentagem de vitória, este deveria ser o vencedor");
            Assert.IsTrue(matchResult.Loser == loser, $"Pelo critério da maior porcentagem de vitória, este deveria ser o perdedor");
        }

        [TestMethod]
        public void HigherMartialArtsQtyPercentageShouldWin()
        {
            var competitors = new List<ICompetitor>();   
            var matches = new List<IFightMatch>();
            var match = new FightMatch();

            var winner = new FightCompetitor();
            winner.Name = "Vencedor";
            winner.Wins = 10;
            winner.Losses = 0;
            winner.TotalFights = 10;
            winner.MartialArts = new List<string>(){"a", "b", "c"};

            var loser = new FightCompetitor();
            loser.Name = "Perdedor";
            loser.Wins = 10;
            loser.Losses = 0;
            loser.TotalFights = 10;
            loser.MartialArts = new List<string>(){"a"};
            
            competitors.Add(winner);
            competitors.Add(loser);
           
            match.Competitors = competitors;
            matches.Add(match);
            
            WinPercentageCriteria criteria = new WinPercentageCriteria();
            
            var results = criteria.Apply(matches);
            var matchResult = (FightMatch)results.First();

            Assert.IsTrue(matchResult.Winner == winner, $"Pelo critério da maior qtd de artes marciais, este deveria ser o vencedor");
            Assert.IsTrue(matchResult.Loser == loser, $"Pelo critério da maior qtd de artes marciais, este deveria ser o perdedor");


        }
        
        [TestMethod]
        public void HigherFightsQtyShouldWins()
        {
            var competitors = new List<ICompetitor>();   
            var matches = new List<IFightMatch>();
            var match = new FightMatch();

            var winner = new FightCompetitor();
            winner.Name = "Vencedor";
            winner.Wins = 11;
            winner.Losses = 0;
            winner.TotalFights = 11;
            winner.MartialArts = new List<string>(){"a", "b", "c"};

            var loser = new FightCompetitor();
            loser.Name = "Perdedor";
            loser.Wins = 10;
            loser.Losses = 0;
            loser.TotalFights = 10;
            loser.MartialArts = new List<string>(){"a", "b", "c"};
            
            competitors.Add(winner);
            competitors.Add(loser);
           
            match.Competitors = competitors;
            matches.Add(match);
            
            WinPercentageCriteria criteria = new WinPercentageCriteria();
            
            var results = criteria.Apply(matches);
            var matchResult = (FightMatch)results.First();

            Assert.IsTrue(matchResult.Winner == winner, $"Pelo critério da maior quantidade de lutas, este deveria ser o vencedor");
            Assert.IsTrue(matchResult.Loser == loser, $"Pelo critério da maior quantidade de lutas, este deveria ser o perdedor");


        }
    }
}
