using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.InteropServices;

public class NativeCheck : MonoBehaviour
{
    public class NativeAPI
    {
        [DllImport("__Internal")]
        public static extern string[] getAllXRCloudScenes();
        [DllImport("__Internal")]
        public static extern string[] getAllSmartAssetsForXRCloudScene(string sceneId);
        [DllImport("__Internal")]
        public static extern void loadSmartAsset(string binaryId);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
