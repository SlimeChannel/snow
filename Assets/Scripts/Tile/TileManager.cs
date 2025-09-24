namespace snow.Tile
{
    using System.Collections.Generic;
    using UnityEngine;
    public class TileManager : MonoBehaviour
    {
        [SerializeField] private int _mapScale;
        [SerializeField] private GameObject _tile;
        private List<List<Tile>> _tileList = new();
        void Start()
        {
            for (int i = 0; i < _mapScale; i++)
            {
                _tileList.Add(new List<Tile>());
                for (int j = 0; j < _mapScale; j++)
                {
                    _tileList[i].Add(Instantiate(_tile, new Vector3(i, j, 0), new Quaternion(), this.transform).GetComponent<Tile>());
                }
            }
            transform.position = new Vector3(-_mapScale / 2, -_mapScale / 2, 0);
            switch (_mapScale % 2)
            {
                case 0:
                    for (int i = _mapScale / 2 - 2; i < _mapScale / 2 + 2; i++)
                        for (int j = _mapScale / 2 - 2; j < _mapScale / 2 + 2; j++)
                        {
                            _tileList[i][j].ChangeFlooring(FlooringType.wood);
                        }
                    break;
                case 1:
                    for (int i = _mapScale / 2 - 2; i < _mapScale / 2 + 3; i++)
                        for (int j = _mapScale / 2 - 2; j < _mapScale / 2 + 3; j++)
                        {
                            _tileList[i][j].ChangeFlooring(FlooringType.wood);
                        }
                    break;
            }
        }
        public void SnowFall()
        {
            foreach (List<Tile> list in _tileList)
                foreach (Tile tile in list)
                    tile.ChangeSnowPercentage(0.2f);
        }
    }
}
