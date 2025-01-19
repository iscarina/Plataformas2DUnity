using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AllController : MonoBehaviour
{

    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unmuteSprite;
    [SerializeField] private Button ButtonSound;
    private AudioSource[] audioSources;
    private bool isMuted = false;

    private void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        ButtonSound.onClick.AddListener(Volume);

        UpdateVolumeSprite();
    }

    public void l1()
    {
        SceneManager.LoadScene("LEVEL1");
    }

    public void l2()
    {
        SceneManager.LoadScene("LEVEL2");
    }

    public void l3()
    {
        SceneManager.LoadScene("LEVEL3");
    }

    public void boss()
    {
        SceneManager.LoadScene("BOSS1");
    }
    public void Volume()
    {
        isMuted = !isMuted;

        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.mute = isMuted;
        }

        UpdateVolumeSprite();
    }

    private void UpdateVolumeSprite()
    {
        ButtonSound.image.sprite = isMuted ? muteSprite : unmuteSprite;
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
