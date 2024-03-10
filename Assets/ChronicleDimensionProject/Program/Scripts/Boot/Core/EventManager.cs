using System.Collections.Generic;
using ChronicleDimensionProject.Common;
using UnityEngine.Events;

namespace ChronicleDimensionProject.Scripts.OutGame
{
    /// <summary>
    /// イベントを管理するクラスです。
    /// </summary>
    public class EventManager : AbstractSingletonMonoBehaviour<EventManager>
    {
        //イベント待ちを記録するDictionary
        private Dictionary<string, UnityEvent> eventDictionary;

        //コンストラクタ
        public EventManager()
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
        }


        /// <summary>
        /// イベントスタンバイ開始。
        /// listerに関数名を渡すことで、
        /// eventNameイベントがTrigger（下記）されると関数が呼び出されます
        /// </summary>
        public static void StartListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// オブジェクトを破棄するときは
        /// これでスタンバイ状態を解かないと
        /// スタンバイ記録がずっと堆積していってしまいます
        /// </summary>
        public static void StopListening(string eventName, UnityAction listener)
        {
            //インライン化とは、コンパイラがメソッド呼び出しを行わず、
            //メソッドの本体を呼び出し元に埋め込むことです。
            UnityEvent thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        /// <summary>
        /// イベントをTriggerします
        /// AddListnerで登録していた関数全てが呼び出されます
        /// </summary>
        public static void TriggerEvent(string eventName)
        {
            UnityEvent thisEvent = null;
            if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }
    }
}


