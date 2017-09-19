using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using TownCrier;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            var port = int.Parse(args[0]);
            var url = $"tcp://localhost:{port}/TownCrierService";

            var topic = args[1];
            var crier = (Crier)Activator.GetObject(typeof(Crier), url);
            Console.WriteLine(crier.Announce(topic));
        }
    }
}
