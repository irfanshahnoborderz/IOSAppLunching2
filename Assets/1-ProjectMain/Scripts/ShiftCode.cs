using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShiftCode : MonoBehaviour
{
    private bool Show;
    // Start is called before the first frame update
    void Start()
    {
        Show = false;
        this.gameObject.GetComponent<InputField>().contentType = InputField.ContentType.Password;
      //  this.gameObject.GetComponent<InputField>().ActivateInputField();

    }
    public void ToggleShowPassword()
    {
        Show = !Show;
          if(Show)
        {
            this.gameObject.GetComponent<InputField>().contentType = InputField.ContentType.Standard;
            this.gameObject.GetComponent<InputField>().ActivateInputField();

        }
        else
        {        this.gameObject.GetComponent<InputField>().contentType = InputField.ContentType.Password;
            this.gameObject.GetComponent<InputField>().ActivateInputField();

        }
    }

}
