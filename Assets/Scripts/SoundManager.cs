using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

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

}