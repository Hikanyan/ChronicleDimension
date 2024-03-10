using UnityEngine;

namespace ChronicleDimensionProject.Common
{
    /// <summary> シングルトンの基底クラス </summary>
    public abstract class AbstractSingletonMonoBehaviour<T> : MonoBehaviour, ISingleton<T> where T : MonoBehaviour
    {
        /// <summary>
        /// 継承先でDontDestroyOnLoadを使用するかどうかを制御します。
        /// </summary>
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
                if ((_instance as AbstractSingletonMonoBehaviour<T>).UseDontDestroyOnLoad)
                {
                    DontDestroyOnLoad(singletonObject);
                }

                return _instance;
            }
        }

        protected abstract bool UseDontDestroyOnLoad { get; }

        protected virtual void Awake()
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

        public virtual void OnAwake()
        {
        }

        protected virtual void OnDestroy()
        {
            if (_instance != this) return;
            _instance = null;
            OnDestroyed();
        }

        public virtual void OnDestroyed()
        {
        }
    }
}