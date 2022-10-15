using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
 public string playerName;
    public TMP_InputField inputField;
    public TMP_Text highscoreText;
    public Button playButton;

    public void Start()
    {
        playButton.interactable = false;
        PercistanceVariables.Instance.LoadGameRank();
        highscoreText.text = $"Highscore - {PercistanceVariables.Instance.bestPlayer.ToUpper()}: {PercistanceVariables.Instance.bestScore}";
    }

    public void SaveText()
    {
        playButton.interactable = true;
        playerName = inputField.text;
        PercistanceVariables.Instance.currentPlayer = playerName;
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }

}