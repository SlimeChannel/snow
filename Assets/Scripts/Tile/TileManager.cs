namespace snow.Tile
{
    using System.Collections.Generic;
    using UnityEngine;
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private readonly int _mapScale;
        [SerializeField] private GameObject _tile;
        private List<List<GameObject>> _tileList = new();
        void Start()
        {
            for (int i = 0; i < _mapScale; i++)
            {
                _tileList.Add(new List<GameObject>());
                for (int j = 0; j < _mapScale; j++)
                {
                    _tileList[i].Add(Instantiate(_tile, new Vector3(i, j, 0), new Quaternion(), this.transform));
                }
            }
            transform.position = new Vector3(-_mapScale / 2, -_mapScale / 2, 0);
            switch (_mapScale % 2)
            {
                case 0:
                    
                    break;
                case 1:
                    break;
            }
        }
    }
}
