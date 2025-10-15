namespace snow.UI
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class CustomInputField : MonoBehaviour, IPointerClickHandler
    {
        private TMP_InputField _inputField;
        private Animator _animator;
        
        private bool _isEditing = false;
        private bool _wasMouseOver = false;

        private void Start()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
                
            if (_inputField == null)
                _inputField = GetComponent<TMP_InputField>();
                
            _inputField.interactable = false;
        }

        private void Update()
        {
            bool isMouseOver = false;
            
            if (EventSystem.current != null)
            {
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                pointerData.position = Input.mousePosition;
                var results = new System.Collections.Generic.List<RaycastResult>();
                EventSystem.current.RaycastAll(pointerData, results);

                foreach (var result in results)
                {
                    if (result.gameObject == gameObject)
                    {
                        isMouseOver = true;
                        break;
                    }
                }
            }
            _wasMouseOver = isMouseOver;

            UpdateAnimation();

            if (_isEditing && (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Submit")))
            {
                Debug.Log("1");
                StopEditing();
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            if (IsSelected() && !_isEditing && Input.GetButtonDown("Submit"))
            {
                Debug.Log("2");
                StartEditing();
            }
            if (_isEditing && Input.GetMouseButtonDown(0) && !isMouseOver)
            {
                Debug.Log("3");
                StopEditing();
            }
        }

        private bool IsSelected()
        {
            return EventSystem.current.currentSelectedGameObject == gameObject;
        }

        private void UpdateAnimation()
        {
            if (_animator == null) return;

            if (_isEditing)
            {
                _animator.Play("Pressed");
            }
            else if (IsSelected())
            {
                _animator.Play("Selected");
            }
            else
            {
                _animator.Play("Normal");
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!_isEditing)
            {
                StartEditing();
            }
        }

        private void StartEditing()
        {
            _isEditing = true;
            _inputField.interactable = true;
            _inputField.ActivateInputField();
            _inputField.caretPosition = _inputField.text.Length;
        }

        private void StopEditing()
        {
            _isEditing = false;
            _inputField.interactable = false;
            _inputField.DeactivateInputField();
        }

        public void OnEndEdit(string text)
        {
            if (_isEditing)
            {
                StopEditing();
            }
        }
    }
}