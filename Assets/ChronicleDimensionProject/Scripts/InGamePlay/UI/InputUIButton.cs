using UnityEngine;
using UnityEngine.EventSystems;


public abstract class InputUIButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    protected bool isPressed = false;

    /// <summary>
    /// クリックしたときの処理を実装する
    /// </summary>
    protected virtual void OnPointerDown()
    {
    }

    /// <summary>
    /// クリックを離したときの処理を実装する
    /// </summary>
    protected virtual void OnPointerUp()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
        OnPointerDown();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        OnPointerUp();
    }
}