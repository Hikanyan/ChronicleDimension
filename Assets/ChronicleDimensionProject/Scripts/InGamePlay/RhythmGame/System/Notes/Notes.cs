using Unity.VisualScripting;
using UnityEngine;

public enum NotesType
{
    None,
    TapNote,
    SkillNote,
    FlickNote,
    DamageNote,
    LongNote,
}

public class Notes : MonoBehaviour
{
    float _time = 0.00f;
    private float _endTime = 0.00f; // ホールド
    int _block = 0;
    NotesType _type;
    bool _visible = false;
    bool _disable = false;
    bool _holding = false; // ホールド

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

    public void SetEndNotesPos(float posz)
    {
        GameObject endObject = Instantiate(gameObject);
        endObject.transform.position = new Vector3(transform.position.x, transform.position.y, posz);
    }
}