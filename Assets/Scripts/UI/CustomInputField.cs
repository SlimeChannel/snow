namespace snow.UI
{
    using UnityEngine;
    using TMPro;
    using UnityEngine.EventSystems;
    using UnityEngine.UI;

    public class InputFieldNavigationOverride : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_InputField _inputField;
        [SerializeField] private Animator _animator;
        
        private bool _isEditing = false;
        private bool _wasMouseOver = false;

        void Start()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();
                
            if (_inputField == null)
                _inputField = GetComponent<TMP_InputField>();
                
            _inputField.interactable = false;
        }

        void Update()
        {
            // Проверяем наведение мыши в каждом кадре
            bool isMouseOver = false;
            
            // Прямая проверка мыши в Update
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

            // Если мышь над объектом - выбираем его
            if (isMouseOver && !_wasMouseOver)
            {
                EventSystem.current.SetSelectedGameObject(gameObject);
            }
            
            _wasMouseOver = isMouseOver;

            // Обновляем анимацию
            UpdateAnimation();

            // Начало редактирования по Enter
            if (IsSelected() && !_isEditing && Input.GetButtonDown("Submit"))
            {
                StartEditing();
            }
            
            // Выход из редактирования по Escape
            if (_isEditing && Input.GetKeyDown(KeyCode.Escape))
            {
                StopEditing();
            }
            
            // Выход из редактирования при клике вне InputField
            if (_isEditing && Input.GetMouseButtonDown(0) && !isMouseOver)
            {
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

        // Клик мышкой - НАЧИНАЕМ РЕДАКТИРОВАНИЕ
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