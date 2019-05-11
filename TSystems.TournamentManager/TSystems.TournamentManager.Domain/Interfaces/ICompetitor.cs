using System;

namespace TSystems.TournamentManager.Domain
{
    public interface ICompetitor
    {
        string Name { get; set; }
        int Age { get; set; }

    }
}
