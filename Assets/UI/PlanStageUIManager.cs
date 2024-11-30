using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanStageUIManager : MonoBehaviour
{
    public void OnLaunchButtonClicked()
    {
        SceneManager.LoadScene("TestScene");
    }
}
