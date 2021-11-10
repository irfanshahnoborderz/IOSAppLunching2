using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 public class Manager2 : MonoBehaviour
{
    public Text argumentTxt;
    private bool focusbool;
    // Start is called before the first frame update
    void Start()
    {
        UpdateArguments();
     }  
    public void UpdateArguments()
    {
#if UNITY_ANDROID
        string arguments = "";
          AndroidJavaClass UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject currentActivity = UnityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
         AndroidJavaObject intent = currentActivity.Call<AndroidJavaObject>("getIntent");
        bool hasExtra = intent.Call<bool>("hasExtra", "arguments");
        if (hasExtra)
        {
            AndroidJavaObject extras = intent.Call<AndroidJavaObject>("getExtras");
            arguments = extras.Call<string>("getString", "arguments");
            argumentTxt.text = arguments;
        }
        else
        {
            argumentTxt.text = "No orguments";
        }  
#endif

#if UNITY_IOS
        
#endif

    }

    private void OnApplicationPause(bool pause)
    {
        //focusbool = pause;
        //if(focusbool)
        //{
        //    UpdateArguments();
        //}     
    }
    private void OnApplicationFocus(bool focus)
    {
        focusbool = focus;
        if (focusbool)
        {
            UpdateArguments();
        }  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
