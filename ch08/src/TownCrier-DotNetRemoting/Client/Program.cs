using System;

namespace Client
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var port = int.Parse(args[0]);
            var gateway = new TownCrierGateway(port);
            Console.WriteLine(gateway.AnnounceToServer(args[1], args[2]));
        }
    }
}
