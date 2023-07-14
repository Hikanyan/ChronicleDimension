using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveButton : MonoBehaviour
{
    [SerializeField] Button saveButton;
    [SerializeField] Button loadButton;

    [SerializeField] Button addButton;
    [SerializeField] int add = 1;
    [SerializeField] Text text;
    SaveData saveData = new SaveData();
    void Start()
    {
        saveButton.onClick.AddListener(OnClickSave);
        loadButton.onClick.AddListener(OnClickLoad);
        addButton.onClick.AddListener(OnClickAdd);
    }

    public void OnClickSave()
    {
        SaveManager.Instance.SaveGame();
    }

    public void OnClickLoad()
    {
        SaveManager.Instance.LoadGame();
    }
    
    public void OnClickAdd()
    {
        saveData.saveData[0] += add;
    }

    private void Update()
    {
        text.text = saveData.saveData[0].ToString();
    }
}