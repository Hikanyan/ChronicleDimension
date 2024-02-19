using UnityEngine;

namespace ChronicleDimensionProject.Common
{
    /// <summary> シングルトンの基底クラス </summary>
    public abstract class AbstractSingleton<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// 継承先でDontDestroyOnLoadを使用するかどうかを制御します。
        /// </summary>
        protected virtual bool UseDontDestroyOnLoad => false;

        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance != null) return _instance;
                _instance = FindObjectOfType<T>();
                if (_instance != null) return _instance;
                GameObject singletonObject = new GameObject(typeof(T).Name);
                _instance = singletonObject.AddComponent<T>();
                DontDestroyOnLoad(singletonObject);
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                if (UseDontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }

                OnAwake();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        /// <summary> Awake時に実行される処理 </summary>
        protected virtual void OnAwake()
        {
        }

        private void OnDestroy()
        {
            if (_instance != this) return;
            _instance = null;
            OnDestroyed();
        }

        /// <summary> OnDestroy時に実行される処理 </summary>
        protected virtual void OnDestroyed()
        {
        }
    }
}