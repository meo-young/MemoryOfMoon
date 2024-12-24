using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class LoadDialogueDatatable : MonoBehaviour
{
    [SerializeField] TextAsset dialogueDatatable;
    [HideInInspector] public List<DialogueInfo> dialogueInfo;
    private void Awake()
    {
        dialogueInfo = new List<DialogueInfo>();
        UpdateDialogueDatatable();
    }

    void UpdateDialogueDatatable()
    {
        StringReader reader = new StringReader(dialogueDatatable.text);
        bool head = false;

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if (!head)
            {
                head = true;
                continue;
            }

            string[] values = line.Split('\t');

            // values[0] => ID
            // values[1] => Character Name
            // values[2] => Sprite Type
            // values[3] => Dialogue Text

            DialogueInfo dialogue = new DialogueInfo(values[0],(int)(SpriteType)Enum.Parse(typeof(SpriteType), values[1]), values[2]);
            dialogueInfo.Add(dialogue);
        }
    }


    enum CharacterType
    {
        엑스트라1 = 0,
        엑스트라2 = 1,
        엑스트라3 = 2,
        츠네모리유우지 = 3,
        스즈키토우마 = 4,
        후케토우지 = 5,
        후지다나카 = 6,
        코즈키료고 = 7,
        기노자사이고 = 8,
        토마코자부로 = 9

    }

    enum SpriteType
    {
        Default = 0,
        Smile = 1,
        Perplexity = 2,
        Playful = 3,
    }
}

public class DialogueInfo
{
    public string characterName;
    public int spriteType;
    public string text;

    public DialogueInfo(string characterName, int spriteType, string text)
    {
        this.characterName = characterName;
        this.spriteType = spriteType;
        this.text = text;
    }
}
