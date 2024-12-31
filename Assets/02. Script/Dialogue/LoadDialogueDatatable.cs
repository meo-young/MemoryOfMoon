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
        dialogueInfo = new List<DialogueInfo>(200);
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

            DialogueInfo dialogue = new DialogueInfo(values[1],(int)(SpriteType)Enum.Parse(typeof(SpriteType), values[2]), values[3], int.Parse(values[4]));
            dialogueInfo.Add(dialogue);
        }
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
    public int nextIndex;

    public DialogueInfo(string characterName, int spriteType, string text, int nextIndex)
    {
        this.characterName = characterName;
        this.spriteType = spriteType;
        this.text = text;
        this.nextIndex = nextIndex;
    }
}
