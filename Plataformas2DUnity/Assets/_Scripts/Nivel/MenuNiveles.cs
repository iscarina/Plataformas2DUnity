using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuNiveles : MonoBehaviour
{
    [SerializeField] private GameObject[] nivelesGO; // Array con los GameObjects de los niveles

    [Header("Audio")]
    [SerializeField] private Sprite muteSprite;
    [SerializeField] private Sprite unmuteSprite;
    [SerializeField] private Button ButtonSound;
    private AudioSource[] audioSources;
    private bool isMuted = false;

    void Start()
    {
        audioSources = FindObjectsOfType<AudioSource>();
        ButtonSound.onClick.AddListener(Volume);

        UpdateVolumeSprite();
        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        for (int i = 0; i < nivelesGO.Length; i++)
        {
            if (i >= AdministradorNiveles.Instance.niveles.Count) continue;

            Nivel nivel = AdministradorNiveles.Instance.niveles[i];
            GameObject nivelGO = nivelesGO[i];

            // Configurar bot�n
            Button boton = nivelGO.GetComponentInChildren<Button>();
            boton.interactable = nivel.desbloqueado;

            int index = i; // Capturar �ndice para usar en el listener
            boton.onClick.AddListener(() => SeleccionarNivel(index));

            // Mostrar la puntuaci�n m�s alta
            TMP_Text puntuacionText = nivelGO.transform.Find("Puntuacion/Text").GetComponent<TMP_Text>();
            puntuacionText.text = nivel.mejorTiempo > 0 ? $"High Score: {nivel.mejorTiempo:F2}" : "High Score: N/A";

            // Actualizar abanicos
            Transform abanicosParent = nivelGO.transform.Find("Abanicos");
            for (int j = 0; j < nivel.abanicosRecogidos.Length; j++)
            {
                Image abanico = abanicosParent.GetChild(j).GetComponent<Image>();
                abanico.color = nivel.abanicosRecogidos[j] ? Color.white : Color.black;
            }
        }
    }

    public void SeleccionarNivel(int index)
    {
        AdministradorNiveles.Instance.nivelActual = index;
        SceneManager.LoadScene(AdministradorNiveles.Instance.niveles[index].nombreNivel);
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
