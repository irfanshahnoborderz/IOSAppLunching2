using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.UI;
class GetMyOjbectiveCUnityPlugin : MonoBehaviour
{
    public Text AvailableFB;
#if UNITY_IOS
    // On iOS and Xbox 360 plugins are statically linked into
    // the executable, so we have to use __Internal as the
    // library name.
    [DllImport("__Internal")]
    private static extern int isFBInstalled();
    [DllImport("__Internal")]
    private static extern int isYelpInstalled();
#else
    // Other platforms load plugins dynamically, so pass the name
    // of the plugin's dynamic library.
    [DllImport("PluginName")]
#endif

    public static int WegotFBApp()
    {
        int FBStatus = 0; //Assign value we recieve to this

        // Calls the isFBInstalled function inside the plugin
        FBStatus = isFBInstalled(); //returns the status of FB install in ObjC plugin

        return FBStatus; // 0 is No, 1 is Yes
    }

    public static int WegotYelpApp()
    {
        int YelpStatus = 0; //Assign value we recieve to this

        // Calls the isFBInstalled function inside the plugin
        YelpStatus = isYelpInstalled(); //returns the status of FB install in ObjC plugin

        return YelpStatus;  // 0 is No, 1 is Yes

    }
    public void TestFB()
    {
        int testfb = GetMyOjbectiveCUnityPlugin.WegotFBApp(); //0 is No, 1 yes
         AvailableFB.text = "available is =  " + testfb.ToString();
       //  int testyelp = GetMyOjbectiveCUnityPlugin().WegotYelpApp(); //0 is No, 1 yes
    }

}
