namespace snow.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System.Collections.Generic;
    using TMPro;
    using Unity.Netcode;
    using Unity.VisualScripting;

    public class MainMenuManager : MonoBehaviour
    {
        public static MainMenuManager Singleton;
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform _settingsContainer;
        [SerializeField] private GameObject _defaultSelectable;
        [SerializeField] private RectTransform _settingsArrow;
        [SerializeField] private List<Button> _settingsButtons;
        [SerializeField] private List<Selector> _selectors;
        [SerializeField] private Button _discardButton;
        [SerializeField] private Button _confirmButton;
        private AudioSource _soundSource;
        public string Nickname;
        public string Code;

        // Y coordinates of the settings arrow depending on the settings category
        private Dictionary<string, int> _arrowPositions = new()
        {
            {"game", 50},
            {"graphics", -50},
            {"audio", -150},
            {"controls", -250}
        };

        private void Start()
        {
            _soundSource = gameObject.transform.GetChild(1).GetComponent<AudioSource>();

            if (Singleton == null) Singleton = this;

            SettingsManager.Initialize();

            Button[] buttons = FindObjectsOfType<Button>();
            foreach (var b in buttons)
                b.AddComponent<CustomButton>();
            
            TMP_InputField[] inputFields = FindObjectsOfType<TMP_InputField>();
            foreach (var i in inputFields)
                i.AddComponent<CustomInputField>();
        }

        private void Update()
        {
            // Mouse navigation
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                PointerEventData pointerData = new(EventSystem.current)
                {
                    position = Input.mousePosition
                };
                List<RaycastResult> results = new();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (var result in results)
                {
                    Button button = result.gameObject.GetComponentInParent<Button>();
                    TMP_InputField inputField = result.gameObject.GetComponentInParent<TMP_InputField>();
                    if (button != null && button.interactable)
                    {
                        EventSystem.current.SetSelectedGameObject(button.gameObject);
                        break;
                    }
                    else if (inputField != null)
                    {
                        EventSystem.current.SetSelectedGameObject(inputField.gameObject);
                        break;
                    }
                    else if (result.gameObject.tag == "UI Background")
                        break;
                }
            }
            // Keyboard navigation
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
                if (EventSystem.current.currentSelectedGameObject == null)
                    EventSystem.current.SetSelectedGameObject(_defaultSelectable);
        }

        // Play the animation of tab switch
        public void TabChange(string animationName)
        {
            _anim.Play(animationName);
        }

        // Reset UI selection on tab switch
        public void TabSelect(GameObject defaultSelectable)
        {
            EventSystem.current.SetSelectedGameObject(null);
            _defaultSelectable = defaultSelectable;
        }

        public void ExecuteExit()
        {
            Application.Quit();
        }

        public void SelectSettingsCategory(string category)
        {
            // Deactivate all _settingsContainer children and reactivate the current category
            for (int i = 0; i < _settingsContainer.childCount; i++)
            {
                GameObject child = _settingsContainer.GetChild(i).gameObject;
                child.SetActive(category == child.GetComponent<SettingsTab>().Tag);
            }
            // Change _settingsArrow position accordingly to the selected settings category
            _settingsArrow.anchoredPosition = new Vector2(_settingsArrow.anchoredPosition.x, _arrowPositions[category]);
        }

        // Dynamic navigation change of buttons in the settings tabs
        public void ChangeSettingsNavigation(Button navigationPoint)
        {
            Navigation nav = _discardButton.navigation;
            nav.selectOnDown = navigationPoint;
            _discardButton.navigation = nav;
            nav = _confirmButton.navigation;
            nav.selectOnDown = navigationPoint.FindSelectableOnRight() != null
                                ? navigationPoint.FindSelectableOnRight()
                                : navigationPoint;
            _confirmButton.navigation = nav;
            foreach (Button button in _settingsButtons)
            {
                nav = button.navigation;
                nav.selectOnRight = navigationPoint;
                button.navigation = nav;
            }
        }

        public void SaveSettings()
        {
            // saving changed settings in playerprefs (UNIMPLEMENTED YET)
            SettingsManager.SaveSettings();
        }

        public void DiscardSettings()
        {
            // discard settings and load previous from playerprefs (UNIMPLEMENTED YET)
            SettingsManager.DiscardSettings();
        }

        // Debug function for local testing as client
        public void ExecuteJoin()
        {
            NetworkManager.Singleton.StartClient();
        }

        // Debug function for local testing as host
        public void ExecuteStart()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void AssignNickname(string value)
        {
            Nickname = value;
        }

        public void AssignCode(string value)
        {
            Code = value;
        }

        public void SelectorUpdate(string type, int index)
        {
            Selector currentSelector = null;
            foreach (Selector selector in _selectors)
                if (selector._type == type)
                    currentSelector = selector;
            if (currentSelector != null) currentSelector.ChangeValue(index);
        }

        public void OnSelectUI()
        {
            _soundSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sound/select_sound"));
        }

        public void OnClickUI()
        {
            _soundSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sound/click_sound"));
        }
    }
}