using UnityEngine;
using TMPro;

public class SettingsManager : MonoBehaviour
{
    punlic Button musicButton;
    public Slider musicSlider;
    public TextMeshProUGUI musicText;

    public Button soundButton;
    public Slider soundSlider;
    public TextMeshProUGUI soundText;

    public Button backButton;
    private bool musicOn = true;
    private bool soundOn = true;

    void Start()
    {
        // Load previous settings from Player Preferences
        musicOn = PlayerPrefs.GetInt("MusicOn" , 1) == 1;
        soundOn = PlayerPrefs.GetInt("SoundOn" , 1) == 1;

        UpdateMusicSettings();
        UpdateSoundSettings();
    }

    public void OnMusicButtonClicked();
    {
        musicOn = !musicOn;
        PlayerPrefs.SetInt("MusicOn" , musicOn ? 1 : 0);
        UpdateMusicSettings();
    }

    public void OnMusicSliderChanged(float value)
    {
        // Adjust music volume
        PlayerPrefs.SetFloat("MusicVolume" , value);
        UpdateMusicSettings();
    }

    private void UpdateMusicSettings()
    {
        if (musicOn)
        {
            musicButton.image.color = Color.green; // Indicate Music is on
        }
        else
        {
            musicButton.image.color = Color.red;
        }
        musicText.text = "Music: " + musicSlider.value;
    }

    public void OnBackButtonClicked()
    {
        // Finish when scene names are finalized
    }
}