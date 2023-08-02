using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Newtonsoft.Json;
using UnityEngine.SocialPlatforms.Impl;

public class SaveManager : MonoBehaviour
{
    //Json Newtonsoft
    protected bool encryptData = true;
    protected string encryptKey = "5829";
    protected string saveFolderName = "/Saved_Data_JSONNewtonsoft/";
    [SerializeField] protected ScoreManager scoreM;
    [SerializeField] protected LivesManager livesM;
    

    protected string EncryptDecrypt(string data)
    {
        string newData = "";

        for (int i = 0; i < data.Length; i++)
        {
            newData += (char)(data[i] ^ encryptKey[i % encryptKey.Length]);
        }

        return newData;
    }
}
