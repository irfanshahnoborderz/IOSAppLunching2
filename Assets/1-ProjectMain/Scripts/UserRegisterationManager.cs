using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Mail;
using UnityEngine.Networking;
using System;
using UnityEngine.SceneManagement;

public class UserRegisterationManager : MonoBehaviour
{
    public static UserRegisterationManager instance;

    [Header("Total-URL")]
    public string EmailURL;


    [Space(5)]
     [Header("Total-Panal")]
    public GameObject FirstPanal;
    public GameObject EmailPanal;
    public GameObject OTPPanal;
    public GameObject PasswordPanal;
    public GameObject usernamePanal;
    public GameObject LoginPanal;
    public GameObject SignUpPanal;

    [Space(5)]
     [Header("SignUp-InputFields")]

    public InputField EmailInputText;
     public InputField UsernameText;
    [Header("Password-InputFields")]
    public InputField Password1InputText;
    public InputField Password2ConfirmInputText;
    [Header("OTP-InputFields")]
    public List<InputField> pin;

    [Space(5)]
    [Header("Login-InputFields")]
    public InputField LoginEmail;
    public InputField LoginPassword;

    [Space(10)]
    [Header("Buttons Texts GameObjects")]
    public Button EmailSubmitButton;


    [Space(10)]
    [Header("Error Texts GameObjects")]
    public GameObject errorTextEmail;
    public GameObject errorTextPassword;
    public GameObject errorTextNumber;
    public GameObject errorTextName;
    public GameObject errorTextPIN;
    public GameObject errorTextLogin;
    public List<string> myData;
    string Email;
    string password;
    string Username;
    private bool isCheckingOTP;
    string CurrentOTP;
    public bool LoggedIn;

    [Space(10)]
    [Header("wallet Connection Section")]
     public GameObject WalletConnectScreen;
    public GameObject WalletConnectDependent1;
    public GameObject WalletConnectDependent2;
    public GameObject walletLoginFailed;
    public GameObject LoginSuccessPanal;
    public String walletuserName;
    public bool Walletbool;
    public Text ReturnDataFromAPI;

     private void Awake()
    {
        instance = this;
    }
    //          public Text CongratulationText;
    // Start is called before the first frame update
    void Start()
    {

       OpenUIPanal(1);
        //Adds a listener to the main input field and invokes a method when the value changes.
        pin[0].onValueChanged.AddListener(delegate { ValueChangeCheck(0); });
        pin[1].onValueChanged.AddListener(delegate { ValueChangeCheck(1); });
        pin[2].onValueChanged.AddListener(delegate { ValueChangeCheck(2); });
        pin[3].onValueChanged.AddListener(delegate { ValueChangeCheck(3); });
        Walletbool = false;
           //EmailInputText.layoutPriority  = 0;
           //EmailInputText.onEndEdit.AddListener(fieldValue => {
           //    //if (trimWhitespace)
           //    //    _inputField.text = fieldValue = fieldValue.Trim();
           //    //if (Input.GetButton(submitKey))
           //    //    validateAndSubmit(fieldValue);
           //});

        EmailSubmitButton.onClick.AddListener(SubmitEmail);
     }

    public int GetKeyboardSize()
    {
        using (AndroidJavaClass UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            AndroidJavaObject View = UnityClass.GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer").Call<AndroidJavaObject>("getView");

            using (AndroidJavaObject Rct = new AndroidJavaObject("android.graphics.Rect"))
            {
                View.Call("getWindowVisibleDisplayFrame", Rct);

                return Screen.height - Rct.Call<int>("height");
            }
        }
    }  




    // Invoked when the value of the text field changes.
    public void ValueChangeCheck(int value)
    {
        if (value == 0)
        {
            Debug.Log("Value Changed  0");
            print(pin[0].text);
            if (pin[1].text == "")
            {
                pin[1].Select();
                pin[1].ActivateInputField();
            }



        }
        if (value == 1)
        {
            Debug.Log("Value Changed  1");
            print(pin[1].text);
            if (pin[2].text == "")
            {
                pin[2].Select();
                pin[2].ActivateInputField();
            }
        }
        if (value == 2)
        {

            Debug.Log("Value Changed  2");
            if (pin[3].text == "")
            {
                pin[3].Select();
                pin[3].ActivateInputField();
            }
        }
        if (value == 3)
        {
            Debug.Log("Value Changed  3");
        }
    }

