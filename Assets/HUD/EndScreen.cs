
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    public static event Action OnEndRun;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private RectTransform endScreenRoot;

    [Header("UI")] 
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI distanceText;
    
    bool alreadyCrashing = false;
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
        if (fuel <= 1f&&!alreadyCrashing)
        {
            alreadyCrashing = true;
            print("fuel is 0, crashing");
            playerAnimator.SetBool("Crash", true);
            Invoke(nameof(OnCrashAnimEnd), 3.0f);
        }
    }
    private void OnCrashAnimEnd()
    {
        coinsText.text = PlayerController.Instance.currentRunCoins.ToString();
        distanceText.text = ((int)EnvManager.Instance.flownDistance).ToString()+" km";
        endScreenRoot.gameObject.SetActive(true);
    }

    public void onEndScreenTapped()
    {
        OnEndRun?.Invoke();
        SceneManager.LoadScene("PlanScene");
    }
}
