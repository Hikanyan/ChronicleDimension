using UnityEngine;

namespace Hikanyan.Core
{
    /// <summary>
    /// 継承してSingleton使用します。
    /// 継承先でAwakeが必要な場合OnAwake()を呼んでください。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                // 既存のインスタンスが存在しない場合、シーン内から検索
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    // 既存のインスタンスが存在しない場合、新しいGameObjectを作成してコンポーネントをアタッチ
                    if (_instance == null)
                    {
                        // 既存のインスタンスが存在しない場合、新しいGameObjectを作成してコンポーネントをアタッチ
                        GameObject singletonObject = new GameObject();
                        _instance = singletonObject.AddComponent<T>();
                        singletonObject.name = typeof(T).ToString();

                        // 継承先でDontDestroyOnLoadを使用するかどうかの判断
                        AbstractSingleton<T> singleton = _instance as AbstractSingleton<T>;
                        if (singleton != null && singleton.UseDontDestroyOnLoad)
                        {
                            DontDestroyOnLoad(singletonObject);
                        }
                    }
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                // 既存のインスタンスが存在し、かつ自身が既存のインスタンスでない場合、自身を破棄
                Destroy(gameObject);
                return;
            }

            // 自身をシングルトンのインスタンスとして設定
            _instance = this as T;

            // 継承先でDontDestroyOnLoadを使用するかどうかの判断
            if (UseDontDestroyOnLoad)
            {
                DontDestroyOnLoad(gameObject);
            }

            OnAwake();
        }

        /// <summary>
        /// 継承先でAwakeが必要な場合
        /// </summary>
        protected virtual void OnAwake()
        {
        }

        /// <summary>
        /// 継承先でDontDestroyOnLoadを使用するかどうかを制御します。
        /// </summary>
        protected virtual bool UseDontDestroyOnLoad => false;
    }
}
