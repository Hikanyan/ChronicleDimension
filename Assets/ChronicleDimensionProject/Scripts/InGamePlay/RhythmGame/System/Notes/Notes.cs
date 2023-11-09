using Unity.VisualScripting;
using UnityEngine;


public class Notes : MonoBehaviour
{
    public float _time = 0.00f;
    public int _block = 0;
    public NotesType _type;
    public bool _visible = false;
    public bool _disable = false;

    [SerializeField] public GameObject endObject = default;
    public float _endTime = 0.00f; // ホールド
    public bool _holding = false; // ホールド

    public enum NotesType
    {
        None,
        TapNote,
        SkillNote,
        FlickNote,
        DamageNote,
        HoldNote,
    }

    public void SetVisible(bool visible)
    {
        if (visible)
        {
            gameObject.SetActive(true);
            _visible = true;
        }
        else
        {
            gameObject.SetActive(false);
            _visible = false;
            transform.position = new Vector3(0, 0, -10);
        }
    }

    public void SetNotesPos(float posz)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, posz);
    }

    /// <summary>LongNotesの最後の処理 </summary>
    /// <param name="posz"></param>
    public void SetEndNotesPos(float posz)
    {
        endObject.transform.position = new Vector3(transform.position.x, transform.position.y, posz);
    }
}