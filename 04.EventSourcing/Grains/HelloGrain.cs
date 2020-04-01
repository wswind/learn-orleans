using Microsoft.Extensions.Logging;
using Orleans.EventSourcing;
using Orleans.Providers;
using System.Threading.Tasks;

namespace OrleansBasics
{
    [LogConsistencyProvider(ProviderName = "LogStorage")]
    public class HelloGrain : JournaledGrain<HelloState, EventData>, IHello
    {
        private readonly ILogger logger;

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            this.logger = logger;
            
        }
        
        public Task AddCount()
        {
            this.State.Count++;
            return Task.CompletedTask;
        }

        public Task<int> GetCount()
        {
            return Task.FromResult(this.State.Count);
        }

        public async Task NewEvent(EventData @event)
        {
            RaiseEvent(@event);
            await ConfirmEvents();
        }


        Task<string> IHello.SayHello(string greeting)
        {
            logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"\n Client said: '{greeting}', so HelloGrain says: Hello!");
        }
    }
}