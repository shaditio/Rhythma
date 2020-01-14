using UnityEngine;
using UnityEngine.UI;

public class SongOptionSliderController : MonoBehaviour
{
    public Slider slider;
    private Text textSliderValue;

    void Start()
    {
        textSliderValue = GetComponent<Text>();
        slider.value = GlobalStore.speed;
        ShowSliderValue();
    }

    public void ShowSliderValue()
    {
        textSliderValue.text = slider.value.ToString();
    }

    public void updateSpeed(float value){
        GlobalStore.speed = value;
    }
}