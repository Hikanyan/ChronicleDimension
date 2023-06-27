using System;
using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Hikanyan.Gameplay.UI
{
    public class MenuUIButto : InputUIButton
    {
        [SerializeField]
        CanvasGroup _button;


        protected void OnPointerDown()
        {
            //transform.DO
        }

        protected void OnPointerUp()
        {
            
        }
    }
}