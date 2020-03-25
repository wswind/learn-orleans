using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static int count = 0;
        static void Main(string[] args)
        {
            object locker = new object();
            var tasks = new List<Task>();
            for (int i = 0; i < 100; i++)
            {
                var task = Task.Run(() => {
                    lock(locker)
                    {
                        count++;
                    }
                
                });
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine(count);
        }
    }
}
