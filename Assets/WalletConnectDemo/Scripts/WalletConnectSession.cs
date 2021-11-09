using Nethereum.Web3;
using Nethereum.JsonRpc;
using Nethereum.Util;
using Nethereum.Signer;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using WalletConnectSharp.Core;
using WalletConnectSharp.Core.Models;
using System.Threading.Tasks;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Model;
using Nethereum.Contracts;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.Extensions;

using Nethereum.JsonRpc.UnityClient;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Util;

public class WalletConnectSession : MonoBehaviour
{
    
    private WalletConnect walletConnect;
    private WalletConnectView walletConnectView;
    public UserAuthorization userAthr;

    private Web3 web3;   

    bool isSessionClosed;
    


    void OnDestroy()
    {
        if (walletConnect != null)
        {
            walletConnect.OnConnect -= OnConnect;
            //walletConnect.OnDisconnect -= OnDisconnect;
            walletConnect.OnOpened -= OnOpened;
            walletConnect.OnClose -= OnClose;
            walletConnect.OnError -= OnError;
            walletConnect.Dispose();
            walletConnect = null;
        }
    }


    void Start()
    {
        isSessionClosed = false;
    }

    
    void Update()
    {
        if (walletConnect != null)
        {
            walletConnect.DispatchMessageQueue();
        }
    }



    public void SetConnection(ClientMeta metadata, Transform parent)
    {
        walletConnect = new WalletConnect(metadata);
        walletConnectView = parent.GetComponent<WalletConnectView>();

        walletConnect.OnConnect += OnConnect;
        //walletConnect.OnDisconnect += OnDisconnect;
        walletConnect.OnOpened += OnOpened;
        walletConnect.OnClose += OnClose;
        walletConnect.OnError += OnError;
    }


    public async void AsyncWalletConnect()
    {
        try
        {
            await walletConnect.Connect();
        }
        catch (Exception e)
        {
            Debug.Log("An error occurred: " + e.Message);
            Debug.Log(e.StackTrace);
        }
    }


    public async void AsyncWalletDisconnect()
    {
        if ((walletConnect != null) && walletConnect.Connected)
        {
            try
            {
                await walletConnect.Disconnect();
            }
            catch (Exception e)
            {
                Debug.Log("An error occurred: " + e.Message);
                Debug.Log(e.StackTrace);
            }
        }
    }


    private void OnConnect(object walletConnectSenderObject, WalletConnect result)
    {
        Debug.Log("OnConnect Accounts: " + result.Accounts.Length);
        Debug.Log("Account: " + result.Accounts[0]);
        Debug.Log("ChainId: " + result.ChainId);
        walletConnectView.OnConnectedCallback(result.Accounts[0]);
    }


    public string WalletConnectURI
    {
        get { return walletConnect.URI; }
    }


    // Get Client Balance Info MetaMask Wallet

    async void GetClientBalanceInfo()
    {
        await GetClientBalanceTask();
    }

    async Task GetClientBalanceTask()
    {
        web3 = new Web3(walletConnect.CreateProvider("ff985ca9bdea42c1bd647e4ad5ed628d"));

        var balance = await web3.Eth.GetBalance.SendRequestAsync(walletConnect.Accounts[0]);
        var etherAmount = Web3.Convert.FromWei(balance.Value);

        print("hello - : " + etherAmount);
    }


    // Peroform Transaction And Get Transaction Hash Status

    async void TransactionFromClientAccount()
    {
        await TransactionFromClientAccountTask();
    }

