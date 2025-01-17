using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Nivel
{
    public string nombreNivel;
    public bool desbloqueado;
    public bool[] abanicosRecogidos; // Indica qué estrellas se han recogido en este nivel
    public float mejorTiempo;
}

public class AdministradorNiveles : MonoBehaviour
{
    public List<Nivel> niveles;
    public int nivelActual;

    public static AdministradorNiveles Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Asignar esta instancia
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Destruir esta instancia duplicada
        }
        CargarProgreso();
        DontDestroyOnLoad(gameObject);
    }

    public void CompletarNivel(float tiempo)
    {
        Nivel nivel = niveles[nivelActual];
        nivel.mejorTiempo = Mathf.Min(nivel.mejorTiempo == 0 ? tiempo : nivel.mejorTiempo, tiempo);

        if (nivelActual + 1 < niveles.Count)
        {
            niveles[nivelActual + 1].desbloqueado = true;
        }

        GuardarProgreso();
    }

    public void RecogerAbanico(int abanicoIndex)
    {
        Debug.Log(abanicoIndex);
        niveles[nivelActual].abanicosRecogidos[abanicoIndex] = true;
        GuardarProgreso();
    }

    public void GuardarProgreso()
    {
        for (int i = 0; i < niveles.Count; i++)
        {
            string claveEstrellas = "Nivel_" + i + "_Estrellas";
            string estrellas = string.Join(",", niveles[i].abanicosRecogidos);
            PlayerPrefs.SetString(claveEstrellas, estrellas);

            PlayerPrefs.SetFloat("Nivel_" + i + "_Tiempo", niveles[i].mejorTiempo);
            PlayerPrefs.SetInt("Nivel_" + i + "_Desbloqueado", niveles[i].desbloqueado ? 1 : 0);
        }
        PlayerPrefs.Save();
    }

    public void CargarProgreso()
    {
        for (int i = 0; i < niveles.Count; i++)
        {
            string claveEstrellas = "Nivel_" + i + "_Estrellas";
            string estrellasGuardadas = PlayerPrefs.GetString(claveEstrellas, "");

            niveles[i].abanicosRecogidos = string.IsNullOrEmpty(estrellasGuardadas)
                ? new bool[3] // Por defecto, 3 estrellas por nivel
                : Array.ConvertAll(estrellasGuardadas.Split(','), bool.Parse);

            niveles[i].mejorTiempo = PlayerPrefs.GetFloat("Nivel_" + i + "_Tiempo", 0);
            niveles[i].desbloqueado = PlayerPrefs.GetInt("Nivel_" + i + "_Desbloqueado", i == 0 ? 1 : 0) == 1;
        }
    }
}
