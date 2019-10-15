﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Streaming.Payloads;
using Microsoft.Bot.Streaming.PayloadTransport;
using Microsoft.Bot.Streaming.Transport;

namespace Microsoft.Bot.Streaming.UnitTests
{
    internal class MockPayloadSender : IPayloadSender
    {
        public MockPayloadSender()
        {
            SentHeaders = new List<Header>();
        }

        public event DisconnectedEventHandler Disconnected;

        public List<Header> SentHeaders { get; set; }

        public bool IsConnected => throw new NotImplementedException();

        public void Connect(ITransportSender sender)
        {
            throw new NotImplementedException();
        }

        public void Disconnect(DisconnectedEventArgs e = null)
        {
            throw new NotImplementedException();
        }

        public void SendPayload(Header header, Stream payload, bool isLengthKnown, Func<Header, Task> sentCallback)
        {
            SentHeaders.Add(header);
        }
    }
}
