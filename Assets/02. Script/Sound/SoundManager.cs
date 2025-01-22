using UnityEngine;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine.Audio;
using static Constant;
public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public SerializedDictionary<SFX, AudioClip> sfxs = new ();    // SFX 모음
    public SerializedDictionary<BGM, AudioClip> bgms = new ();    // BGM 모음

    [Header("# BGM")]
    [Range(0, 1)] public float bgmVolume;

    [Header("# SFX")]
    public int channels;
    [Range(0, 1)] public float sfxVolume;


    private AudioSource bgmPlayer;
    private Queue<AudioSource> sfxQueue;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start() {
        Init();
    }

    #region Initalize
    void Init()
    {
        InitBGMPlayer();
        InitSFXPlayer();
    }

    void InitBGMPlayer()
    {
        GameObject bgmObject = new GameObject("BGMPlayer");
        bgmObject.transform.parent = this.transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();

        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.dopplerLevel = 0.0f;              // 입체효과 비활성화
        bgmPlayer.reverbZoneMix = 0.0f;             // 동굴과 같은 입체환경 반영 비활성화
    }

    void InitSFXPlayer()
    {
        GameObject sfxObject = new GameObject("SFXPlayer");
        sfxObject.transform.parent = this.transform;
        AudioSource[] sfxPlayers = new AudioSource[channels];
        sfxQueue = new Queue<AudioSource>();

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].loop = false;
            sfxPlayers[i].volume = sfxVolume;
            sfxPlayers[i].dopplerLevel = 0.0f;      // 입체효과 비활성화
            sfxPlayers[i].reverbZoneMix = 0.0f;     // 동굴과 같은 입체환경 반영 비활성화
            sfxQueue.Enqueue(sfxPlayers[i]);
        }

    }
    #endregion

    #region BGM
    public void PlayBGM(BGM bgm)
    {
        if (bgmPlayer == null)
        {
            Debug.Log("bgmPlayer가 초기화 되지 않았습니다");
            return;
        }

        AudioClip clip = bgms[bgm];

        bgmPlayer.clip = clip;
        bgmPlayer.Play();
    }

    public void StopBGM()
    {
        if (bgmPlayer == null)
        {
            Debug.Log("bgmPlayer가 초기화 되지 않았습니다");
            return;
        }

        bgmPlayer.Stop();
    }
    #endregion

    #region SFX
    public void PlaySFX(SFX _sfx)
    {
        if (sfxQueue.Count == 0)
        {
            Debug.LogWarning("사용 가능한 AudioSource가 없습니다.");
            return;
        }

        AudioSource player = sfxQueue.Dequeue();
        AudioClip clip = sfxs[_sfx];

        if (player != null)
        {
            player.clip = clip;

            player.Play();

            StartCoroutine(ReturnToQueueAfterPlay(player));
        }
    }

    private IEnumerator ReturnToQueueAfterPlay(AudioSource player)
    {
        yield return new WaitForSeconds(player.clip.length);

        sfxQueue.Enqueue(player);
    }
    #endregion
}
