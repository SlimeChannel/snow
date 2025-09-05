namespace snow
{
    using UnityEngine;
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private int _cameraLooseness = 20;
        [SerializeField] private Transform _target;
        private void FixedUpdate()
        {
            Vector2 camPos = new(transform.position.x, transform.position.y);
            Vector2 tarPos = new(_target.position.x, _target.position.y);
            if (camPos != tarPos)
                transform.position += new Vector3((tarPos - camPos).x, (tarPos - camPos).y, 0) / _cameraLooseness;
        }
    }
}