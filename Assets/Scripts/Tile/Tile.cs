namespace snow.Tile
{
    using UnityEngine;
    public class Tile : MonoBehaviour
    {
        [SerializeField] private GameObject _flooring;
        [SerializeField] private GameObject _snow;
        [SerializeField] private GameObject _object;
        [SerializeField] private float _snowPercentage;
        [SerializeField] private float _revealPercentage;
    }
}