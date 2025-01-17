using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuNiveles : MonoBehaviour
{
    [SerializeField] private GameObject[] nivelesGO; // Array con los GameObjects de los niveles

    void Start()
    {
        ActualizarInterfaz();
    }

    void ActualizarInterfaz()
    {
        for (int i = 0; i < nivelesGO.Length; i++)
        {
            if (i >= AdministradorNiveles.Instance.niveles.Count) continue;

            Nivel nivel = AdministradorNiveles.Instance.niveles[i];
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
        AdministradorNiveles.Instance.nivelActual = index;
        SceneManager.LoadScene(AdministradorNiveles.Instance.niveles[index].nombreNivel);
    }
}
