using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{

    [SerializeField] private float stageDuration = 120f;
    private bool wonCondition = false; // did the player win
    private float timeLeft;
    [SerializeField] private TMP_Text timeDisplay;
    [SerializeField] private StageManager stageM;

    private void Awake()
    {
        if(stageM.Test == false)
        {
            timeLeft = stageDuration;
        }
        else
        {
            timeLeft = 10;
        }
            
    }
    private void Update()
    {
        if(SceneManager.GetActiveScene().name != "Hall Of Fame" && SceneManager.GetActiveScene().name != "Main Menu") // only if in stages
        {
            if (timeLeft >= 0)
            {
                timeLeft -= Time.deltaTime;
                int minuts = (int)(UnityEngine.Mathf.FloorToInt(timeLeft / 60)); // why isnt there a modulu fuction
                int seconds = (int)(timeLeft % 60f); //this is not modulu, its the remainder operation

                timeDisplay.text = string.Format("{0:0}:{1:00}", minuts, seconds);
            }
            else if (!wonCondition && stageM.GetGameState()) // player didnt win yet and is still playing
            {
                wonCondition = true;
                stageM.Win();
            }
        }
        
    }


}
