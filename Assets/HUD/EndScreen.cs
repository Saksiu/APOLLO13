using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private RectTransform endScreenRoot;
    private void Start()
    {
        PlayerFuelComponent.OnFuelChanged += HandleFuelChanged;
        endScreenRoot.gameObject.SetActive(false);
        print("hello");
    }

    private void OnDestroy()
    {
        PlayerFuelComponent.OnFuelChanged -= HandleFuelChanged;
    }

    private void HandleFuelChanged(float fuel)
    {
        if (fuel <= 0)
        {
            print("fuel is 0, crashing");
            playerAnimator.SetBool("Crash", true);
            Invoke(nameof(OnCrashAnimEnd), 2.0f);
        }
    }
    private void OnCrashAnimEnd()
    {
        endScreenRoot.gameObject.SetActive(true);
    }

    public void onEndScreenTapped()
    {
        SceneManager.LoadScene("PlanScene");
    }
}
