using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
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

    public void Jugar()
    {
        SceneManager.LoadScene("MenuNiveles");
    }

    public void All()
    {
        SceneManager.LoadScene("All");
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

}
