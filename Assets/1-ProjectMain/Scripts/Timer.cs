using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour
{
    private float timeRemaining = 100;
    public float TotalTimer;
    public bool timerIsRunning = false;
    private Text timerText;
    private void Start()
    {
        timeRemaining = TotalTimer;
         // Starts the timer automatically
        timerIsRunning = true;
       // timerText
    }

    private void OnEnable()
    {
        timeRemaining = TotalTimer;
        // Starts the timer automatically
        timerIsRunning = true;
        // timerText
    }  
    public void ResetTimer()
    {
        timeRemaining = TotalTimer;
     }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                float minutes = Mathf.FloorToInt(timeRemaining / 60);
                float seconds = Mathf.FloorToInt(timeRemaining % 60);
                  this.GetComponent<Text>().text = minutes.ToString () + ":" + seconds.ToString() ; 
            }
            else   
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                this.GetComponent<Text>().text = timeRemaining.ToString();
                timerIsRunning = false;
            }
        }
    }
}