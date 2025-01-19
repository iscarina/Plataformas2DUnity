using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private Sound[] musicSounds, sfxSounds;
    [SerializeField] private AudioSource musicSource, sfxSource;

    public enum SOUNDS_ENUM
    {
        MainTheme,
        BossAtt,
        BossMano,
        Dash,
        HitEnemy,
        HitPlayer,
        Latigo,
        Abanico
    }

    public static readonly Dictionary<SOUNDS_ENUM, string> SOUNDS = new Dictionary<SOUNDS_ENUM, string>
    {
        { SOUNDS_ENUM.MainTheme, "MainTheme" },
        { SOUNDS_ENUM.BossAtt, "BossAtt" },
        { SOUNDS_ENUM.BossMano, "BossMano" },
        { SOUNDS_ENUM.Dash, "Dash" },
        { SOUNDS_ENUM.HitEnemy, "HitEnemy" },
        { SOUNDS_ENUM.HitPlayer, "HitPlayer" },
        { SOUNDS_ENUM.Latigo, "Latigo" },
        { SOUNDS_ENUM.Abanico, "Abanico" },
    };

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        PlayMusic(AudioManager.SOUNDS[AudioManager.SOUNDS_ENUM.MainTheme]);
    }

    public void PlayMusic(string name)
    {
        Sound s = System.Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.LogWarning($"Sound Not Found {name}");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = System.Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void StopSFX()
    {
        sfxSource.Stop();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
