using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace locktest_regular_lock
{
    public class Work
    {
        private readonly object _lockObject=new object();
        public long AmountOfWork = 50000;
        public long DoSomeWork()
        {
            long result = 0;
            lock (_lockObject)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} aquired lock and will do {AmountOfWork} units of work.");
                int x =new Random().Next(1,500000);
                Random rnd = new Random();
                Stopwatch watch = new Stopwatch();
                watch.Start();
                for (long l = 0; l < AmountOfWork; l++)
                {
                    x = x + (int)l;
                    if (l % 2 == 0)
                    {
                        result = x=x+rnd.Next(1,100);
                    }
                }
                watch.Stop();
                Console.WriteLine($"Result of calculation: {result} Time: {watch.ElapsedMilliseconds}");
            }
            return result;
        }
    }
}
