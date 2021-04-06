using UnityEngine;

public class GestorDeCamaras : MonoBehaviour
{
    [SerializeField] private GameObject[] camaras;

    public void CambiarCamara(GameObject camaraAActivar)
    {
        //Debug.Log("CAMARA A ACTIVAR:" + camaraAActivar.name);

        foreach (GameObject camara in camaras)
        {
            if (camara.activeSelf)
            {
                //Debug.Log("CAMARA A DESACTIVAR: " + camara.name);
                camara.SetActive(false);
                camaraAActivar.SetActive(true);
                break;
            }
        }

        //Debug.Log("-------------CAMBIADA-------------");
    }
}
