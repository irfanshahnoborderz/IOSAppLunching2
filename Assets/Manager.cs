using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Manager : MonoBehaviour
{
    public string AppUrl;
    public string bundleId;
    public InputField Field;
    bool fail = false;


    // Start is called before the first frame update
    void Start()
    {
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenApp()
    {
        LaunchApp2();
    }
      
    public void LaunchApp2()
    {
        bool fail = false;  
        //string message = "PLayer name " + "_" + "   Email  ";
         string message = Field.text;  
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
        AndroidJavaObject launchIntent = null;

        try
        {
            launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);
           // launchIntent.Call<AndroidJavaObject>("putExtra", bundleId + "arguments", message);
            launchIntent.Call<AndroidJavaObject>("putExtra", "arguments", message);
         }      
        catch (System.Exception e)
        {
            fail = true;
        }

        if (fail)
        {
            Debug.Log("app not found");
        }
        else
        {
            ca.Call("startActivity", launchIntent);
        }
        up.Dispose();
        ca.Dispose();
        packageManager.Dispose();
        launchIntent.Dispose();
    }  
    /*
    public void launchApp()
    {
        AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.intent.action.VIEW");

        string arg1 = Random.RandomRange(50, 100).ToString();  
        AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
        AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");  
        AndroidJavaObject launchIntent = null;
         try   
        {
             launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleId);  
          }        
        catch (System.Exception e)
        {   
            fail = true;
        }    

        if (fail)
        { //open app in store
            Application.OpenURL(AppUrl);
        }
        else //open the app
            ca.Call("startActivity", launchIntent);     
    }   
    */
}
