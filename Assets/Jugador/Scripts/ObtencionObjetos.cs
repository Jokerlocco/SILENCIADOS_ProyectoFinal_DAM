using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObtencionObjetos : MonoBehaviour
{
    public bool colisionando = false;

    private void OnTriggerEnter(Collider other)
    {
        colisionando = true;
    }

    private void OnTriggerExit(Collider other)
    {
        colisionando = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interactuar") && colisionando)
        {
            RecogerObjeto();
        }
    }

    private void RecogerObjeto()
    {
        if (gameObject.CompareTag("GlifoDeBronce"))
        {
            FindObjectOfType<Inventario>().GlifoBronceEnElInventario = true;
            gameObject.SetActive(false);
            Debug.Log("Glifo de bronce recogido");
        }
    }
}
