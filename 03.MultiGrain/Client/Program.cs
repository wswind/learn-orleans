using Microsoft.Extensions.Logging;
using Orleans;
using Orleans.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrleansBasics
{
    public class Program
    {
        static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                using (var client = await ConnectClient())
                {
                    await DoClientWork(client);
                    Console.ReadKey();
                }

                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine($"\nException while trying to run client: {e.Message}");
                Console.WriteLine("Make sure the silo the client is trying to connect to is running.");
                Console.WriteLine("\nPress any key to exit.");
                Console.ReadKey();
                return 1;
            }
        }

        private static async Task<IClusterClient> ConnectClient()
        {
            IClusterClient client;
            client = new ClientBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                })
                .ConfigureLogging(logging => logging.AddConsole())
                .Build();

            await client.Connect();
            Console.WriteLine("Client successfully connected to silo host \n");
            return client;
        }

        private static async Task DoClientWork(IClusterClient client)
        {
           
            var client1 = client.GetGrain<IHello>(0);
            var client2 = client.GetGrain<IHello>(1);

            //https://dotnet.github.io/orleans/Documentation/grains/grain_identity.html
            var id1 = client1.GetGrainIdentity().GetPrimaryKeyLong(out string keyExt);
            var id2 = client2.GetGrainIdentity().GetPrimaryKeyLong(out string keyExt2);

            Console.WriteLine(id1);
            Console.WriteLine(keyExt);
            Console.WriteLine(id2);
            Console.WriteLine(keyExt2);

            await client1.AddCount();
            var count1 = await client1.GetCount();
            Console.WriteLine("count1:{0}", count1);

            await client2.AddCount();
            await client2.AddCount();
            var count2 = await client2.GetCount();
            Console.WriteLine("count2:{0}", count2);

        }
    }
}