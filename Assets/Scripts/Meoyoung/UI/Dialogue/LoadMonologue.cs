using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMonologue : MonoBehaviour
{
    private static LoadMonologue _instance;
    public static LoadMonologue Instance { get { return _instance; } }

    private Dictionary<string, string> monologues = new Dictionary<string, string>();

    [Tooltip("독백 데이터를 가져올 CSV 파일 경로 리스트")]
    public List<string> monologuePaths = new List<string>(); // Inspector 창에서 경로를 설정할 수 있도록 리스트 추가

    private void Awake()
    {
        _instance = this;
        LoadMonologues();

        /*if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            LoadMonologues();
        }*/
    }

    private void LoadMonologues()
    {
        foreach (string path in monologuePaths)
        {
            TextAsset csvFile = Resources.Load<TextAsset>("CSV/Monologue/" + path);
            if (csvFile != null)
            {
                string[] lines = csvFile.text.Split('\n');
                for (int i = 1; i < lines.Length; i++) // Skip header
                {
                    string[] values = lines[i].Split('\t');
                    if (values.Length >= 2)
                    {
                        string objectID = values[0].Trim();
                        string sceneName = values[1]; // Remove quotes
                        string objectName = values[3];
                        string monologue = values[4];
                        Debug.Log("objectID : " + objectID +
                            " sceneName : "+ sceneName + 
                            " objectName : " + objectName + 
                            " monologue : " + monologue);
                        monologues[objectID] = monologue;
                    }
                }
            }
            else
            {
                Debug.LogWarning($"CSV 파일을 찾을 수 없습니다: {path}");
            }
        }
    }

    public string GetMonologue(string objectID)
    {
        if (monologues.TryGetValue(objectID, out string monologue))
        {
            return monologue;
        }
        return "이 물건에 대해 특별한 것은 없어 보여.";
    }
}
