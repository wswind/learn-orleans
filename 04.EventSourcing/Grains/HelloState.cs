
namespace OrleansBasics
{
    public class HelloState
    {
        public int Count { get; set; }
        public void Apply(EventDataAdd addData)
        {
            Count += addData.AddCount;
        }
        public void Apply(EventDataMinus minusData)
        {
            Count -= minusData.MinusCount;
        }
    }
}