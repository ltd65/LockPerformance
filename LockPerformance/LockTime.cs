using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace LockPerformance
{
    public class LockTime
    {
        public long artificialResult;
        private object lockObject = new object();

        private ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);

        public void RunStandardLock(long amount)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            artificialResult = 0;
            for (long i = 0; i < amount; i++)
            {
                lock(lockObject)
                {
                    artificialResult += i;
                }
            }
            watch.Stop();
            Console.WriteLine($"Standard Lock needed {watch.Elapsed.TotalMilliseconds} ms.");

        }

        public void RunReaderWriterWithReadLock(long amount)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            artificialResult = 0;
            for (long i = 0; i < amount; i++)
            {
                lockSlim.EnterReadLock();
                artificialResult += i;
                lockSlim.ExitReadLock();
            }
            watch.Stop();
            Console.WriteLine($"Slim Read Lock needed {watch.Elapsed.TotalMilliseconds} ms.");
        }

        public void RunReaderWriterWithWriteLock(long amount)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            artificialResult = 0;
            for (long i = 0; i < amount; i++)
            {
                lockSlim.EnterWriteLock();
                artificialResult += i;
                lockSlim.ExitWriteLock();
            }
            watch.Stop();
            Console.WriteLine($"Slim write Lock needed {watch.Elapsed.TotalMilliseconds} ms.");

        }
    }
}
