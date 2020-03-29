using Microsoft.Extensions.Logging;
using Orleans.Providers;
using System.Threading.Tasks;

namespace OrleansBasics
{
    [StorageProvider(ProviderName="DevStore")]
    public class HelloGrain : Orleans.Grain<PersistentData>, IHello
    {
        private readonly ILogger logger;

        public override Task OnActivateAsync()
        {
            this.ReadStateAsync();
            return base.OnActivateAsync();
        }

        public override Task OnDeactivateAsync()
        {
            this.WriteStateAsync();
            return base.OnDeactivateAsync();
        }

        public HelloGrain(ILogger<HelloGrain> logger)
        {
            this.logger = logger;
            
        }

        public async Task AddCount()
        {
            this.State.Count ++;
            await this.WriteStateAsync();
        }

        public Task<int> GetCount()
        {
            return Task.FromResult(this.State.Count);
        }

        Task<string> IHello.SayHello(string greeting)
        {
            logger.LogInformation($"\n SayHello message received: greeting = '{greeting}'");
            return Task.FromResult($"\n Client said: '{greeting}', so HelloGrain says: Hello!");
        }
    }
}