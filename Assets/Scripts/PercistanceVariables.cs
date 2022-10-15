using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PercistanceVariables : MonoBehaviour
{

    public static PercistanceVariables Instance;

    public string nameStored = "John Smith";
    public int highScoreStored;
    private void Awake()
    {

        // Check if there is already an instance of this game object (singleton ---> single instance)
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        // Assign (store the data of) 'This' class to the instance
        Instance = this;
        // Do not destroy this game object when loading or unloading a new scene
        DontDestroyOnLoad(gameObject);

    }

    public void StoreName(string name)
    {
        //nameStored = name;
        //highScoreStored = score;

        SaveData data = new SaveData();

        data.nameStored = name;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Name stored: " + nameStored);
        
    }

    public void StoreHighScore(int score)
    {
        SaveData data = new SaveData();

        data.highScoreStored = score;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

        Debug.Log("Score stored: " + highScoreStored);

    }

}

    [System.Serializable]
    class SaveData
    {
        public string nameStored;
        public int highScoreStored;
    }
