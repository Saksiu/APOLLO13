using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanStageUIManager : MonoBehaviour
{
    [SerializeField] private Animator rocketAnimator;
    public void OnLaunchButtonClicked()
    {
        rocketAnimator.SetBool("Launch", true);
        Invoke(nameof(OnLaunchAnimEnd),2.5f);
    }

    public void OnMainMEnuButtonClicked()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnLaunchAnimEnd()
    {
        SceneManager.LoadScene("TestScene");
    }
}
