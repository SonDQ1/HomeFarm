using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public static SoundController instance;
    public AudioSource[] MusicAudio;
    public AudioSource[] SoundAudio;

    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
        _playSoundBird();
        
    }
    public void _playSound(int index)
    {
        if (!SoundAudio[0].isPlaying)
            SoundAudio[index].Play();
        if (index != 0) SoundAudio[index].Play();
    }

    public void _stopSound(int index)
    {
        SoundAudio[(int)index].Stop();
    }

    public void _playMusicBgr(int id, float volum)
    {
        MusicAudio[0].Play();
        MusicAudio[0].volume = volum;
    }
    public void _playSoundBird()
    {
        StartCoroutine(delaySoundBird());
    }
    public void _changeSoundVolume(float vl)
    {
        for (int i = 0; i < SoundAudio.Length; i++)
        {
            SoundAudio[i].volume = vl;
        }
    }
    public void _changeMusicVolume(float vl)
    {
        for (int i = 0; i < MusicAudio.Length; i++)
        {
            MusicAudio[i].volume = vl;
        }
    }

    IEnumerator delaySoundBird()
    {
        yield return new WaitForSeconds(Random.Range(3f,5f));
        int rd = Random.Range(10, 18);
        _playSound(rd);
        StartCoroutine(delaySoundBird());
    }
}
