using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace OrleansBasics
{
    public class HelloGrain : Orleans.Grain, IHello
    {
        private readonly ILogger logger;
        private int _count;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            this.logger = logger;
            _count = 0;
        }

        public Task AddCount()
        {
            _count++;
            return Task.CompletedTask;
        }

        public Task<int> GetCount()
        {
            return Task.FromResult(_count);
        }

        Task<string> IHello.SayHello(string greeting)
        {
            logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"\n Client said: '{greeting}', so HelloGrain says: Hello!");
        }
    }
}