    async Task TransactionFromClientAccountTask()
    {
         
        web3 = new Web3(walletConnect.CreateProvider("ff985ca9bdea42c1bd647e4ad5ed628d"));

        long ammount = (long)(0.01f * 1000000000000000000);

        try
        {
            var trx = await web3.Eth.Transactions.SendTransaction.SendRequestAsync(new Nethereum.RPC.Eth.DTOs.TransactionInput()
            {
                From = walletConnect.Accounts[0],
                To = "0x07deef23bA5A2B0D657774B163AaA1352E07e457",
                Gas = new Nethereum.Hex.HexTypes.HexBigInteger(21000),
                GasPrice = new Nethereum.Hex.HexTypes.HexBigInteger(22000000000),
                Nonce = new Nethereum.Hex.HexTypes.HexBigInteger(0),
                Value = new Nethereum.Hex.HexTypes.HexBigInteger(ammount),
                Data = "0x"
            });
             Debug.Log("trx hash " + trx);
            GetTransactionHashStatus(trx);
        }
        catch 
        {
            print("Client Cancelled The Transaction");
        }

        //try
        //{
        //    await walletConnect.Disconnect();
        //}
        //catch (Exception e)
        //{
        //    Debug.Log("An error occurred: " + e.Message);
        //    Debug.Log(e.StackTrace);
        //}

     }

    async void GetTransactionHashStatus(string l_Hash)
    {
        await GetTransactionHashStatusTask(l_Hash);
    }

    async Task GetTransactionHashStatusTask(string l_Hash)
    {
        
        var l_HashStatus = web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(l_Hash);
        await l_HashStatus;
        
        if(l_HashStatus.IsCompleted)
        {
            print("Hash Status - True");
        }
        else if(l_HashStatus.IsCanceled)
        {
            print("Hash Status - Cancled");
        }
        else if(l_HashStatus.IsFaulted)
        {
            print("Hash Status - Faulted");
        }
    }



    public static string ToHexString(string str)
    {
        var sb = new StringBuilder();
        var bytes = Encoding.Unicode.GetBytes(str);

        foreach (var t in bytes)
        {
            sb.Append(t.ToString("X2"));
        }

        return sb.ToString();
    }

    private void CloseSession()
    {
        if (isSessionClosed)
        {
            return;
        }
        Debug.Log("Session Closed");
        isSessionClosed = true;
        walletConnectView.OnDisconnectedCallback();
        Destroy(this.gameObject);
    }
    

    private void OnDisconnect(object walletConnectSenderObject, WalletConnect result)
    {
        Debug.Log("OnDisconnect");
    }

    private void OnOpened(object walletConnectSenderObject, WalletConnect result)
    {
        walletConnectView.OnOpenedCallback();
    }   

    private void OnClose(object walletConnectSenderObject, WalletConnect result)
    {
        CloseSession();
    }

    private void OnError(object walletConnectSenderObject, WalletConnect result)
    {
        CloseSession();
    }










    #region Old Waste Code


    //IEnumerator getbalanceinfocorout8ine()
    //{
    //    var balanceRequest = new EthGetBalanceUnityRequest("ff985ca9bdea42c1bd647e4ad5ed628d");
    //    yield return balanceRequest.SendRequest(walletConnect.Accounts[0], BlockParameter.CreateLatest());

    //    // decimal BalanceAddressTo = UnitConversion.Convert.FromWei(balanceRequest.Result.Value);


    //    Debug.Log("Balance of account:" + balanceRequest.Result.Value);
    //}



    //async void GetBalance1()
    //{
    //    GetBalanceInfo();
    //}

    //IEnumerator GetBalanceInformation()
    //{
    //    var balreq = new EthGetBalanceUnityRequest(walletConnect.Accounts[0]);

    //    yield return balreq.SendRequest(walletConnect.Accounts[0], BlockParameter.CreateLatest());

    //    decimal BalanceAddressTo = UnitConversion.Convert.FromWei(balreq.Result.Value);
    //    //ResultBalanceAddressTo.text = BalanceAddressTo.ToString();

    //    Debug.Log("Balance of account:" + BalanceAddressTo);
    //}

    //async Task GetBalanceInfo()
    //{
    //    print(walletConnect.Accounts[0]);
    //    //var response = await web3.Eth.Sign.SendRequestAsync(walletConnect.Accounts[0], "Here you GO!");
    //    web3 = new Web3(walletConnect.CreateProvider("ff985ca9bdea42c1bd647e4ad5ed628d"));

    //    var balance = web3.Eth.GetBalance.SendRequestAsync(walletConnect.Accounts[0]);
    //    await balance;

    //    print("Balance -  : " + balance.Result.HexValue);
    //}


