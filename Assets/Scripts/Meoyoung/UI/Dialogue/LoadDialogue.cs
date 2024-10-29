using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialogue : MonoBehaviour
{
    private static LoadDialogue _instance;
    public static LoadDialogue Instance { get { return _instance; } }

    private Dictionary<string, (string, string, string)> dialogues = new Dictionary<string, (string, string, string)>(); //캐릭터 이름, 스프라이트, 대사 순으로 저장

    private void Awake()
    {
        _instance = this;
        LoadDialogues();
        /*if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            LoadDialogues();
        }*/
    }

    private void LoadDialogues()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("CSV/Dialogue/CharacterDialogue"); //텍스트 파일 인덱스에 따라서 버그가 발생할 수 있음
        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // Skip header
        {
            string[] values = lines[i].Split('\t');
            if (values.Length >= 2)
            {
                string objectID = values[0].Trim();
                string sceneName = values[1]; // Remove quotes
                string characterName = values[3];
                string spriteName = values[4];
                string dialogue = values[5];
                //Debug.Log(objectID + characterName + spriteName + dialogue);
                dialogues[objectID] = (characterName, spriteName, dialogue);
            }
        }
    }

    public (string, string, string) GetDialogue(string objectID) //캐릭터 이름, 스프라이트 종류, 대사 순으로 반환
    {
        if (dialogues.TryGetValue(objectID, out var dialogue))
        {
            Debug.Log(dialogue);
            return dialogue;
        }
        return ("", "", "이 물건에 대해 특별할 것은 없어 보여.");
    }

}
