using UnityEngine;

public class GestorDeCamaras : MonoBehaviour
{
    [SerializeField] private GameObject[] camaras;
    [SerializeField] private GameObject camaraOscura; // Con esto la transición de cámaras funciona mejor.

    public void CambiarCamara(GameObject camaraAActivar)
    {
        foreach (GameObject camara in camaras)
        {
            if (camara.activeSelf)
            {
                camara.SetActive(false);
                camaraOscura.SetActive(true);
                break;
            }
        }

        camaraOscura.SetActive(false);
        camaraAActivar.SetActive(true);
    }
}
