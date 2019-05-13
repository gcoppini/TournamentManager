using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TSystems.TournamentManager.Data.Repository
{
    public class BaseRepository<TEntity> : IDisposable, IReadOnlyRepository<TEntity> where TEntity : class
    {
        // Flag stating if the current instance is allready disposed.
        private bool _disposed;


        public override string ToString()
        {
            return "Repository with type : " + typeof(TEntity).Name;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize((object)this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose all managed resources here.
               
            }

            _disposed = true;
        }
    }
}