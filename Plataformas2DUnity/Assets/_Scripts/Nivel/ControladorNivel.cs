using UnityEngine;

public class ControladorNivel : MonoBehaviour
{
    private float tiempoActual;
    private bool nivelCompletado;

    void Update()
    {
        if (!nivelCompletado)
        {
            tiempoActual += Time.deltaTime;
        }
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
}
