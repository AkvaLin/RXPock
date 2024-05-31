using UniRx;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private Button musicButton;
    [SerializeField] private Button soundsButton;
    [SerializeField] private Button playButton;
    private AudioController audioController;
    
    void Start()
    {
        playButton.onClick.AddListener(Play);
        musicButton.onClick.AddListener(ToggleMusic);
        soundsButton.onClick.AddListener(ToggleSound);

        audioController = FindObjectOfType<AudioController>();
        
        audioController.Refresh();
        
        audioController.isMusicEnabled.Subscribe(flag =>
        {
            if (flag)
            {
                musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disable music";
            }
            else
            {
                musicButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enable music";
            }
        });

        audioController.isSoundsEnabled.Subscribe(flag =>
        {
            if (flag)
            {
                soundsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Disable sounds";
            }
            else
            {
                soundsButton.GetComponentInChildren<TextMeshProUGUI>().text = "Enable sound";
            }
        });
    }

    void Play()
    {
        audioController.isMusicEnabled.Dispose();
        audioController.isSoundsEnabled.Dispose();

        SceneManager.LoadScene("GameScene");
    }

    void ToggleMusic()
    {
        audioController.isMusicEnabled.Value = !audioController.isMusicEnabled.Value;
    }

    void ToggleSound()
    {
        audioController.isSoundsEnabled.Value = !audioController.isSoundsEnabled.Value;
    }
}
