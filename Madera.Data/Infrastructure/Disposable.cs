using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Madera.Data.Infrastructure
{
    public class Disposable : IDisposable
    {
        private bool isDisposed;

        public Disposable()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }

        // Ovveride this to dispose custom objects
        protected virtual void DisposeCore()
        {
        }
    }
}