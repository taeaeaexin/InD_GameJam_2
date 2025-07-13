using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    private Dictionary<string, AudioSource> activeSFX = new Dictionary<string, AudioSource>();//ȿ���� �Ͻ� �ߴ��� ����

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // �ߺ� �ν��Ͻ� ����
        }
    }

    public void PlayBGM(AudioClip bgmClip)
    {
        if (bgmAudioSource == null || bgmClip == null) return;

        // �̹� ���� Ŭ���� ��� ���̸� �ƹ� �͵� ���� ����
        if (bgmAudioSource.isPlaying && bgmAudioSource.clip == bgmClip)
        {
            return;
        }

        bgmAudioSource.Stop(); // �ٸ� ���̸� ���� ��� ����
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null) return;

        sfxAudioSource.PlayOneShot(clip);
    }
    public void Play_R_SFX(string namePrefix, int maxIndex)//SoundManager.Instance.Play_R_SFX("sound/�̸�",��ȣ);
    {
        if (string.IsNullOrEmpty(namePrefix) || maxIndex < 1)
            return;

        int randomIndex = Random.Range(1, maxIndex + 1); // 1���� maxIndex����
        string clipName = $"{namePrefix}{randomIndex}";

        AudioClip clip = Resources.Load<AudioClip>($"sound/{clipName}");
        if (clip != null)
        {
            sfxAudioSource.PlayOneShot(clip);
        }
    }
    public void PlaySFXWithStop(string clipName) // �߰��� ���� �� �ִ� ȿ������ ���� ���� �ż��� 
    {
        if (string.IsNullOrEmpty(clipName))
            return;

        // �̹� ��� ���̸� ���� �ߴ�
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

        // ���� AudioSource ����
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = clip;
        newSource.loop = true;
        newSource.Play();

        // ��� ������ �ڵ� ����
        StartCoroutine(RemoveWhenDone(clipName, newSource));

        // ���
        activeSFX[clipName] = newSource;
    }
    public void StopSFX(string clipName)
    {
        if (activeSFX.TryGetValue(clipName, out AudioSource source))
        {
            if (source != null)
            {
                source.Stop();
                // ���⼱ Destroy ���� ����
            }

            activeSFX.Remove(clipName);
        }
    }

    private IEnumerator RemoveWhenDone(string clipName, AudioSource source)
    {
        yield return new WaitUntil(() => source == null || !source.isPlaying);

        if (source != null)
        {
            Destroy(source);
        }
    }



}