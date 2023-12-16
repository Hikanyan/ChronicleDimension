using UnityEngine;

/// <summary>
/// 継承してSingleton使用します。
/// 継承先でAwakeが必要な場合OnAwake()を呼んでください。
/// 継承先でインスタンスをDeleteする必要な場合はDelete()を呼んでください。
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class AbstractSingleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject();
                    instance = singletonObject.AddComponent<T>();
                    singletonObject.name = typeof(T).ToString();
                    DontDestroyOnLoad(singletonObject);
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        OnAwake();
    }

    /// <summary> 継承先でAwakeが必要な場合 </summary>
    protected virtual void OnAwake()
    {
    }

    /// <summary> 継承先でインスタンスをDeleteする必要な場合 </summary>
    protected void Delete()
    {
        Destroy(gameObject);
        instance = null;//インスタンスをnullにすることで再度Awakeが呼ばれるようにする
    }
}