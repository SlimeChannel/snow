namespace snow.Weather
{
    using UnityEngine;
    using snow.Tile;
    public class WeatherManager : MonoBehaviour
    {
        [SerializeField] private int _maxTime;
        public int Time;

        private void FixedUpdate()
        {
            Time += 1;
            if(Time == _maxTime)
            {
                Time = 0;
                OnDayStart();
            }
        }
        private void OnDayStart()
        {
            this.transform.parent.Find("TileManager").GetComponent<TileManager>().SnowFall();
        }
    }
}