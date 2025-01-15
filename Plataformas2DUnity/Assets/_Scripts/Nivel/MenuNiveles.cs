using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuNiveles : MonoBehaviour
{
    [SerializeField] private GameObject[] nivelesGO; // Array con los GameObjects de los niveles

    private AdministradorNiveles administrador;

    void Start()
    {
        administrador = FindObjectOfType<AdministradorNiveles>();
        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        for (int i = 0; i < nivelesGO.Length; i++)
        {
            if (i >= administrador.niveles.Count) continue;

            Nivel nivel = administrador.niveles[i];
            GameObject nivelGO = nivelesGO[i];

            // Configurar botón
            Button boton = nivelGO.GetComponentInChildren<Button>();
            boton.interactable = nivel.desbloqueado;

            int index = i; // Capturar índice para usar en el listener
            boton.onClick.AddListener(() => SeleccionarNivel(index));

            // Mostrar la puntuación más alta
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
        administrador.nivelActual = index;
        SceneManager.LoadScene(administrador.niveles[index].nombreNivel);
    }
}
