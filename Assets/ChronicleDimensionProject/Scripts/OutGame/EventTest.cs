using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTest : MonoBehaviour
{
    private UnityAction someListener;
    private GameObject spawnedObject;

    void Awake()
    {
        someListener = new UnityAction(SomeFunction);
    }

    void OnEnable()
    {
        EventManager.StartListening("test", someListener);
        EventManager.StartListening("Spawn", SpawnObject);
        EventManager.StartListening("Destroy", DestroyObject);
    }

    void OnDisable()
    {
        EventManager.StopListening("test", someListener);
        EventManager.StopListening("Spawn", SpawnObject);
        EventManager.StopListening("Destroy", DestroyObject);
    }

    void OnDestroy()
    {
        // OnDestroyメソッド内で生成したオブジェクトがあれば破棄する
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
        }
    }

    void SomeFunction()
    {
        Debug.Log("Some Function was called!");
    }

    void SpawnObject()
    {
        // 新しいGameObjectを生成して保持する
        spawnedObject = new GameObject("SpawnedObject");
        Debug.Log("Spawned Object!");
    }

    void DestroyObject()
    {
        // 保持しているGameObjectを破棄する
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            Debug.Log("Destroyed Object!");
        }
    }
}