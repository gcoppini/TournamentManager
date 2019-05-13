using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TSystems.TournamentManager.Data.Models;

namespace TSystems.TournamentManager.Data.Repository
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
    }
}