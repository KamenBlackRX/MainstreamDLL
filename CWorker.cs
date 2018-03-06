using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mainstream.Threading.Worker
{
    public static class CWorker
    {
        private static CancellationTokenSource cancellToken;

        public static void RunAsync(Action func, params object[] args)
        {
            cancellToken = new CancellationTokenSource();
            Task.Factory.StartNew( () => func, cancellToken.Token);            

        }

        public static void KillAllThreads()
        {
            if(cancellToken != null)
                cancellToken.Cancel();
        }
    }
}
