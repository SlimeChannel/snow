namespace snow.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.EventSystems;
    using TMPro;
    using System;

    public class CustomInputField : MonoBehaviour, IPointerClickHandler, ISelectHandler
    {
        private TMP_InputField _inputField;
        private Animator _animator;
        private bool _isEditing = false;
        private bool _justStoppedEditing = false;
        private event Action OnSelection;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _inputField = GetComponent<TMP_InputField>();
            _inputField.interactable = false;
            OnSelection += MainMenuManager.Singleton.OnSelectUI;
        }

        private void OnDestroy()
        {
            OnSelection -= MainMenuManager.Singleton.OnSelectUI;
        }

        private void Update()
        {
            bool isMouseOver = false;
            if (EventSystem.current != null)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;
                List<RaycastResult> results = new List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);
                foreach (var result in results)
                    if (result.gameObject == gameObject)
                    {
                        isMouseOver = true;
                        break;
                    }
            }

            if (_isEditing)
            {
                if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Escape))
                    StopEditing();
                else if (Input.GetMouseButtonDown(0) && !isMouseOver)
                {
                    _inputField.enabled = false;
                    StopEditing();
                    _inputField.enabled = true;
                }
            }
            else if (!_justStoppedEditing && IsSelected() && Input.GetButtonDown("Submit"))
            {
                StartEditing();
            }
            UpdateAnimation();

            if (_justStoppedEditing && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
                _justStoppedEditing = false;
        }

        private bool IsSelected()
        {
            return EventSystem.current.currentSelectedGameObject == gameObject;
        }

        private void UpdateAnimation()
        {
            if (_animator == null) return;
            _animator.Play(_isEditing ? "Pressed" : IsSelected() ? "Selected" : "Normal");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isEditing) StartEditing();
        }

        private void StartEditing()
        {
            MainMenuManager.Singleton.OnClickUI();
            _isEditing = true;
            _inputField.interactable = true;
            _inputField.caretWidth = 1;
            _inputField.selectionColor = new Color32(168, 206, 255, 255);
            _inputField.ActivateInputField();
            _inputField.caretPosition = _inputField.text.Length;
            _justStoppedEditing = false;
        }
        
        private void StopEditing()
        {
            _isEditing = false;
            _inputField.interactable = false;
            _inputField.caretWidth = 0;
            _inputField.selectionColor = Color.clear;
            _inputField.DeactivateInputField();
            EventSystem.current.SetSelectedGameObject(gameObject);
            _justStoppedEditing = true;
        }

        public void OnEndEdit()
        {
            if (_isEditing) StopEditing();
        }

        public void OnSelect(BaseEventData eventData)
        {
            OnSelection?.Invoke();
        }
    }
}