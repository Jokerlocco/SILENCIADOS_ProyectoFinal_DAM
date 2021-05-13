using UnityEngine;

public class SangreEnPantalla : MonoBehaviour
{
    private Animator animacion;

    private void Start()
    {
        animacion = gameObject.GetComponent<Animator>();
    }

    public void MostrarSangreEnPantalla()
    {
        animacion.SetTrigger("MostrarSangreEnPantalla");
    }

    public void OcultarSangreEnPantalla() // Es llamado desde la animación de mostrar la sangre en pantalla
    {
        animacion.SetTrigger("OcultarSangreEnPantalla");
    }
}
