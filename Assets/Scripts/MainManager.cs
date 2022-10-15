using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainManager : MonoBehaviour
{
    // Make an instance of this class (we can now call it by 'MainManager.Instance')
    public static MainManager Instance;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    private bool m_Started = false;
    public int m_Points;

    private bool m_GameOver = false;
    public TextMeshProUGUI Score;

    public string bestPlayerName;
    public int bestScore;

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

        Score.text = "High Score: " + bestPlayerName + " - " + bestScore;

    }

    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {

        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    void UpdateScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayerName = data.nameStored;
            bestScore = data.highScoreStored;

            Debug.Log("The name decoded: " + bestPlayerName);
            Debug.Log("The Score decoded: " + bestScore);
        }

        Score.text = "High Score: " + bestPlayerName + " - " + bestScore;
    }

    public void GameOver()
    {
        PercistanceVariables.Instance.StoreHighScore(m_Points);
        UpdateScore();
        m_GameOver = true;
        GameOverText.SetActive(true);
    }

}