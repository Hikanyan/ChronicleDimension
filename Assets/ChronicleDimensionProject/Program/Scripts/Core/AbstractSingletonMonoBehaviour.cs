using UnityEngine;

namespace ChronicleDimensionProject.Common
{
    /// <summary> シングルトンの基底クラス </summary>
    public abstract class AbstractSingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// 継承先でDontDestroyOnLoadを使用するかどうかを制御します。
        /// </summary>
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;
                instance = FindObjectOfType<T>();
                GameObject singletonObject = new GameObject(typeof(T).Name);
                instance = singletonObject.AddComponent<T>();
                if ((instance as AbstractSingletonMonoBehaviour<T>).UseDontDestroyOnLoad)
                {
                    DontDestroyOnLoad(singletonObject);
                }

                return instance;
            }
        }

        protected abstract bool UseDontDestroyOnLoad { get; }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                if (UseDontDestroyOnLoad)
                {
                    DontDestroyOnLoad(gameObject);
                }

                OnAwake();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnDestroy()
        {
            if (instance != this) return;
            instance = null;
            OnDestroyed();
        }

        protected virtual void OnDestroyed()
        {
        }
    }
}