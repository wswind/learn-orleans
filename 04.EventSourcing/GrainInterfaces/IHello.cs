using System.Threading.Tasks;

namespace OrleansBasics
{
    public interface IHello : Orleans.IGrainWithIntegerKey
    {
        Task<string> SayHello(string greeting);
        Task AddCount();
        Task<int> GetCount();
        Task NewEvent(EventData @event);
    }
}