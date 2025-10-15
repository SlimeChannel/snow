namespace snow.Player
{
    using UnityEngine;
    using Unity.Netcode;
    public class CameraMovement : NetworkBehaviour
    {
        [SerializeField] private int _cameraLooseness = 20;
        [SerializeField] private Transform _target;
        public override void OnNetworkSpawn()
        {
            if (!IsOwner) gameObject.SetActive(false);
        }
        private void FixedUpdate()
        {
            Vector2 camPos = new(transform.position.x, transform.position.y);
            Vector2 tarPos = new(_target.position.x, _target.position.y);
            if (camPos != tarPos)
                transform.position += new Vector3((tarPos - camPos).x, (tarPos - camPos).y, 0) / _cameraLooseness;
        }
    }
}