using System;
using System.Collections.Generic;
using System.Threading;
using locktest_regular_lock;

namespace LockPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            long amountOfWork;
            long noOfThreads;
            Work work = new Work();
//            WorkWithReaderWriteLock work = new WorkWithReaderWriteLock();

            if (!long.TryParse(args[0], out amountOfWork))
            {
                amountOfWork = 50000;
            }
            work.AmountOfWork = amountOfWork;
            if (!long.TryParse(args[1], out noOfThreads))
            {
                noOfThreads = 1000;
            }

            Console.WriteLine(
                $"Locktest (regular lock). Starting with {noOfThreads} threads and amountOfWork={amountOfWork}");

            List<Thread> workers = new List<Thread>();
            for (long threads = 0; threads < noOfThreads; threads++)
            {
                Thread worker = new Thread(DoWork);
                workers.Add(worker);
            }
            foreach (Thread worker in workers)
            {
                worker.Start(work);
            }
        }

        public static void DoWork(object x)
        {
            Work y = (Work)x;
            //            WorkWithReaderWriteLock y = (WorkWithReaderWriteLock)x;

            do
            {
                long result = y.DoSomeWork();
            } while (true);
        }
    }
}
