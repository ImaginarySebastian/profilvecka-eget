using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using JetBrains.Annotations;

public class Menu : MonoBehaviour
{
    [SerializeField] UIDocument _document;
    [SerializeField] List<ButtonEvent> _buttonEvents;
    [SerializeField] List<VolumeSlider> _volumeSliders;
    [SerializeField] List<ToggleEvent> _toggleEvents;
    [SerializeField] string startMenu;

    private VisualElement _curMenu = null;
    private DropdownField _difficultyDropdown;

    public void SwitchMenu(string menuName)
    {
        if (_curMenu != null)
        {
            _curMenu.style.display = DisplayStyle.None;
        }
        _curMenu = _document.rootVisualElement.Q<VisualElement>(menuName);
        _curMenu.style.display = DisplayStyle.Flex;
    }

    public void HideMenu()
    {
        if (_curMenu != null)
        {
            _curMenu.style.display = DisplayStyle.None;
        }
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void Quit()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    private void OnEnable()
    {
        _curMenu = _document.rootVisualElement.Q<VisualElement>(startMenu);
        _buttonEvents.ForEach(button => button.Activate(_document));
        _volumeSliders.ForEach(slider => slider.Activete(_document));
        _toggleEvents.ForEach(toggle => toggle.Activete(_document));

        // Hämta och konfigurera svårighets-dropdown
        _difficultyDropdown = _document.rootVisualElement.Q<DropdownField>("DifficultyChoose");
        if (_difficultyDropdown != null)
        {
            Debug.Log("Dropdown hittades!");
            _difficultyDropdown.choices = new List<string> { "Easy", "Medium", "HardCore"};
            _difficultyDropdown.value = PlayerPrefs.GetString("Difficulty", "Medium");
            _difficultyDropdown.RegisterValueChangedCallback(evt => SetDifficulty(evt.newValue));
            string difficulty = PlayerPrefs.GetString("Difficulty", "Medium");
            Debug.Log("Loaded Difficulty: " + difficulty);
        }
    }

    private void OnDisable()
    {
        _buttonEvents.ForEach(button => button.InActivate(_document));
        _volumeSliders.ForEach(slider => slider.InActivate(_document));
        _toggleEvents.ForEach(toggle => toggle.InActivate(_document));
    }

    private void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("Difficulty", difficulty);
        PlayerPrefs.Save();
    }


}

[System.Serializable]
public class ClickChange : UnityEvent<ClickEvent>
{
}

[System.Serializable]
public class BoolChange : UnityEvent<bool>
{
}

[System.Serializable]
public class VolumeSlider
{
    [SerializeField] string _sliderName = "";
    [SerializeField] AudioMixer _audioMixer;
    [SerializeField] string _volumeName = "";
    [SerializeField] ClickChange _clickEvent;
    Slider _slider;

    public void Activete(UIDocument document)
    {
        if (_slider == null)
        {
            _slider = document.rootVisualElement.Q<Slider>(_sliderName);
        }
        float startVol = 0;
        _audioMixer.GetFloat(_volumeName, out startVol);
        _slider.value = startVol + 80;
        _slider.RegisterCallback<ChangeEvent<float>>(evt => _audioMixer.SetFloat(_volumeName, evt.newValue - 80));
        _slider.RegisterCallback<ClickEvent>(_clickEvent.Invoke);
    }

    public void InActivate(UIDocument document)
    {
        _slider.UnregisterCallback<ChangeEvent<float>>(evt => _audioMixer.SetFloat(_volumeName, evt.newValue - 80));
    }
}

[System.Serializable]
public class ToggleEvent
{
    [SerializeField] string _toggleName = "";
    [SerializeField] BoolChange _boolEvent;

    Toggle _toggle;

    public void Activete(UIDocument document)
    {
        if (_toggle == null)
        {
            _toggle = document.rootVisualElement.Q<Toggle>(_toggleName);
        }
        _toggle.RegisterCallback<ChangeEvent<bool>>(evt => _boolEvent.Invoke(evt.newValue));
    }

    public void InActivate(UIDocument document)
    {
        _toggle.UnregisterCallback<ChangeEvent<bool>>(evt => _boolEvent.Invoke(evt.newValue));
    }
}

[System.Serializable]
public class ButtonEvent
{
    [SerializeField] string _buttonName = "";
    [SerializeField] UnityEvent unityEvent;
    Button button;

    public void Activate(UIDocument document)
    {
        if (button == null)
        {
            button = document.rootVisualElement.Q<Button>(_buttonName);
        }
        button.clicked += unityEvent.Invoke;
    }

    public void InActivate(UIDocument document)
    {
        button.clicked -= unityEvent.Invoke;
    }
}
