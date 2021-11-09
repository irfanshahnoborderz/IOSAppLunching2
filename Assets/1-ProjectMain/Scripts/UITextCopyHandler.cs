using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UITextCopyHandler : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{

    public GameObject m_CopyBtn;

    public void OnPointerDown(PointerEventData eventData)
    {
        m_CopyBtn.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //m_CopyBtn.SetActive(false);
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}
