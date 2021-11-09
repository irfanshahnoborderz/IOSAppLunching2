using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Nethereum.Web3;
using System.Threading.Tasks;

 using System;
 using Nethereum.Web3.Accounts;
using Nethereum.Web3.Accounts.Managed;
using Nethereum.Hex.HexTypes;
using Nethereum.HdWallet;


public class NewBehaviourScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        await GetAccountBalance(); 
         print("After");
     }    

    // Update is called once per frame
    void Update()
    {
        
    }

    static async Task SendTransection()
    {
        var privateKey = "0xb5b1870957d373ef0eeffecc6e4812c0fd08f554b37b233526acc331bf1544f7";
        var account = new Account(privateKey);
        var web3 = new Web3(account);
        var toAddress = "0x13f022d72158410433cbd66f5dd8bf6d2d129924";
        var transaction = await web3.Eth.GetEtherTransferService()
                        .TransferEtherAndWaitForReceiptAsync(toAddress, 1.11m);
     
    }

    static async Task GetAccountBalance()
    {
        var web3 = new Web3("https://mainnet.infura.io/v3/1uQWrVj3kr1yGDp29vcqMF7tSSr");
        var balance = await web3.Eth.GetBalance.SendRequestAsync("0xde0b295669a9fd93d5f28d9ec85e40f4cb697bae");
        Debug.Log($"Balance in Wei: {balance.Value}");
         var etherAmount = Web3.Convert.FromWei(balance.Value);
        Debug.Log($"Balance in Ether: {etherAmount}"); 
    }
}
