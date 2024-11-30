using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartGameButtonClicked()
    {
        SceneManager.LoadScene("TestScene");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit(0);
    }
}
