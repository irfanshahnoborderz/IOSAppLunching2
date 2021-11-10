using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenFB : MonoBehaviour
{
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
  public  void OpenFacebookPage()
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
