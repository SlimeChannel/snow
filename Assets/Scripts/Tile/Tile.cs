namespace snow.Tile
{
    using UnityEngine;
    public class Tile : MonoBehaviour
    {
        [SerializeField] private GameObject _flooringObject;
        [SerializeField] private GameObject _snowObject;
        [SerializeField] private GameObject _objectObject;
        [SerializeField] private float _snowPercentage;
        [SerializeField] private float _objectRevealPercentage;
        [SerializeField] private FlooringType _flooringType;
        [SerializeField] private ColorType _colorType;
        public void ChangeSnowPercentage(float addedPercentage)
        {
            if (_flooringType != FlooringType.wood)
            {
                _snowPercentage += addedPercentage;
                OnSnowPercentageChange();
            }
        }
        public void ChangeFlooring(FlooringType flooringType)
        {
            _flooringType = flooringType;
            OnFlooringChange();
        }
        private void OnFlooringChange()
        {
            _flooringObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Tiles/" + _flooringType.ToString());
            if (_flooringType == FlooringType.wood)
            {
                _snowPercentage = 0;
                OnSnowPercentageChange();
            }
        }
        private void OnSnowPercentageChange()
        {
            _snowObject.GetComponent<SpriteRenderer>().color = ColorList.List[_colorType.ToString()] - new Color(0, 0, 0, 1 - _snowPercentage);
        }
        private void Start()
        {
            OnFlooringChange();
            OnSnowPercentageChange();
        }
    }
}