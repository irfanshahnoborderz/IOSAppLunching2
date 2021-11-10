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

    public string facebookApp;
    public string facebookAddress;

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
#if UNITY_ANDROID
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
#endif
#if UNITY_IOS
        OpenFacebookPage();
#endif
    }  
    public void OpenFacebookPage()
    {
        float startTime;
        startTime = Time.timeSinceLevelLoad;

        //open the facebook app
        Application.OpenURL(facebookApp);

        if (Time.timeSinceLevelLoad - startTime <= 1f)
        {
            //fail. Open safari.
            Application.OpenURL(facebookAddress);
        }
    }
}
