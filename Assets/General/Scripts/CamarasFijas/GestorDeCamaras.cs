using UnityEngine;

public class GestorDeCamaras : MonoBehaviour
{
    [SerializeField] private GameObject[] camaras;

    public void DesactivarTodasLasCamaras()
    {
        for (int numCamara = 0; numCamara < camaras.Length; numCamara++)
            camaras[numCamara].SetActive(false);
    }
}
