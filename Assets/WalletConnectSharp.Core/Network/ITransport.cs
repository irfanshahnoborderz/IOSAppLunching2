using System;
using System.Threading.Tasks;
using WalletConnectSharp.Core.Events.Request;
using WalletConnectSharp.Core.Events.Response;
using WalletConnectSharp.Core.Models;

namespace WalletConnectSharp.Core.Network
{
    public interface ITransport : IDisposable
    {
        event EventHandler<MessageReceivedEventArgs> MessageReceived;
        event EventHandler<MessageReceivedEventArgs> OpenReceived;
        event EventHandler<MessageReceivedEventArgs> CloseReceived;
        event EventHandler<MessageReceivedEventArgs> ErrorReceived;

        void DispatchMessageQueue();

        Task Open(string bridgeURL);

        Task Close();

        Task SendMessage(NetworkMessage message);

        Task Subscribe(string topic);

        Task Subscribe<T>(string topic, EventHandler<JsonRpcResponseEvent<T>> callback) where T : JsonRpcResponse;

        Task Subscribe<T>(string topic, EventHandler<JsonRpcRequestEvent<T>> callback) where T : JsonRpcRequest;
    }
}