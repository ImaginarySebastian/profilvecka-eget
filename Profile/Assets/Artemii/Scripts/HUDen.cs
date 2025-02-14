using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class HUDen : MonoBehaviour
{
    [SerializeField] UIDocument _document;
    VisualElement _healthContainer;
    private VisualElement _curMenu = null;
    float normalTimeScale;
    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InitializeHUD());
    }

    private IEnumerator InitializeHUD()
    {
        yield return new WaitForSeconds(0.1f); // Vänta lite så att GameSession hinner starta

        _curMenu = _document.rootVisualElement.Q<VisualElement>("HUD");
        _healthContainer = _document.rootVisualElement.Q<VisualElement>("HealthContainer");

        if (_healthContainer != null)
        {
            GameSession session = FindObjectOfType<GameSession>();
            if (session != null)
            {
                CreateHearts(_healthContainer, (uint)session.playerLives);
            }
        }
    }
    public void UpdateHearts(int newLives)
    {
        if (_healthContainer != null)
        {
            CreateHearts(_healthContainer, (uint)newLives);
        }
    }
    private void CreateHearts(VisualElement HealthContainer, uint hearts)
    {
        HealthContainer.Clear();
        for(int i = 0; i < hearts; i++)
        {
            VisualElement token = new VisualElement();
            token.AddToClassList("heart");
            HealthContainer.Add(token);
        }
    }
    public void PauseMode()
    {
        if (_curMenu != null)
        {
            _curMenu.style.display = DisplayStyle.None;
        }
        _curMenu = _document.rootVisualElement.Q<VisualElement>("UIVisualTreePaus");
        _curMenu.style.display = DisplayStyle.Flex;
        normalTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        _curMenu.style.display = DisplayStyle.None;
        _curMenu = _document.rootVisualElement.Q<VisualElement>("HUD");
        _curMenu.style.display = DisplayStyle.Flex;
        Time.timeScale = normalTimeScale;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

