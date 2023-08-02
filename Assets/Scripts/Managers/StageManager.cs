using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class StageManager : MonoBehaviour
{
    [SerializeField] private bool test = false;
    [SerializeField] private string nextStage;
    [SerializeField] private GameObject[] toDisable;
    private string lastStage;
    private bool gameState; // if win/lose then false if playing then true

    [Header("Saving System")]
    [SerializeField] private GameSaveManager gameSaveManager;
    [SerializeField] private HOFManager HOFM;

    [Header("Win")]
    [SerializeField] [Tooltip("Win window")] private GameObject win;
    [SerializeField] private TMP_Text winLivesDisplay;
    [SerializeField] private TMP_Text winScoreDisplay;
    [SerializeField] private TMP_Text winPointsAddDisplay;

    [Header("Lose")]
    [SerializeField] [Tooltip("Win window")] private GameObject lose;
    [SerializeField] private TMP_Text loseScoreDisplay;

    [Header("Hall Of Fame")]
    [SerializeField] private GameObject playerDataPrefab; // prefab of the players data at the end of the game that is displayed in a container as alist of up to 10 prefabs
    [SerializeField] private Transform playerDataTransform; //container object that keeps a list o up to 10 prefabs fo player data and keeps a constant an cleen look to it

   
    private string stage;

    public string Stage { get { return stage; } set { stage = value; } }
    public bool Test { get { return test; } set { test = value; } }


    public TMP_Text WinLivesDisplay { get { return winLivesDisplay; } set { winLivesDisplay = value; } }
    public TMP_Text WinScoreDisplay { get { return winScoreDisplay; } set { winScoreDisplay = value; } }

    private void Awake()
    {
        gameState = true;
        if (win != null && lose != null)
        {
            win.SetActive(false);
            lose.SetActive(false);
        }
        else if (lose != null)
        {
            lose.SetActive(false);
        }
        
    }
    private void Start()
    {
        if(SceneManager.GetActiveScene().name != "Main Menu" && SceneManager.GetActiveScene().name != "Hall Of Fame") 
        {
            gameState = true;
            for (int i = 0; i < toDisable.Length; i++)
            {
                toDisable[i].SetActive(true);
            }
            gameSaveManager.LoadData();
            lastStage = stage;
            stage = SceneManager.GetActiveScene().name;
            
        }
        else if(SceneManager.GetActiveScene().name == "Hall Of Fame")
        {
            HOFM.LoadData();
            for(int i = 0; i < HOFM.HOF.HallList.Count; i++)
            {
                GameObject cell = Instantiate(playerDataPrefab,playerDataTransform);
                cell.transform.GetChild(0).GetComponent<TMP_Text>().text = (i+1).ToString();
                cell.transform.GetChild(1).GetComponent<TMP_Text>().text = HOFM.HOF.HallList[i].Name;
                cell.transform.GetChild(2).GetComponent<TMP_Text>().text = HOFM.HOF.HallList[i].Points.ToString();
                cell.transform.GetChild(3).GetComponent<TMP_Text>().text = (HOFM.HOF.HallList[i].Success) ? "Won":"Failed";
            }

        }
        Time.timeScale = 1.0f;
    }
    public bool GetGameState()
    {
        return gameState;
    }

    public void GoToNextStage()
    {
        gameSaveManager.SaveData();
        SceneManager.LoadScene(nextStage);
    }

    public void NewCampaign()
    {
        gameSaveManager.ResetData();
        SceneManager.LoadScene("Stage 1");
    }

    public void ContinueLastCamaign()
    {
        if (lastStage != null)
        {
            SceneManager.LoadScene(lastStage);
        }
    }

    public void Lose()
    {
        gameState = false;
        gameSaveManager.SaveData();
        Vector2 temp = gameSaveManager.ReturnData();
        loseScoreDisplay.text = temp.x.ToString(); //score display gets the score value saved in vect 2 as X
        foreach (GameObject element in toDisable)
        {
            element.SetActive(false);
            
        }
        //Time.timeScale = 0;
        lose.SetActive(true);
    }

    public void GoToHOF() 
    {
        HOFM.SaveData();
        SceneManager.LoadScene("Hall Of Fame");
    }

    public void Win()
    {
        gameState = false;
        for (int i = 0; i < toDisable.Length; i++)
        {
            toDisable[i].SetActive(false);
        }
        gameSaveManager.SaveData();

        Vector2 temp = gameSaveManager.ReturnData();
        winScoreDisplay.text = temp.x.ToString(); //score display gets the score value saved in vect 2 as X
        winLivesDisplay.text = temp.y.ToString(); //lives display gets the lives value saved in vect 2 as Y
        winPointsAddDisplay.text = "+" + (temp.y * 100).ToString(); //adds 100 points for each life you didnt lose
                     
        win.SetActive(true);
        GetComponent<ScoreManager>().AddFinalScore(temp.y);
        if (SceneManager.GetActiveScene().name != "Final Stage")
        {
            GetComponent<LivesManager>().AddLife();
        }
        gameSaveManager.SaveData();
        Debug.Log("saved");
    }
    public void ExitToMainMenu()
    {
        gameSaveManager.SaveData();
        SceneManager.LoadScene("Main Menu");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }

    
}
