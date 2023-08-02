using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSaveManager : SaveManager
{
    //Json Newtonsoft
    [SerializeField] protected StageManager stageM;
    private string saveFileName = "save2_json_u.sav";
    

    public void SaveData()
    {
        string path = $"{Application.persistentDataPath}/{saveFolderName}/{saveFileName}";
        PlayerSavedData data = new PlayerSavedData(scoreM.Score, livesM.Lives, stageM.Stage);
        Directory.CreateDirectory(Path.GetDirectoryName(path));

        string dataToSave = JsonConvert.SerializeObject(data, Formatting.Indented);

        if (encryptData)
        {
            dataToSave = EncryptDecrypt(dataToSave);
        }

        File.WriteAllText(path, dataToSave);
    }

    public void LoadData()
    {
        string path = $"{Application.persistentDataPath}/{saveFolderName}/{saveFileName}";

        string dataToLoad = File.ReadAllText(path);

        if (encryptData)
        {
            dataToLoad = EncryptDecrypt(dataToLoad);
        }

        PlayerSavedData data = JsonConvert.DeserializeObject<PlayerSavedData>(dataToLoad);

        scoreM.Score = data.score;
        livesM.Lives = data.lives;
        stageM.Stage = data.stage;
    }

    public Vector2 ReturnData()
    {
        string path = $"{Application.persistentDataPath}/{saveFolderName}/{saveFileName}";

        string dataToLoad = File.ReadAllText(path);

        if (encryptData)
        {
            dataToLoad = EncryptDecrypt(dataToLoad);
        }

        PlayerSavedData data = JsonConvert.DeserializeObject<PlayerSavedData>(dataToLoad);

        return new Vector2(data.score,data.lives);
    }

    public void ResetData()
    {
        string path = $"{Application.persistentDataPath}/{saveFolderName}/{saveFileName}";
        PlayerSavedData data = new PlayerSavedData();
        Debug.Log(path);
        Directory.CreateDirectory(Path.GetDirectoryName(path));

        string dataToSave = JsonConvert.SerializeObject(data, Formatting.Indented);

        if (encryptData)
        {
            dataToSave = EncryptDecrypt(dataToSave);
        }

        File.WriteAllText(path, dataToSave);
    }
}
