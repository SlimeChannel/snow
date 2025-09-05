namespace snow
{
    using Items.Inventory;
    using UnityEngine;

    public class Player : MonoBehaviour
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
            _movementVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        }
        void FixedUpdate()
        {
            _rb.velocity = _movementVector * BaseSpeed;
        }
    }
}