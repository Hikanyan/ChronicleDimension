using UnityEngine;
using UnityEngine.Events;
using System.Collections;

//AddListnerを行う方です
public class EventTest : MonoBehaviour {

    private UnityAction someListener;

    void Awake ()
    {
        someListener = new UnityAction (SomeFunction);
    }

    void OnEnable ()
    {
        //ここでAddListnerでイベントを登録しています
        EventManager.StartListening ("test", someListener);
        EventManager.StartListening ("Spawn", SomeOtherFunction);
        EventManager.StartListening ("Destroy", SomeThirdFunction);
    }

    void OnDisable ()
    {
        //事後処理を忘れずに
        EventManager.StopListening ("test", someListener);
        EventManager.StopListening ("Spawn", SomeOtherFunction);
        EventManager.StopListening ("Destroy", SomeThirdFunction);
    }

    //EventManagerに登録する関数たち

    void SomeFunction ()
    {
        Debug.Log ("Some Function was called!");
    }

    void SomeOtherFunction ()
    {
        Debug.Log ("Some Other Function was called!");
    }

    void SomeThirdFunction ()
    {
        Debug.Log ("Some Third Function was called!");
    }
}