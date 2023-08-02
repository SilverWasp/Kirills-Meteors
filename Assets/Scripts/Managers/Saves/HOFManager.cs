using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HOFManager : SaveManager
{
    //Json Newtonsoft
    
    private string saveFileName = "HallOfFame.sav";
    private HallOfFameData _HOF;
    [SerializeField] private TMP_InputField playerName;
    [Header("Final stage only")]
    [SerializeField] private TMP_InputField winPlayerName;
    

    public HallOfFameData HOF { get { return _HOF; } set { _HOF = value; } }

    public void SaveData()
    {
        string path = $"{Application.persistentDataPath}/{saveFolderName}/{saveFileName}";
        // makes sure that if a player chosses a blank name when wining or losing it will still write his score but if there is a name it findes it from both input fields
        HallOfFamePlayerData data = new HallOfFamePlayerData(playerName.text == ""? (winPlayerName.text == ""? playerName.text : winPlayerName.text) : playerName.text, scoreM.Score, livesM.Lives > 0);
        if (File.Exists(path))
        {
            LoadData();
        }
        else
        {
            _HOF = new HallOfFameData();
        }
        _HOF.AddToListHOF(data); 
        Directory.CreateDirectory(Path.GetDirectoryName(path));

        string dataToSave = JsonConvert.SerializeObject(_HOF, Formatting.Indented);

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

        _HOF = JsonConvert.DeserializeObject<HallOfFameData>(dataToLoad);
    }
}