    //static void customcallback(ErrorResponse e, IAsyncResult result)
    //{
    //    print(result);
    //}

    //void GetDataValue(string takebool)
    //{
    //    print("in continue function");
    //}

    //IEnumerator waitandShowResult()
    //{
    //    Debug.Log("Responce of Sign before =  ");
    //    yield return new WaitForSeconds(20);
    //    Debug.Log("Responce of Sign after =  " + mytask.IsCompleted);
    //}
    //async void SignMthod(string addr)
    //{
    //    MyAccountAddress = addr;
    //    Debug.Log("SingMehtod Start");
    //    web3 = new Web3(walletConnect.CreateProvider("ff985ca9bdea42c1bd647e4ad5ed628d"));
    //    string msg = "nounce Testing";
    //    Debug.Log("got Nounce" + msg);
    //    message = ToHexString(msg);
    //    var ResponceSign = await web3.Eth.Sign.SendRequestAsync(addr, msg);
    //    Debug.Log("nounce sign " + web3.Eth.Sign.SendRequestAsync(addr, msg).IsCompleted);


    //}



    //MyAccountAddress = addr;
    //string msg = "nounce Testing";   
    //Debug.Log("got Nounce  " + msg);
    //message = ToHexString(msg);

    //tasknew = await web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message, hello);
    //   Debug.Log(walletConnect.Accounts[0]);

    //   var response = await web3.Eth.Sign.SendRequestAsync(walletConnect.Accounts[0], "Here you GO!");

    //Debug.Log("sign1 " + response.IsCompleted);
    //Debug.Log("sign2 " + response);
    //Debug.Log("sign3 " + response.IsCanceled);
    //Debug.Log("sign4 " + response.Id);
    //Debug.Log("sign5 " + response.GetHashCode());


    //string msg = "nounce Testing";
    //Debug.Log("got Nounce" + msg);    
    //message = ToHexString(msg);
    //// var New2 = web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).IsCompleted;
    //var res = web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message);
    //yield return res.IsCompleted;
    // var res = web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).Status;


    //IEnumerator CheckRequest()
    //{
    //    yield return new WaitForSeconds(10);
    //    if(StartCheckingSigning)  
    //    is_requestSent = false;
    //}

    //if(StartCheckingSigning)
    //{
    //    if (!is_requestSent)
    //    {

    //        //Debug.Log("nounce sign " + web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).IsCompleted);  
    //        //if (web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).Result != null)
    //        //{
    //        //    print("result is " + web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).Result);
    //        //}
    //        is_requestSent = true;
    //        StartCoroutine(CheckRequest());
    //     }    
    // }

    //public async void AsyncWalletEthTransaction()
    //{
    //    var ipcClient = new Nethereum.JsonRpc.Ipc .Client. ("./geth.ipc");
    //    var web3 = new Nethereum.Web3.Web3(walletConnect.CreateProvider(ipcClient));
    //    //var web3 = new Nethereum.Web3.Web3(walletConnect.CreateProvider("ff985ca9bdea42c1bd647e4ad5ed628d"));
    //    //await web3.Eth.Sign.SendRequestAsync("0x12674123716", "nounce");
    //    //web3.Personal.SignAndSendTransaction.SendRequestAsync()

    //    //var signer1 = new EthereumMessageSigner();
    //    //var signature1 = signer1.EncodeUTF8AndSign("testing ",web3);

    //    //try
    //    //{
    //    //    await web3.Eth.TransactionManager.SendTransactionAsync(new Nethereum.RPC.Eth.DTOs.TransactionInput()
    //    //    {
    //    //        From = walletConnect.Accounts[0],
    //    //        To = walletConnect.Accounts[0],
    //    //        Gas = new Nethereum.Hex.HexTypes.HexBigInteger(21000),
    //    //        GasPrice = new Nethereum.Hex.HexTypes.HexBigInteger(22000000000),
    //    //        Nonce = new Nethereum.Hex.HexTypes.HexBigInteger(0),
    //    //        Value = new Nethereum.Hex.HexTypes.HexBigInteger(0),
    //    //        Data = "0x"
    //    //    });
    //    //}
    //    //catch (Exception e)
    //    //{
    //    //    Console.WriteLine(e);
    //    //}

