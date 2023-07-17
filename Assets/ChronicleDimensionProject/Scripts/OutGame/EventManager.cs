using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

//イベントを管理するクラス
public class EventManager : AbstractSingleton<EventManager>
{
    //イベント待ちを記録するDictionary
    private Dictionary <string, UnityEvent> eventDictionary;
    
}
