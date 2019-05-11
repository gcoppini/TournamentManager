using System;
using System.Collections;
using System.Collections.Generic;

namespace TSystems
{
    public class FightCompetitor : ICompetitor,  IComparable<FightCompetitor>
    {
        public string Name { get; set; }
        public int Age { get; set; }
        
        public List<string> MartialArts { get; set; }
       
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int TotalFights { get; set; }

        public int Points { get; set; }

        public int? Ranking { get; set; }

        public int WinPercentagem { 
            get
            {
                 var p = ((double)Wins / (double)TotalFights) * 100;
                 return (int)p;
            }
          
        }

        public IGroupTournament Group { get; set; }

        public void AddWin()
        {
            this.Wins++;
            this.TotalFights++;
            this.Points++;
        }

        public void AddLose()
        {
            this.Losses++;
            this.TotalFights++;
        }
        
        public int CompareTo(FightCompetitor other)
        {
            // Alphabetic sort if salary is equal. [A to Z]
            if (this.Name == other.Name)
            {
                return this.Name.CompareTo(other.Name);
            }
            // Default to salary sort. [High to low]
            return other.Name.CompareTo(this.Name);
        }

        public override string ToString()
        {
            // String representation.
            return this.Name + " - " + this.WinPercentagem + " - "+this.MartialArts.Count+" - " + this.TotalFights;
        }

    }
}