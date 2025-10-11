namespace snow.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.EventSystems;
    using System.Collections.Generic;
    using TMPro;

    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private Animator _anim;
        [SerializeField] private Transform _settingsContainer;
        [SerializeField] private GameObject _defaultSelectable;
        [SerializeField] private RectTransform _settingsArrow;
        [SerializeField] private List<Button> _settingsButtons;
        private Dictionary<string, int> _arrowPositions = new()
        {
            {"game", 50},
            {"graphics", -50},
            {"audio", -150},
            {"controls", -250}
        };
        private void Update()
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;

                System.Collections.Generic.List<RaycastResult> results = new System.Collections.Generic.List<RaycastResult>();
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
            if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
                if (EventSystem.current.currentSelectedGameObject == null)
                    EventSystem.current.SetSelectedGameObject(_defaultSelectable);
        }
        public void TabChange(string animationName)
        {
            _anim.Play(animationName);
        }
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
            for (int i = 0; i < _settingsContainer.childCount; i++)
            {
                GameObject child = _settingsContainer.GetChild(i).gameObject;
                child.SetActive(category == child.GetComponent<SettingsTab>().Tag);
            }
            _settingsArrow.anchoredPosition = new Vector2(_settingsArrow.anchoredPosition.x, _arrowPositions[category]);
        }
        public void ChangeSettingsNavigation(Button navigationPoint)
        {
            foreach (Button button in _settingsButtons)
            {
                Navigation nav = button.navigation;
                nav.selectOnRight = navigationPoint;
                button.navigation = nav;
            }
        }
        public void SaveSettings()
        {

        }
        public void DiscardSettings()
        {

        }
    }
}