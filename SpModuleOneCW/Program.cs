namespace SpModuleOneCW
{
    internal class Program
    {
        private static Mutex mutex = new Mutex();
        private static bool isFirstThreadFinished = false;

        static void Main()
        {
            Thread thread1 = new Thread(PrintAscending);
            Thread thread2 = new Thread(PrintDescending);
            thread1.Start();
            thread2.Start();

            Console.WriteLine("Программа завершена.");
        }

        static void PrintAscending()
        {
            mutex.WaitOne();

            for (int i = 0; i <= 20; i++)
            {
                Console.WriteLine("Первый поток: " + i);
                Thread.Sleep(100);
            }

            isFirstThreadFinished = true;
            mutex.ReleaseMutex();
        }

        static void PrintDescending()
        {
            while (!isFirstThreadFinished)
            {
                mutex.WaitOne();

                for (int i = 10; i >= 0; i--)
                {
                    Console.WriteLine("Второй поток: " + i);
                    Thread.Sleep(100);
                }

                mutex.ReleaseMutex();
            }
        }
    }
}