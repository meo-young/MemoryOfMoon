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

    public SerializedDictionary<SFX, AudioClips> sfxs = new ();    // SFX 모음
    public SerializedDictionary<BGM, AudioClips> bgms = new ();    // BGM 모음

    [Header("# BGM")]
    [Range(0, 1)] public float bgmVolume;

    [Header("# SFX")]
    public int channels;
    [Range(0, 1)] public float sfxVolume;
    [Tooltip("소리가 들리는 최대 거리")]
    public float maxHearDistance = 20f;     // 소리가 들리는 최대 거리 추가


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

        if(bgms[bgm].clips.Length == 0)
        {
            Debug.LogWarning("BGM 클립이 없습니다");
            return;
        }

        bgmPlayer.clip = bgms[bgm].clips[UnityEngine.Random.Range(0, bgms[bgm].clips.Length)];
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
    public void PlaySFX(SFX _sfx, Vector3 _position = default)
    {
        if (sfxQueue.Count == 0)
        {
            Debug.LogWarning("사용 가능한 AudioSource가 없습니다.");
            return;
        }

        if(sfxs[_sfx].clips.Length == 0)
        {
            Debug.LogWarning("SFX 클립이 없습니다");
            return;
        }

        AudioSource player = sfxQueue.Dequeue();
        AudioClip clip = sfxs[_sfx].clips[UnityEngine.Random.Range(0, sfxs[_sfx].clips.Length)];

        if (player != null)
        {
            player.clip = clip;
            player.volume = UnityEngine.Random.Range(sfxVolume - 0.1f, sfxVolume + 0.1f);
            PlaySoundWithSpaceEffect(player, _position);
        }
    }

    private void PlaySoundWithSpaceEffect(AudioSource _audioSource, Vector3 _position)
    {
        // 위치가 지정된 경우에만 스테레오 팬과 거리 기반 볼륨 적용
        if (_position != default)
        {
            // 카메라 기준으로 상대적 위치 계산
            Vector3 cameraPosition = Camera.main.transform.position;
            Vector3 directionToSound = _position - cameraPosition;
            Vector3 localDirection = Camera.main.transform.InverseTransformDirection(directionToSound);
            float distance = directionToSound.magnitude;

            // 거리에 따른 볼륨 계산
            float volumeMultiplier = 1f - Mathf.Clamp01(distance / maxHearDistance);
            _audioSource.volume = sfxVolume * volumeMultiplier;

            // 좌우 위치에 따른 StereoPan 계산 (-1 ~ 1)
            float stereoPan = Mathf.Clamp(localDirection.x / 3f, -1f, 1f);
                
            // 상하 위치에 따른 가중치 계산 (0 ~ 1)
            float verticalFactor = Mathf.Abs(localDirection.y) / 3f;
            verticalFactor = Mathf.Clamp01(verticalFactor);
                
            // 상하 거리가 멀수록 좌우 효과를 감소
            stereoPan *= (1f - verticalFactor * 0.5f);
                
            _audioSource.panStereo = stereoPan;
        }
        else
        {
            _audioSource.panStereo = 0f;
            _audioSource.volume = sfxVolume;
        }

        _audioSource.Play();
        sfxQueue.Enqueue(_audioSource);
    }
    #endregion
}


[System.Serializable]
public class AudioClips
{
    public AudioClip[] clips;
}