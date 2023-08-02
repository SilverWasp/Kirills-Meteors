using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreDisplay;
    
    [SerializeField] private GameSaveManager gameSaveManager;
    [SerializeField] private float timeBetween = 0.1f;
    private int score = 0;

    public int Score { get { return score; } set { score = value; } }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "Hall Of Fame")
        {
            gameSaveManager.LoadData();
            scoreDisplay.text = Score.ToString();
        }
    }

    private void OnEnable()
    {
        Meteor.OnLaserHitMeteor += AddScore;
    }

    private void OnDisable()
    {
        Meteor.OnLaserHitMeteor -= AddScore;
    }

    private void AddScore(int points) // the score in gameplay over time
    {   
        StartCoroutine(AddPointsCoroutine(points));
    }

    public void AddFinalScore(float temp) // the score in the win window that adds points based on lives
    {
        StartCoroutine(FinalDataCoRoutine((int)temp));
    }


    private IEnumerator AddPointsCoroutine(int points) // the score in gameplay over time
    {
        int _score = score;
        score += points;
        gameSaveManager.SaveData();
        for (int i = 0; i < points; i++)
        {
            _score++;
            scoreDisplay.text = _score.ToString();
            yield return new WaitForSeconds(timeBetween);
        }
    }

    public IEnumerator FinalDataCoRoutine(int _lives) // the score in the win window that adds points based on lives and adds to display lives 1 life for visual effect
    {
        int _score = score;
        score += (int)_lives * 100;
        gameSaveManager.SaveData();
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < (_lives * 100); i++)
        {
            _score ++;
            GetComponent<StageManager>().WinScoreDisplay.text = _score.ToString();
            yield return new WaitForSeconds(timeBetween/(10*_lives));
        }
        yield return new WaitForSeconds(1.5f);
        if (SceneManager.GetActiveScene().name != "Final Stage")
        {
            GetComponent<StageManager>().WinLivesDisplay.text = (_lives + 1).ToString();
        }
    }
}