    //    // web3.eth.sign(msg, accounts[0]);


    //    //await web3.Eth.Transactions.SendTransaction.SendRequestAsync(new Nethereum.RPC.Eth.DTOs.TransactionInput()
    //    //{
    //    //    From = walletConnect.Accounts[0],
    //    //    To = walletConnect.Accounts[0],
    //    //    Gas = new Nethereum.Hex.HexTypes.HexBigInteger(21000),
    //    //    GasPrice = new Nethereum.Hex.HexTypes.HexBigInteger(22000000000),
    //    //    Nonce = new Nethereum.Hex.HexTypes.HexBigInteger(0),
    //    //    Value = new Nethereum.Hex.HexTypes.HexBigInteger(0),
    //    //    Data = "0x"
    //    //});

    //    var response = await web3.Eth.Sign.SendRequestAsync(walletConnect.Accounts[0], "Here you GO!");

    //    var balance = await web3.Eth.GetBalance.SendRequestAsync(walletConnect.Accounts[0]);
    //    Debug.Log($"Balance in Wei: {balance.Value}");

    //    var etherAmount = Web3.Convert.FromWei(balance.Value);
    //    Debug.Log($"Balance in Ether: {etherAmount}");
    //}

    //try
    //{
    //  await web3.Eth.Sign.SendRequestAsync(addr, msg);
    //    {
    //        print("Successful");
    //    };
    //}
    //catch (Exception e)
    //{
    //    Debug.Log("Error " + e);
    //  //  Console.WriteLine(e);
    //}


    // var ResponceSign1 = await Web3.OfflineTransactionSigner.SignTransactionAsync(addr, "nounce testing");// SendRequestAsync(addr);


    //  web3.Eth.Accounts.


    //   WalletConnectView.instance.SignedText.text = "Signed Msg is  =  "+ ResponceSign.ToString();
    // var txCount = await web3.Eth.Transactions.GetTransactionCount.SendRequestAsync(addr);
    //  print(txCount);


    //private void ResponceofSign(  )
    //{
    //    throw new NotImplementedException();
    //}



    //    message = web3.Eth.accounts.hashMessage(message)
    //  var New2 = awa web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message);
    //  ResponceofSign = await web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message);

    //Nethereum.JsonRpc.Client.RpcMessages.RpcResponseMessage rpc;



    // Debug.Log("Debuggin TaskNew --  "+tasknew);
    //  print("mytask " + mytask);   
    //var hash = web3.utils.sha3(message)
    //var accounts = await web3.eth.getAccounts()
    //var signature = await web3.eth.personal.sign(hash, accounts[0])


    // var signature = await  MyContract.web3.eth.sign(prefixedHash, accounts[1]);
    //    print(mytask.IsCompleted);

    //  await web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).ContinueWith((mytask) =>
    //{
    //    if (!mytask.IsCompleted )
    //    {
    //        GetDataValue(mytask.Result);
    //    }
    //});   
    //  StartCoroutine(waitandShowResult());

    //  mytask.IsCompleted

    //    Task<string> t = Task.Run(() = > LongRunningOperation("Continuewith", 500));
    //    t.ContinueWith((t1) = >
    //{
    //        if (t1.IsCompleted && !t1.IsFaulted && !t1.IsCanceled) UpdateUI(t1.Result);
    //    });

    // WalletConnectView.instance.SignedText.text = "Signed Msg before Responce  =  " + ResponceofSign ;


    //else
    //{
    //     WalletConnectView.instance.SignedText.text = "Signed Msg after  =  " + ResponceofSign ;
    //    //  print("result is " + web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).Result);
    //}
    //Debug.Log("nounce sign " + web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).IsCompleted);  
    //if (web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).Result != null)
    //{
    //}


    //if (web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).Result != null)
    //{
    //    print("result is " + web3.Eth.Sign.SendRequestAsync(MyAccountAddress, message).Result);
    //}
    //  StartCheckingSigning = true;

    #endregion
}
