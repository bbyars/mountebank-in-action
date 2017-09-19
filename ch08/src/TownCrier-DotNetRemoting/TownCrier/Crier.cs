using System;

namespace TownCrier
{
    public class Crier : MarshalByRefObject
    {
        public delegate void AnnounceHandler(string topic);
        public event AnnounceHandler AnnounceTopic;

        public string Announce(string topic)
        {
            AnnounceTopic?.Invoke(topic);
            return $"Hear ye! Hear ye! On this datetime {DateTime.Now} I hearby announce that {topic}";
        }
    }
}
