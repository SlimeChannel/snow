namespace snow.Player
{
    using Item.Inventory;
    using UnityEngine;
    using Unity.Netcode;

    public class Player : NetworkBehaviour
    {
        public Inventory Inventory { get; private set; }
        public int Health { get; private set; }
        public int MaxHealth { get; private set; }
        public int Hunger { get; private set; }
        public int MaxHunger { get; private set; }
        public int Stamina { get; private set; }
        public int MaxStamina { get; private set; }
        public float Temperature { get; private set; }
        public int Insulation { get; private set; }
        public int BaseSpeed;// { get; private set; }
        // public Tile[] LoadedTimes { get; private set; }
        // public AttitudeType AttitudeType { get; private set; }
        private Vector2 _movementVector;
        private Rigidbody2D _rb;
        void Start()
        {
            _rb = gameObject.GetComponent<Rigidbody2D>();
        }
        void Update()
        {
            if (!IsOwner) return;
            _movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }
        void FixedUpdate()
        {
            if (!IsOwner) return;
            _rb.velocity = _movementVector * BaseSpeed;
        }
    }
}