using UnityEngine;

namespace ChronicleDimensionProject.RhythmGame
{
    public class RhythmGameTimer : MonoBehaviour
    {
        private float _startTime = 0;
        public float RealTime {  get; private set;  }
        static public RhythmGameTimer Instance;

        private float cacheTime = 0;

        public bool _isRunning { get; private set; } = false;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

        }

        public void TimerStart()// GameManagerから呼び出す
        {
            _isRunning = true;
            _startTime = Time.realtimeSinceStartup;
        }
        float _pauseTime = 0;
        public void TimerPause()
        {
            _isRunning = false;
            _pauseTime = Time.realtimeSinceStartup;
        }

        public void TimerUnPause()
        {
            _isRunning = true;
            cacheTime += Time.realtimeSinceStartup - _pauseTime;
            //_startTime = Time.realtimeSinceStartup;
        }

        public void TimerStop()// GameManagerから呼び出す
        {
            _isRunning = false;
        }

        // Update is called once per frame
        void Update()
        {
            if(!_isRunning)
            {
                return;
            }

            RealTime = Time.realtimeSinceStartup - _startTime - cacheTime;

            Debug.Log(RealTime);
        }
    }
}