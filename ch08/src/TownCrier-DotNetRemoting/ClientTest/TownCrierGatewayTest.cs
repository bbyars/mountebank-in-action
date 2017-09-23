using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
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

//        [Test]
        public void SerTest()
        {
            var stubResult = new AnnouncementLog("Test Message");
            var serializeAnnounceCall = SerializeAnnounceCall(stubResult);
            Assert.That(serializeAnnounceCall, Is.EqualTo("Lk5FVAEAAAAAAD0BAAAEAAEBJQAAAHRjcDovL2xvY2FsaG9zdDozMDAwL1Rvd25DcmllclNlcnZpY2UGAAEBGAAAAGFwcGxpY2F0aW9uL29jdGV0LXN0cmVhbQAAAAEAAAD/////AQAAAAAAAAAVFAAAABIIQW5ub3VuY2USUVRvd25Dcmllci5DcmllciwgVG93bkNyaWVyLCBWZXJzaW9uPTEuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbBABAAAAAQAAAAkCAAAADAMAAABAVG93bkNyaWVyLCBWZXJzaW9uPTEuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbAUCAAAAHlRvd25Dcmllci5Bbm5vdW5jZW1lbnRUZW1wbGF0ZQIAAAAZPEdyZWV0aW5nPmtfX0JhY2tpbmdGaWVsZBY8VG9waWM+a19fQmFja2luZ0ZpZWxkAQEDAAAABgQAAAAFSGVsbG8GBQAAAAVUb3BpYws="));
        }
        [Test]
        public void ClientShouldAddSuccessMessage()
        {
            var imposter = mb.CreateTcpImposter(3000, "TownCrierService", TcpMode.Binary);
            var predicateFields = new TcpPredicateFields { Data = ToBase64("Announce") };
            var stubResult = new AnnouncementLog("Test Message");

            imposter.AddStub()
                .On(new ContainsPredicate<TcpPredicateFields>(predicateFields))
                .ReturnsData(SerializeAnnounceCall(stubResult));
            mb.Submit(imposter);

            var gateway = new TownCrierGateway(3000);
            var result = gateway.AnnounceToServer("ignore", "ignore");
            Assert.That(result, Does.Contain($"Call Success!\n{stubResult}"));
        }

        private string ToBase64(string plaintext)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(plaintext));
        }

        public string SerializeAnnounceCall(Object obj)
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
