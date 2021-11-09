using System.Collections.Generic;
using System.Threading.Tasks;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.Client.RpcMessages;
using System;
using UnityEngine;
using UnityEditor;

namespace WalletConnectSharp.Core.Client.Nethereum
{
    public class WalletConnectClient : ClientBase
    {
        private long _id;
        public WalletConnect Provider { get; }

        public WalletConnectClient(WalletConnect provider)
        {
            this.Provider = provider;
        }

        protected override async Task<RpcResponseMessage> SendAsync(RpcRequestMessage message, string route = null)
        {
            //Debug.Log("insidethedefault message response");
            _id += 1;
            var mapParameters = message.RawParameters as Dictionary<string, object>;
            var arrayParameters = message.RawParameters as object[];
            var rawParameters = message.RawParameters;

            RpcRequestMessage rpcRequestMessage;
            if (mapParameters != null) 
                rpcRequestMessage = new RpcRequestMessage(_id, message.Method, mapParameters);
            else if (arrayParameters != null)
                rpcRequestMessage = new RpcRequestMessage(_id, message.Method, arrayParameters);
            else
                rpcRequestMessage = new RpcRequestMessage(_id, message.Method, rawParameters);
            
            TaskCompletionSource<RpcResponseMessage> eventCompleted = new TaskCompletionSource<RpcResponseMessage>(TaskCreationOptions.None);
            
            Provider.Events.ListenForResponse<RpcResponseMessage>(rpcRequestMessage.Id, (sender, args) =>
            {
                eventCompleted.TrySetResult(args.Response);
            });
            
            await Provider.SendRequest(rpcRequestMessage);

            return await eventCompleted.Task;
        }
    }
}