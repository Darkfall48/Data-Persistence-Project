using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PercistanceVariables : MonoBehaviour
{

    public static PercistanceVariables Instance;

    public string currentPlayer;
    public int bestScore;
    public string bestPlayer;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class SaveData
    {
        public int savedScore;
        public string savedPlayer;
    }

    public void SaveGameRank(string bestPlayer, int bestScore)
    {
        SaveData data = new SaveData();

        data.savedPlayer = bestPlayer;
        data.savedScore = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGameRank()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            bestPlayer = data.savedPlayer;
            bestScore = data.savedScore;
        }
    }
}
