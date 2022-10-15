using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUI : MonoBehaviour
{
    public string nameInput;
    public void StartNew()
    {
        // Load Main Scene
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

        // Les '#' servent a garder le code qui est vrai (ou faux), l'autre ne sera pas pris en compte
        #if (UNITY_EDITOR)
        {
            // Quit the game (Unity Editor)
            EditorApplication.ExitPlaymode();
        }

        #else
        {
            // Quit the game (Game Build)
            Application.Quit();
        }
        
        #endif

    }

    public void ReadNameInput(string name)
    {
        nameInput = name;
        Debug.Log(nameInput);
    }
}
