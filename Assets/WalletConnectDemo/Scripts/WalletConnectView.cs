using System;
using System.Collections;
using System.Collections.Generic;
//using Nethereum.JsonRpc.UnityClient;
//using Nethereum.StandardTokenEIP20.ContractDefinition;
using Nethereum.Web3;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Contracts;
using UnityEngine;
using System.Numerics;
using System.Threading.Tasks;
using System.Text;
using System.Net;
using WalletConnectSharp.Core;
using WalletConnectSharp.Core.Models;
using UnityEngine.UI;

public class WalletConnectView : MonoBehaviour
{   
    public static WalletConnectView instance;
    private const string WC_SESSION_OBJECT_NAME = "WalletConnectSession";
    private bool waitToDisconnect;
    private string walletPublicKey;
    
    private WalletConnectSession walletConnectSession;

    public WalletLinkView walletLinkView;
    public Button disconnectButton;
    public Button quitButton;
    public TMPro.TextMeshProUGUI walletPublicKeyText;
    public TMPro.TextMeshProUGUI sessionMsgText;  

    [Header("Dapp Metadata")]
    public string dappDescription = "XANA ";
    public string dappIcon = "";
    public string dappName = "XANA Test";
    public string dappUrl = "https://www.xana.com";
    public GameObject SendPublicKeyBtn;    
    public Text SignedText;



     
    void Start()
    {
        instance = this;
        walletConnectSession = null;
    }

    


    public void SetPublicKey(string account)
    {
        walletPublicKey = "Connected Wallet\n" + account;
         PlayerPrefs.SetString("walletPublicKeySigned", account);
         walletPublicKeyText.text = walletPublicKey;
    }  

    public void SetConnectedState(bool status)
    {
        walletPublicKeyText.gameObject.SetActive(status);
        SendPublicKeyBtn.SetActive(status);
        disconnectButton.gameObject.SetActive(status);

        #if !UNITY_WEBGL && !UNITY_EDITOR
        quitButton.gameObject.SetActive(!status);
        #endif
    }

    public void ConnectToWallet()
    {
        if (walletConnectSession != null)
        {
            return;   
        }

        GameObject walletConnectSessionGO = new GameObject(WC_SESSION_OBJECT_NAME);
        walletConnectSessionGO.transform.SetParent(this.transform.parent);
        walletConnectSession = walletConnectSessionGO.AddComponent(typeof(WalletConnectSession)) as WalletConnectSession;

        var metadata = new ClientMeta()
        {
            Description = dappDescription,
            Icons = new[] { dappIcon },
            Name = dappName,
            URL = dappUrl
        };
        walletConnectSession.SetConnection(metadata, this.transform);
        sessionMsgText.text = "Creating Session . . .";
        sessionMsgText.gameObject.SetActive(true);
        //walletLinkView.SetLink(walletConnectSession.WalletConnectURI);
        //walletLinkView.gameObject.SetActive(true);
        walletConnectSession.AsyncWalletConnect();
    }


    public void ResetWalletConnectSession()
    {
        walletConnectSession = null;
    }


    public void DisconnectToWallet()
    {
        if (walletConnectSession != null && !waitToDisconnect)
        {
            waitToDisconnect = true;
            walletConnectSession.AsyncWalletDisconnect();
        }
    }


    public void OnConnectedCallback(string account)
    {
        sessionMsgText.gameObject.SetActive(false);
         SetPublicKey(account);
        SetConnectedState(true);
         walletLinkView.gameObject.SetActive(false);
    }


    public void OnDisconnectedCallback()
    {
        if (walletPublicKeyText.gameObject.activeSelf)
        {
            sessionMsgText.gameObject.SetActive(false);
        }
        else
        {
            sessionMsgText.text = "Try Again!";
            sessionMsgText.gameObject.SetActive(true);
        }

        SetConnectedState(false);
        SetPublicKey("");
        ResetWalletConnectSession();

        waitToDisconnect = false;

        walletLinkView.gameObject.SetActive(false);
    }


    public void OnOpenedCallback()
    {
        sessionMsgText.gameObject.SetActive(false);
        walletLinkView.SetLink(walletConnectSession.WalletConnectURI);
        walletLinkView.gameObject.SetActive(true);
    }


    public void Quit()
    {
        Application.Quit();
    }
}
