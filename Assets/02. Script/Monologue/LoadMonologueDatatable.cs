using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LoadMonologueDatatable : MonoBehaviour
{
    [SerializeField] TextAsset monologueDatatable;
    public Dictionary<string, string> monologues;

    private void Awake()
    {
        monologues = new Dictionary<string, string>();
        UpdateMonologueDatatable();
    }

    // [0] => Object Name
    // [1] => Monologue
    // [2] => Interaction
    // [3] => 비고
    void UpdateMonologueDatatable()
    {
        StringReader reader = new(monologueDatatable.text);
        bool head = false;

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            if(!head)
            {
                head = true;
                continue;
            }

            string[] values = line.Split('\t');
            monologues.Add(values[0], values[1]);
        }
    }
}