    public void OpenUIPanal(int ActivePanalCounter)
    {
        FirstPanal.SetActive(false);
        EmailPanal.SetActive(false);
        OTPPanal.SetActive(false);
        PasswordPanal.SetActive(false);
        usernamePanal.SetActive(false);
        LoginPanal.SetActive(false);
        LoginSuccessPanal.SetActive(false);
        SignUpPanal.SetActive(false);
        WalletConnectScreen.SetActive(false);
        WalletConnectDependent1.SetActive(false);
        WalletConnectDependent2.SetActive(false);
        walletLoginFailed.SetActive(false);
         switch (ActivePanalCounter)
        {
            case 1:
                {
                    FirstPanal.SetActive(true);
                    break;
                }
             case 2:
                {
                    EmailPanal.SetActive(true);
                    EmailInputText.text = "";
                    EmailInputText.Select();
                    pin[0].ActivateInputField();
                     break;
                }
            case 3:
                {
                    OTPPanal.SetActive(true);
                    for (int i = 0; i < pin.Count; i++)
                    {
                        pin[i].text = "";
                    }
                     pin[0].Select();
                    pin[0].ActivateInputField();
                    break;
                }
            case 4:
                {
                    Password1InputText.text = "";
                    Password2ConfirmInputText.text = "";
                    PasswordPanal.SetActive(true);
                    Password1InputText.Select();
                    Password1InputText.ActivateInputField();
                    break;
                }
            case 5:
                {
                    usernamePanal.SetActive(true);
                    UsernameText.Select();
                    UsernameText.ActivateInputField();
                    break;
                }
            case 6:
                {
                    LoginPanal.SetActive(true);
                    LoginEmail.text = "";
                    LoginPassword.text = "";
                    break;
                }
            case 7:
                {
                    LoggedIn = true;
                      LoginSuccessPanal.SetActive(true);
                    break;
                }
            case 8:
                {
                    SignUpPanal.SetActive(true);

                    break;
                }
            case 9:
                {
                    WalletConnectScreen.SetActive(true);
                     break;
                }
            case 10:
                {
                    WalletConnectDependent1.SetActive(true);
                    WalletConnectDependent2.SetActive(true);
                    break;
                }

            case 11:
                {
                    walletLoginFailed.SetActive(true);
                     break;
                }
        }
    }
    
    public void SendWalletCheckAPI()
    {
         if (PlayerPrefs.GetString("walletPublicKeySigned") != "")
        {
            string url = "https://api.xana.net/xanaWeb/signWithAddress";  
            WWWForm form = new WWWForm();
            form.AddField("token", "piyush55");
            form.AddField("owner", PlayerPrefs.GetString("walletPublicKeySigned"));
             var www = new WWW(url, form);
            StartCoroutine(HitWalletAPI(url, form));   
        }
    }
    public void SignUpOfWallet()
    {
        Walletbool = true;
      } 
  
    // [System.Obsolete]
    public void SubmitEmail()
    {
        print(EmailInputText.text);
        if (EmailInputText.text == "")
        {
            errorTextEmail.GetComponent<Animator>().SetBool("playAnim", true);
            errorTextEmail.GetComponent<Text>().text = "Email Field should not be empty";
            StartCoroutine(WaitUntilAnimationFinished(errorTextEmail.GetComponent<Animator>()));
            return;
        }
        else
        {
                print("Email validity is " + IsValidEmail(EmailInputText.text));
                if (IsValidEmail(EmailInputText.text))
                {
                  string email = EmailInputText.text;

                //     string url = "https://api.xana.net/xanaWeb/sendOtp";
                string url = EmailURL;
 
                WWWForm form = new WWWForm();
                    //form.AddField("token", "piyush55");
                    form.AddField("email", email);
                    // var www = new WWW(url, form);
                    //StartCoroutine(WaitForRequestForEmail(www, email));
                    StartCoroutine(HitEmailAPI(url, email, form));
                }  
                else
                {
                    errorTextEmail.GetComponent<Animator>().SetBool("playAnim", true);
                    errorTextEmail.GetComponent<Text>().text = "Please Enter Valid Email";
                    StartCoroutine(WaitUntilAnimationFinished(errorTextEmail.GetComponent<Animator>()));
                }
            }
    }

     bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public void SubmitOTP()
    {
        string OTP = "";
        for (int i = 0; i < pin.Count; i++)
        {
            OTP += pin[i].text;
        }

        print("OTP entered by user is " + OTP);
          if (OTP == "" || OTP.Length < 4)
        {
            errorTextPIN.GetComponent<Animator>().SetBool("playAnim", true);
            errorTextPIN.GetComponent<Text>().text = "OTP Fields should not be empty";
            StartCoroutine(WaitUntilAnimationFinished(errorTextPIN.GetComponent<Animator>()));
            return;
        }
        string url = "https://api.xana.net/xanaWeb/verifyOtp";
        WWWForm form = new WWWForm();
        form.AddField("token", "piyush55");
        form.AddField("email", Email);
        //  int newOTP = int.Parse(OTP);   
        form.AddField("otp", OTP);
        var www = new WWW(url, form);
        StartCoroutine(HitOTPAPI(url, form));
        //  StartCoroutine(WaitForRequestOfOTP(www));
    }
    public void SubmitPassword()
    {
        string pass1 = Password1InputText.text;
        string pass2 = Password2ConfirmInputText.text;
         if (pass1 == "" || pass2 == "")
        {
             print("Password Field should not be empty");
            return;
        }
        if (pass1 == pass2)
        {
            password = pass1;
            OpenUIPanal(5);
            //PasswordPanal.SetActive(false);
            //usernamePanal.SetActive(true);
        }
        else
        {
            print("Password not matched");
        }
    }
    public void SubmitUserNameAndRegisterUser()
    {
        string Localusername = UsernameText.text;
        if (Localusername == "")
        {
            print("Username Field should not be empty");

            errorTextName.GetComponent<Animator>().SetBool("playAnim", true);
            errorTextName.GetComponent<Text>().text = "Username Field should not be empty";
            StartCoroutine(WaitUntilAnimationFinished(errorTextName.GetComponent<Animator>()));
            return;
        }
  
            string url = "https://api.xana.net/xanaWeb/register";
            WWWForm form = new WWWForm();
            form.AddField("token", "piyush55");
            form.AddField("username", Localusername);
            form.AddField("email", Email);
            form.AddField("password", password);
            var www = new WWW(url, form);
            StartCoroutine(RegisterUser(www));
      
    }

    public void SubmitLoginCredentials()
    {
        string L_LoginEmail = LoginEmail.text;
        string L_loginPassword = LoginPassword.text;

        if (L_LoginEmail == "" || L_loginPassword == "")
        {
            print("Email Or Password should not be empty");
            errorTextLogin.GetComponent<Animator>().SetBool("playAnim", true);
            errorTextLogin.GetComponent<Text>().text = "Email Or Password should not be empty";
            StartCoroutine(WaitUntilAnimationFinished(errorTextLogin.GetComponent<Animator>()));
            return;
        }
        string url = "https://api.xana.net/xanaWeb/login";
        WWWForm form = new WWWForm();
        form.AddField("token", "piyush55");
        form.AddField("email", L_LoginEmail);
        form.AddField("password", L_loginPassword);
        var www = new WWW(url, form);
        StartCoroutine(LoginUser(www));
    }

