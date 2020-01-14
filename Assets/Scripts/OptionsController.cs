using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    public Text musicVolumeValue;
    public Text sfxVolumeValue;
    public Toggle fogToggle;
    public void Start()
    {
        musicVolumeSlider.value = GlobalStore.songVolume * 100;
        sfxVolumeSlider.value = GlobalStore.sfxVolume * 100;
        showSfxSliderValue();
        showMusicSliderValue();
        showFogToggleValue();

    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void showMusicSliderValue()
    {
        musicVolumeValue.text = musicVolumeSlider.value.ToString();
    }

    public void showSfxSliderValue()
    {
        sfxVolumeValue.text = sfxVolumeSlider.value.ToString();
    }

    public void showFogToggleValue()
    {
        fogToggle.isOn = GlobalStore.enableFog;
    }

    public void changeMusicVolume(float value)
    {
        GlobalStore.songVolume = value / 100;
    }

    public void changeSfxVolume(float value)
    {
        GlobalStore.sfxVolume = value / 100;
    }

    public void toggleFog(bool value)
    {
        GlobalStore.enableFog = value;
    }
}