using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    private Dictionary<string, AudioSource> activeSFX = new Dictionary<string, AudioSource>();//효과음 일시 중단을 위한

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // 중복 인스턴스 방지
        }
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmAudioSource == null || bgmClip == null) return;

        // 이미 같은 클립이 재생 중이면 아무 것도 하지 않음
        if (bgmAudioSource.isPlaying && bgmAudioSource.clip == bgmClip)
        {
            return;
        }

        bgmAudioSource.Stop(); // 다른 곡이면 기존 재생 중지
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxAudioSource.PlayOneShot(clip);
    }
    public void Play_R_SFX(string namePrefix, int maxIndex)//SoundManager.Instance.Play_R_SFX("sound/이름",번호);
    {
        if (string.IsNullOrEmpty(namePrefix) || maxIndex < 1)
            return;

        int randomIndex = Random.Range(1, maxIndex + 1); // 1부터 maxIndex까지
        string clipName = $"{namePrefix}{randomIndex}";

        AudioClip clip = Resources.Load<AudioClip>($"sound/{clipName}");
        if (clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }
    public void PlaySFXWithStop(string clipName) // 중간에 끊을 수 있는 효과음을 위한 전용 매서드 
    {
        if (string.IsNullOrEmpty(clipName))
            return;

        // 이미 재생 중이면 먼저 중단
        if (activeSFX.ContainsKey(clipName))
        {
            activeSFX[clipName].Stop();
            Destroy(activeSFX[clipName]);
            activeSFX.Remove(clipName);
        }

        AudioClip clip = Resources.Load<AudioClip>($"sound/{clipName}");
        if (clip == null)
        {
            Debug.LogWarning($"Clip {clipName} not found in Resources.");
            return;
        }

        // 전용 AudioSource 생성
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = true;
        newSource.Play();

        // 재생 끝나면 자동 제거
        StartCoroutine(RemoveWhenDone(clipName, newSource));

        // 등록
        activeSFX[clipName] = newSource;
    }
    public void StopSFX(string clipName)
    {
        if (activeSFX.TryGetValue(clipName, out AudioSource source))
        {
            source.Stop();
            Destroy(source);
            activeSFX.Remove(clipName);
        }
    }
    private IEnumerator RemoveWhenDone(string clipName, AudioSource source)
    {
        yield return new WaitUntil(() => !source.isPlaying);
        if (activeSFX.ContainsKey(clipName))
        {
            activeSFX.Remove(clipName);
        }
        Destroy(source);
    }


}