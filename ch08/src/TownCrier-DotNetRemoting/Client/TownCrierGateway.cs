using System;
using TownCrier;

namespace Client
{
    public class TownCrierGateway
    {
        private readonly int port;

        public TownCrierGateway(int port)
        {
            this.port = port;
        }

        public string AnnounceToServer(string greeting, string topic)
        {
            var url = $"tcp://localhost:{port}/TownCrierService";

            var template = new AnnouncementTemplate(greeting, topic);
            var crier = (Crier)Activator.GetObject(typeof(Crier), url);
            var response = crier.Announce(template).ToString();
            return $"Call Success!\n{response}";
        }

//        public string AnnounceToServer(string topic)
//        {
//            var url = $"tcp://localhost:{port}/TownCrierService";
//
//            var crier = (Crier)Activator.GetObject(typeof(Crier), url);
//            var response = crier.Announce2(topic);
//            return $"Call Success!\n{response}";
//        }
    }
}
