using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using Unity.VisualScripting;

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

            string characterName = values[1];
            int spriteType = (int)(SpriteType)Enum.Parse(typeof(SpriteType), values[2]);
            string dialogText = values[3];
            int nextIndex = int.Parse(values[4]);
            int transitionType = -1;

            if (values[5] != "") 
                transitionType = (int)(TransitionType)Enum.Parse(typeof(TransitionType), values[5]);

            int characterType = (int)(CharacterType)Enum.Parse(typeof(CharacterType), characterName.Replace(" ", ""));

            DialogueInfo dialogue = new DialogueInfo(characterName, spriteType, dialogText, nextIndex, transitionType, characterType);
            dialogueInfo.Add(dialogue);
        }
    }


    enum SpriteType
    {
        Default = 0,
        Smile = 1,
        Perplexity = 2,
        Angry = 3,
        Serious = 4,
        Mad = 5,
        Afraid = 6
    }

    enum TransitionType
    {
        Before = 0,
        After = 1,
        Both = 2
    }

    enum CharacterType
    {
        여학생A = 0,
        여학생B = 1,
        남학생A = 2,
        남학생B = 3,
        남학생C = 4,
        츠네모리유우지 = 5,
        스즈키토우마 = 6,
        후케토우지 = 7,
        후지다나카 = 8,
        코즈키료고 = 9,
        기노자사이고 = 10,
        토마코자부로 = 11,
        츠네모리유우카 = 12
    }
}

public class DialogueInfo
{
    public string characterName;
    public int spriteType;
    public string text;
    public int nextIndex;
    public int transitionType;
    public int characterType;

    public DialogueInfo(string characterName, int spriteType, string text, int nextIndex, int transitionType, int characterType)
    {
        this.characterName = characterName;
        this.spriteType = spriteType;
        this.text = text;
        this.nextIndex = nextIndex;
        this.transitionType = transitionType;
        this.characterType = characterType;
    }
}
