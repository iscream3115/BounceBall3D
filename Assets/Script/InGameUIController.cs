using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class InGameUIController : MonoBehaviour
{

    [SerializeField] private AudioMixer masterMixer;

    private Label pointLabel;
    private VisualElement pauseMenu, settingsMenu;
    private Button pauseButton, retryButton, volumeButton, mainMenuButton,PauseExitButton,SettingsExitButton;

    private Slider volumeSlider,BGMSlider;


    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        pointLabel = root.Q<Label>("PointText");

        pauseMenu = root.Q<VisualElement>("PauseMenu");
        settingsMenu = root.Q<VisualElement>("SettingsMenu");

        pauseButton = root.Q<Button>("PauseButton");
        retryButton = root.Q<Button>("RetryButton");
        volumeButton = root.Q<Button>("VolumeButton");
        mainMenuButton = root.Q<Button>("MainMenuButton");
        PauseExitButton = root.Q<Button>("PauseExitButton");
        SettingsExitButton = root.Q<Button>("SettingsExitButton");

        volumeSlider = root.Q<Slider>("SFXSlider");
        //BGMSlider = root.Q<Slider>("BGMSlider");

        pauseButton.clicked += EnterPauseMenu;
        retryButton.clicked += RetryScene;
        volumeButton.clicked += OpenVolumeMenu; // 구현은 따로
        mainMenuButton.clicked += GoToMainMenu;
        PauseExitButton.clicked += ExitPauseMenu;
        SettingsExitButton.clicked += ExitVolumeMenu;

        volumeSlider.RegisterValueChangedCallback(OnVolumeChanged);
        //BGMSlider.RegisterValueChangedCallback(OnVolumeChanged);

        pauseMenu.style.display = DisplayStyle.None;
        settingsMenu.style.display = DisplayStyle.None;
    }

    private void EnterPauseMenu()
    {
        if (pauseMenu.style.display == DisplayStyle.None)
            pauseMenu.style.display = DisplayStyle.Flex;
    }

    private void ExitPauseMenu()
    {
        if (pauseMenu.style.display == DisplayStyle.Flex)
            pauseMenu.style.display = DisplayStyle.None;
    }

    private void RetryScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // 너가 만든 메인 메뉴 씬 이름
    }

    private void OpenVolumeMenu()
    {
        if (settingsMenu.style.display == DisplayStyle.None)
            settingsMenu.style.display = DisplayStyle.Flex;
        if (pauseMenu.style.display == DisplayStyle.Flex)
            pauseMenu.style.display = DisplayStyle.None;
    }

    private void OnVolumeChanged(ChangeEvent<float> evt)
    {
        masterMixer.SetFloat("MasterVolume", evt.newValue);
    }

    private void ExitVolumeMenu()
    {
        if (settingsMenu.style.display == DisplayStyle.Flex)
            settingsMenu.style.display = DisplayStyle.None;

    }

    void Update()
    {
        if (pointLabel != null)
        {
            pointLabel.text = $"{GameManageScript.instance.GetCurrentScore()} / {GameManageScript.instance.GetTotalPoints()}";
        }
        else
        {
            Debug.Log("Point Label is not found");
        }
    }
}
