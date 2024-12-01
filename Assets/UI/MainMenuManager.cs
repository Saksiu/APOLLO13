using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainMenuCanvasGroup;
    [SerializeField] private CanvasGroup creditsCanvasGroup;
    [SerializeField] private CanvasGroup howToPlayCanvasGroup;
    public void OnStartGameButtonClicked()
    {
        SceneManager.LoadScene("PlanScene");
    }

    public void OnExitButtonClicked()
    {
        Application.Quit(0);
    }
    public void OnCreditsButtonClicked()
    {
        mainMenuCanvasGroup.alpha = 0;
        mainMenuCanvasGroup.interactable = false;
        mainMenuCanvasGroup.blocksRaycasts = false;
        
        creditsCanvasGroup.alpha = 1;
        creditsCanvasGroup.interactable = true;
        creditsCanvasGroup.blocksRaycasts = true;
    }
    public void OnHowToPlayButtonClicked()
    {
        mainMenuCanvasGroup.alpha = 0;
        mainMenuCanvasGroup.interactable = false;
        mainMenuCanvasGroup.blocksRaycasts = false;
        
        howToPlayCanvasGroup.alpha = 1;
        howToPlayCanvasGroup.interactable = true;
        howToPlayCanvasGroup.blocksRaycasts = true;
    }
    public void onReturnToMainMenuButtonClicked()
    {
        mainMenuCanvasGroup.alpha = 1;
        mainMenuCanvasGroup.interactable = true;
        mainMenuCanvasGroup.blocksRaycasts = true;
        
        creditsCanvasGroup.alpha = 0;
        creditsCanvasGroup.interactable = false;
        creditsCanvasGroup.blocksRaycasts = false;
        
        howToPlayCanvasGroup.alpha = 0;
        howToPlayCanvasGroup.interactable = false;
        howToPlayCanvasGroup.blocksRaycasts = false;
    }
}
