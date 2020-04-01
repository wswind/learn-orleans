using System;

namespace OrleansBasics
{
    [Serializable]
    public class EventData
    {
        public EventData()
        {
            When = DateTime.UtcNow;
        }

        public DateTime When;
    }

    public class EventDataAdd : EventData
    {
        public int AddCount;
    }

    public class EventDataMinus : EventData
    {
        public int MinusCount;
    }
}
