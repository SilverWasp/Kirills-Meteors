using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class LivesManager : MonoBehaviour
{
    [SerializeField] private TMP_Text livesDisplay;
    

    [SerializeField] private GameSaveManager gameSaveManager;
    [SerializeField] private StageManager stageM;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerHolder;

    private int lives = 5;

    public int Lives { get { return lives; } set { lives = value; } }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "Hall Of Fame")
        {
            gameSaveManager.LoadData();
            livesDisplay.text = Lives.ToString();
        }

    }

    private void OnEnable()
    {
        Meteor.OnMeteorHitPlayer += DecreseLives;
    }

    private void OnDisable()
    {
        Meteor.OnMeteorHitPlayer -= DecreseLives;
    }

    private void DecreseLives(int damage) // updates lives left, and instantiates player again after being hit by meteor
    {
        lives += damage;
        livesDisplay.text = lives.ToString();
        if (lives <= 0)
        {
            stageM.Lose();
        }
        else if (stageM.GetGameState() == true)
        {

            GameObject newPlayer = Instantiate(player, Vector2.zero, Quaternion.identity);
            newPlayer.transform.SetParent(playerHolder.transform); // player holder in toDisable list in STAGE MANAGER to make sure clone is disabled in win or lose
            newPlayer.SetActive(true);
        }
    }

    public void AddLife() // add a life at the end of the stage if not the final stage in STAGE MANAGER
    {
        lives++;
    }
}
