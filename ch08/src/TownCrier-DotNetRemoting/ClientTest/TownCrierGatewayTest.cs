using System;
using System.IO;
using System.Runtime.Remoting.Messaging;
using System.Text;
using NUnit.Framework;
using Client;
using MbDotNet;
using MbDotNet.Enums;
using MbDotNet.Models.Predicates;
using MbDotNet.Models.Predicates.Fields;
using RemotingProtocolParser;
using RemotingProtocolParser.TCP;
using TownCrier;

namespace ClientTest
{
    [TestFixture]
    public class TownCrierGatewayTest
    {
        private readonly MountebankClient mb = new MountebankClient();

        [TearDown]
        public void TearDown()
        {
            mb.DeleteAllImposters();
        }

        [Test]
        public void ClientShouldAddSuccessMessage()
        {
            var stubResult = new AnnouncementLog("TEST");
            CreateImposter(3000, "Announce", stubResult);
            var gateway = new TownCrierGateway(3000);

            var result = gateway.AnnounceToServer("ignore", "ignore");

            Assert.That(result, Is.EqualTo($"Call Success!\n{stubResult}"));
        }

        [Test]
        public void LargeMessagesShouldBeOK()
        {
            var stubResult = new AnnouncementLog("TEST");
            CreateImposter(3000, "Announce", stubResult);
            var gateway = new TownCrierGateway(3000);
            var topic = "TEST";
            for (var i = 0; i < 1500; i += 1)
            {
                topic += "TEST";
            }

            var result = gateway.AnnounceToServer("ignore", topic);

            Assert.That(result, Is.EqualTo($"Call Success!\n{stubResult}"));
        }

        private void CreateImposter(int port, string methodName, AnnouncementLog result)
        {
            var imposter = mb.CreateTcpImposter(port, "", TcpMode.Binary);
            imposter.AddStub()
                .On(ContainsMethodName(methodName))
                .ReturnsData(Serialize(result));
            mb.Submit(imposter);
        }

        private ContainsPredicate<TcpPredicateFields> ContainsMethodName(string methodName)
        {
            var predicateFields = new TcpPredicateFields { Data = ToBase64(methodName) };
            return new ContainsPredicate<TcpPredicateFields>(predicateFields);
        }

        private string ToBase64(string plaintext)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plaintext));
        }

        public string Serialize(Object obj)
        {
            var messageRequest = new MethodCall(new[] {
                new Header(MessageHeader.Uri, "tcp://localhost:3000/TownCrier"),
                new Header(MessageHeader.MethodName, "Announce"),
                new Header(MessageHeader.MethodSignature, new[] { typeof(AnnouncementTemplate) }),
                new Header(MessageHeader.TypeName, typeof(Crier).AssemblyQualifiedName),
                new Header(MessageHeader.Args, new[] { obj })
            });
            var responseMessage = new MethodResponse(new[]
            {
                new Header(MessageHeader.Return, obj)
            }, messageRequest);

            var responseStream = BinaryFormatterHelper.SerializeObject(responseMessage);
            using (var stream = new MemoryStream())
            {
                var handle = new TcpProtocolHandle(stream);
                handle.WritePreamble();
                handle.WriteMajorVersion();
                handle.WriteMinorVersion();
                handle.WriteOperation(TcpOperations.Reply);
                handle.WriteContentDelimiter(TcpContentDelimiter.ContentLength);
                handle.WriteContentLength(responseStream.Length);
                handle.WriteTransportHeaders(null);
                handle.WriteContent(responseStream);
                return Convert.ToBase64String(stream.ToArray());
            }
        }
    }
}
