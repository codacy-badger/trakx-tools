﻿using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Trakx.Data.Common.Sources.CryptoCompare.DTOs.Inbound;

namespace Trakx.Data.Common.Sources.CryptoCompare
{
    public interface IWebSocketStreamer
    {
        IObservable<InboundMessageBase> AllInboundMessagesStream { get; }
        IObservable<Trade> TradeStream { get; }
        IObservable<Ticker> TickerStream { get; }
        IObservable<AggregateIndex> AggregateIndexStream { get; }
        IObservable<SubscribeComplete> SubscribeCompleteStream { get; }
        IObservable<UnsubscribeComplete> UnsubscribeCompleteStream { get; }
        IObservable<LoadComplete> LoadCompleteStream { get; }
        IObservable<UnsubscribeAllComplete> UnsubscribeAllCompleteStream { get; }
        IObservable<HeartBeat> HeartBeatStream { get; }
        void PublishInboundMessageOnStream(string rawMessage);
    }

    public class WebSocketStreamer : IWebSocketStreamer
    {
        private readonly ILogger<WebSocketStreamer> _logger;
        private readonly ISubject<InboundMessageBase> _incomingMessageSubject;
        
        public WebSocketStreamer(ILogger<WebSocketStreamer> logger)
        {
            _logger = logger;
            _incomingMessageSubject = new ReplaySubject<InboundMessageBase>(1);
        }

        public IObservable<InboundMessageBase> AllInboundMessagesStream => _incomingMessageSubject.AsObservable();
        public IObservable<Trade> TradeStream => _incomingMessageSubject.Cast<Trade>().AsObservable();
        public IObservable<Ticker> TickerStream => _incomingMessageSubject.Cast<Ticker>().AsObservable();
        public IObservable<AggregateIndex> AggregateIndexStream => _incomingMessageSubject.Cast<AggregateIndex>().AsObservable();
        public IObservable<Ohlc> OhlcStream => _incomingMessageSubject.Cast<Ohlc>().AsObservable();
        public IObservable<SubscribeComplete> SubscribeCompleteStream => _incomingMessageSubject.Cast<SubscribeComplete>().AsObservable();
        public IObservable<UnsubscribeComplete> UnsubscribeCompleteStream => _incomingMessageSubject.Cast<UnsubscribeComplete>().AsObservable();
        public IObservable<LoadComplete> LoadCompleteStream => _incomingMessageSubject.Cast<LoadComplete>().AsObservable();
        public IObservable<UnsubscribeAllComplete> UnsubscribeAllCompleteStream => _incomingMessageSubject.Cast<UnsubscribeAllComplete>().AsObservable();
        public IObservable<HeartBeat> HeartBeatStream => _incomingMessageSubject.Cast<HeartBeat>().AsObservable();

        public void PublishInboundMessageOnStream(string rawMessage)
        {
            try
            {
                _logger.LogTrace("Received WebSocketInboundMessage {0}{1}", Environment.NewLine, rawMessage);
                var message = JsonSerializer.Deserialize<InboundMessageBase>(rawMessage);
                switch (message.Type)
                {
                    case Trade.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<Trade>(rawMessage));
                        break;
                    case Ticker.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<Ticker>(rawMessage));
                        break;
                    case AggregateIndex.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<AggregateIndex>(rawMessage));
                        break;
                    case Ohlc.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<Ohlc>(rawMessage));
                        break;
                    case SubscribeComplete.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<SubscribeComplete>(rawMessage));
                        break;
                    case UnsubscribeComplete.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<SubscribeComplete>(rawMessage));
                        break;
                    case LoadComplete.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<LoadComplete>(rawMessage));
                        break;
                    case UnsubscribeAllComplete.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<SubscribeComplete>(rawMessage));
                        break;
                    case HeartBeat.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<HeartBeat>(rawMessage));
                        break;
                    case Error.TypeValue:
                        _incomingMessageSubject.OnNext(JsonSerializer.Deserialize<Error>(rawMessage));
                        break;
                    default:
                        return;
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, "Failed to publish {0}", rawMessage);
            }
        }
    }
}