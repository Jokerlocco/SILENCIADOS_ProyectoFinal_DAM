using UnityEngine;

public class OjoZesimov : MonoBehaviour
{
    private Animator animacion;

    private void Start()
    {
        animacion = gameObject.GetComponent<Animator>();
    }

    public void MostrarOjoZesimov()
    {
        animacion.SetTrigger("MostrarOjo");
    }

    public void OcultarOjoZesimov() // Es llamado desde la animación de mostrar el ojo
    {
        animacion.SetTrigger("OcultarOjo");
    }
}
