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
            if (args.Length<1 || !int.TryParse(args[0], out int test) || test<1 || test>2)
            {
                Console.WriteLine("At the moment only modes 1 and 2 are supported.");
                Console.WriteLine("Mode 1 runs a lock time comparison");
                Console.WriteLine("Mode 2 runs some thread time and deadlock tests.");
            }
            else
            {
                switch (test)
                {
                    case 1:
                        RunLockTests(args);
                        break;
                    case 2:
                        RunThreadTests(args);
                        break;
                }
            }


        }

        private static void RunThreadTests(string[] args)
        {
            long amountOfWork;
            long noOfThreads;
            Work work = new Work();
            //            WorkWithReaderWriteLock work = new WorkWithReaderWriteLock();

            if (args.Length<2 || !long.TryParse(args[1], out amountOfWork))
            {
                amountOfWork = 50000;
            }
            work.AmountOfWork = amountOfWork;
            if (args.Length <3 || !long.TryParse(args[2], out noOfThreads))
            {
                noOfThreads = 1000;
            }

            Console.WriteLine($"Locktest (regular lock). Starting with {noOfThreads} threads and amountOfWork={amountOfWork}");

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

        private static void RunLockTests(string[] args)
        {
            if (args.Length<2 || !long.TryParse(args[1], out long amountOfWork))
            {
                amountOfWork = 50000;
            }
            var lockClass = new LockTime();
            lockClass.RunStandardLock(amountOfWork);
            lockClass.RunReaderWriterWithReadLock(amountOfWork);
            lockClass.RunReaderWriterWithWriteLock(amountOfWork);
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
