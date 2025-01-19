using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControladorNivel : MonoBehaviour
{
    public static ControladorNivel Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI timeTMP;
    [SerializeField] private GameObject pauseMenu;

    //Volume
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unmuteSprite;
    [SerializeField] private Button ButtonSound;

    private bool isMuted = false;

    private float tiempoActual = 0;
    private float score = 100000;
    private bool nivelCompletado;

    private bool isPaused = false;

    private AudioSource[] audioSources;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1f;
        timeTMP.text = "Time: " + tiempoActual + " s";
        audioSources = FindObjectsOfType<AudioSource>();

        ButtonSound.onClick.AddListener(Volume);

        UpdateVolumeSprite();
    }

    public void Return()
    {
        SceneManager.LoadScene("MenuNiveles");
    }

    public void Exit()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        if (!nivelCompletado)
        {
            tiempoActual += Time.deltaTime;
            score = score - tiempoActual * 2;

            timeTMP.text = tiempoActual.ToString("F2") + " s";
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseMenu.SetActive(false);
    }

    public void CompletarNivel()
    {
        nivelCompletado = true;
        AdministradorNiveles.Instance.CompletarNivel(tiempoActual);

        UnityEngine.SceneManagement.SceneManager.LoadScene("MenuNiveles");
    }

    public void RecogerAbanico(int abanicoindex)
    {
        AdministradorNiveles.Instance.RecogerAbanico(abanicoindex);
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
