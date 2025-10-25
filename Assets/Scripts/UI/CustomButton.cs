namespace snow.UI
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class CustomButton : MonoBehaviour, ISelectHandler
    {
        private event Action OnSelection;
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
            OnSelection += MainMenuManager.Singleton.OnSelectUI;
            _button.onClick.AddListener(MainMenuManager.Singleton.OnClickUI);
        }

        private void OnDestroy()
        {
            OnSelection -= MainMenuManager.Singleton.OnSelectUI;
            _button.onClick.RemoveListener(MainMenuManager.Singleton.OnClickUI);
        }

        public void OnSelect(BaseEventData eventData)
        {
            OnSelection?.Invoke();
        }
    }
}