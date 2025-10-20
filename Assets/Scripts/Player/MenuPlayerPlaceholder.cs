namespace snow.Player
{
    using snow.UI;
    using UnityEngine;
    using Unity.Netcode;
    using Unity.Collections;
    using TMPro;
    
    public class MenuPlayerPlaceholder : NetworkBehaviour
    {
        public NetworkVariable<FixedString64Bytes> Nickname = new(
            "player",
            NetworkVariableReadPermission.Everyone,
            NetworkVariableWritePermission.Owner);
            
        [SerializeField] private int BaseSpeed = 5;
        private Vector2 _movementVector;
        private Rigidbody2D _rb;
        [SerializeField] private TextMeshProUGUI _nicknameField;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public override void OnNetworkSpawn()
        {
            Nickname.OnValueChanged += OnNicknameChanged;

            if (IsOwner) Nickname.Value = MainMenuManager.Singleton.Nickname;
            
            UpdateNicknameDisplay();
        }

        public override void OnNetworkDespawn()
        {
            Nickname.OnValueChanged -= OnNicknameChanged;
        }

        private void OnNicknameChanged(FixedString64Bytes oldValue, FixedString64Bytes newValue)
        {
            UpdateNicknameDisplay();
        }

        private void UpdateNicknameDisplay()
        {
            if (_nicknameField != null) _nicknameField.text = Nickname.Value.ToString();
        }

        private void Update()
        {
            if (!IsOwner) return;
            _movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }

        private void FixedUpdate()
        {
            if (!IsOwner) return;
            _rb.velocity = _movementVector * BaseSpeed;
        }
    }
}