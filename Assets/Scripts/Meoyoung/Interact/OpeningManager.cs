using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpeningManager : MonoBehaviour
{
    public TMP_Text text;
    private string dialogue;
    public AudioSource _audioSource;
    public AudioClip typingSound;
    public static OpeningManager Instance { get; private set; } // Singleton 인스턴스

    private void Start()
    {
        ShowOpening();
    }

    public void ShowOpening()
    {
        dialogue = ". . 그림자집?Q. . . . . 그 다이세츠 지역의?Q네, 그곳에 관한 이야기입니다.";
        StartCoroutine(ActiveOpening(dialogue));
    }
    
    private IEnumerator ActiveOpening(string dialogue)
    {
        // yield return new WaitForSeconds(2.0f);
        text.text = null;
        _audioSource.clip = typingSound;
        _audioSource.loop = true;
        _audioSource.Play();
        for (int i =0 ; i < dialogue.Length ; i++)
        {
            if(dialogue[i] == 'Q')
            {
                text.text += "\n";
                
            }
            else
            {
                text.text += dialogue[i];
                yield return new WaitForSeconds(0.1f);     
            }        
        }
        _audioSource.Stop();
    }
}