     public IEnumerator HitEmailAPI(string URL, string localEmail, WWWForm form)
    {

        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                //  Debug.Log(www.downloadProgress);
                yield return null;
            }
            if (www.isHttpError || www.isNetworkError)
            {
                Debug.LogError("Network Error");
                errorTextEmail.GetComponent<Animator>().SetBool("playAnim", true);
                StartCoroutine(WaitUntilAnimationFinished(errorTextEmail.GetComponent<Animator>()));
                errorTextEmail.GetComponent<Text>().text = www.error.ToUpper();
                Debug.Log("WWW Error: " + www.error);
            }
            else
            {
                if (operation.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    MyClassNewApi myObject = new MyClassNewApi();
                    myObject = CheckResponceJsonNewApi(www.downloadHandler.text);
                     if (myObject.success == "true")
                    {
                             OpenUIPanal(3);
                            Email = localEmail;
                     }  
                    else
                    {
                        errorTextEmail.GetComponent<Animator>().SetBool("playAnim", true);
                        StartCoroutine(WaitUntilAnimationFinished(errorTextEmail.GetComponent<Animator>()));
                        errorTextEmail.GetComponent<Text>().text = myObject.msg.ToUpper();
                        print("Error Occured " + myObject.msg);
                    }
                }
            }
        }
    }
    [System.Obsolete]
    IEnumerator WaitForRequestOfOTP(WWW www)
    {
        yield return www;
        // check for errors   
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
            MyClass myObject = new MyClass();
            myObject = CheckResponceJson(www.data);
            if (myObject.success == "true")
            {
                OpenUIPanal(7);
            }
            else
            {
                errorTextPIN.GetComponent<Animator>().SetBool("playAnim", true);
                errorTextPIN.GetComponent<Text>().text = myObject.data.ToUpper();
                StartCoroutine(WaitUntilAnimationFinished(errorTextPIN.GetComponent<Animator>()));
            }
        }
        else
        {
            errorTextPIN.GetComponent<Animator>().SetBool("playAnim", true);
            errorTextPIN.GetComponent<Text>().text = www.error.ToUpper();
            StartCoroutine(WaitUntilAnimationFinished(errorTextPIN.GetComponent<Animator>()));
        }
    }



    public IEnumerator HitOTPAPI(string URL, WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                //  Debug.Log(www.downloadProgress);
                yield return null;
            }
            if (www.isHttpError || www.isNetworkError)
            {
                errorTextPIN.GetComponent<Animator>().SetBool("playAnim", true);
                errorTextPIN.GetComponent<Text>().text = www.error.ToUpper();
                StartCoroutine(WaitUntilAnimationFinished(errorTextPIN.GetComponent<Animator>()));
            }
            else
            {
                if (operation.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    MyClass myObject = new MyClass();
                    myObject = CheckResponceJson(www.downloadHandler.text);
                    if (myObject.success == "true")
                    {
                        OpenUIPanal(4);
                    }
                    else
                    {
                        errorTextPIN.GetComponent<Animator>().SetBool("playAnim", true);
                        errorTextPIN.GetComponent<Text>().text = myObject.data.ToUpper();
                        StartCoroutine(WaitUntilAnimationFinished(errorTextPIN.GetComponent<Animator>()));
                    }
                }
            }
        }
    }


    [System.Obsolete]
    IEnumerator RegisterUser(WWW www)
    {
        yield return www;
        // check for errors   
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
            MyClass myObject = new MyClass();
            myObject = CheckResponceJson(www.data);
            if (myObject.success == "true")
            {
                print("User Registered succesfully ");
                OpenUIPanal(6);
            }
            else
            {
                errorTextName.GetComponent<Animator>().SetBool("playAnim", true);
                errorTextName.GetComponent<Text>().text = myObject.data.ToUpper();
                StartCoroutine(WaitUntilAnimationFinished(errorTextName.GetComponent<Animator>()));
                print("Error Occured " + myObject.data);
            }
        }
        else
        {
            errorTextName.GetComponent<Animator>().SetBool("playAnim", true);
            errorTextName.GetComponent<Text>().text = www.error.ToUpper();
            StartCoroutine(WaitUntilAnimationFinished(errorTextName.GetComponent<Animator>()));
            Debug.Log("WWW Error: " + www.error);
        }
    }
    [System.Obsolete]
    IEnumerator LoginUser(WWW www)
    {
        yield return www;
        // check for errors   
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.data);
            MyClass myObject = new MyClass();
            myObject = CheckResponceJson(www.data);
            if (myObject.success == "true")
            {
                print("Congratulations Login Successfully done");
                PlayerPrefs.SetString("LoginToken", myObject.data);
                var parts = myObject.data.Split('.');
                if (parts.Length > 2)
                {
                    var decode = parts[1];
                    var padLength = 4 - decode.Length % 4;
                    if (padLength < 4)
                    {
                        decode += new string('=', padLength);
                    }
                    var bytes = System.Convert.FromBase64String(decode);
                    var userInfo = System.Text.ASCIIEncoding.ASCII.GetString(bytes);
                    print(userInfo);
                    LoginClass L_LoginObject = new LoginClass();
                    L_LoginObject = CheckResponceJsonOfLogin(userInfo);
                    PlayerPrefs.SetString("UserName", L_LoginObject.id);
                    print("Welcome " + PlayerPrefs.GetString("UserName"));
                  //  CongratulationText.text = "Congratulations  \n\n Welcome \n\n" + PlayerPrefs.GetString("UserName");
                    OpenUIPanal(7);
                }
            }
            else
            {
                print("Error Occured ");
                errorTextLogin.GetComponent<Animator>().SetBool("playAnim", true);
                errorTextLogin.GetComponent<Text>().text = myObject.data.ToUpper();
                StartCoroutine(WaitUntilAnimationFinished(errorTextLogin.GetComponent<Animator>()));
            }
        }
        else
        {
            errorTextLogin.GetComponent<Animator>().SetBool("playAnim", true);
            errorTextLogin.GetComponent<Text>().text = www.error.ToUpper();
            StartCoroutine(WaitUntilAnimationFinished(errorTextLogin.GetComponent<Animator>()));
            Debug.Log("WWW Error: " + www.error);
        }
    }





    public IEnumerator HitWalletAPI(string URL, WWWForm form)
    {
         using (UnityWebRequest www = UnityWebRequest.Post(URL, form))
        {
            var operation = www.SendWebRequest();
            while (!operation.isDone)
            {
                //  Debug.Log(www.downloadProgress);
                yield return null;
            }
            if (www.isHttpError || www.isNetworkError)
            {
                errorTextPIN.GetComponent<Animator>().SetBool("playAnim", true);
                errorTextPIN.GetComponent<Text>().text = www.error.ToUpper();
                StartCoroutine(WaitUntilAnimationFinished(errorTextPIN.GetComponent<Animator>()));
            }
            else
            {
                if (operation.isDone)
                {
                    Debug.Log(www.downloadHandler.text);
                    MyClass myObject = new MyClass();
                    myObject = CheckResponceJson(www.downloadHandler.text);
                    if (myObject.success == "true")
                    {
                         OpenUIPanal(7);
                        ReturnDataFromAPI.text = myObject.data;
                    }
                    else
                    {
                        OpenUIPanal(11);
                         //errorTextPIN.GetComponent<Animator>().SetBool("playAnim", true);
                        //errorTextPIN.GetComponent<Text>().text = myObject.data.ToUpper();
                        //StartCoroutine(WaitUntilAnimationFinished(errorTextPIN.GetComponent<Animator>()));
                    }
                }
            }
        }
    }


    [Serializable]
    public class MyClassNewApi
    {
        public MyClassNewApi myObject;
        public string success;
        public string data;
        public string msg;
         public MyClassNewApi Load(string savedData)
        {
            myObject = new MyClassNewApi();
             myObject = JsonUtility.FromJson<MyClassNewApi>(savedData);
            return myObject;
        }
    }


    [Serializable]
    public class MyClass
    {
        public MyClass myObject;
        public string success;
        public string data;
        public MyClass Load(string savedData)
        {
            myObject = new MyClass();

            myObject = JsonUtility.FromJson<MyClass>(savedData);
            return myObject;
        }
    }
    [Serializable]
    public class LoginClass  
    {
        public LoginClass LoginObject;
        public string id;
        public string iat;
        public int exp;
        public LoginClass Load(string savedData)
        {
            LoginObject = new LoginClass();
            LoginObject = JsonUtility.FromJson<LoginClass>(savedData);
            return LoginObject;
        }
    }

    LoginClass CheckResponceJsonOfLogin(string Localdata)
    {
        LoginClass myObject = new LoginClass();
        myObject = myObject.Load(Localdata);
        print("user name in class" + (myObject.id));
        return myObject;
    }

    MyClass CheckResponceJson(string Localdata)
    {
        MyClass myObject = new MyClass();
        myObject = myObject.Load(Localdata);

        return myObject;
    }

    MyClassNewApi CheckResponceJsonNewApi(string Localdata)
    {
        MyClassNewApi myObject = new MyClassNewApi();
        myObject = myObject.Load(Localdata);
         return myObject;
    }



    IEnumerator WaitUntilAnimationFinished(Animator MyAnim)
    {
        yield return new WaitForSeconds(1.5f);
        MyAnim.SetBool("playAnim", false);
    }
}
