using UnityEngine;
using UnityEngine.UI;

public class BarUpdater : MonoBehaviour
{
    private Slider _barSlider;
    // private LevelData _levelData; //TODO: are we using any Level data?

    private void Awake()
    {
        _barSlider = GetComponentInChildren<Slider>();
        if (_barSlider == null)
        {
            Debug.LogError("No Slider found in children");
            return;
        }

        PlayerFuelComponent.OnFuelChanged += UpdateBar;
    }

    private void UpdateBar(float currValue)
    {
        _barSlider.maxValue = PlayerController.Instance.FuelComponent.initialFuel;
        _barSlider.value = currValue;
    }

    private void OnDestroy()
    {
        PlayerFuelComponent.OnFuelChanged -= UpdateBar;
    }
}