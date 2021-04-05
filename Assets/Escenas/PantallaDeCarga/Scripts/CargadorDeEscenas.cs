using UnityEngine.SceneManagement;

public static class CargadorDeEscenas
{
    public static string escenaACargar;

    public static void CargarEscena(string nombreEscenaACargar)
    {
        escenaACargar = nombreEscenaACargar;

        // Iremos a la pantalla de carga y ella terminará el proceso de carga de la escena que se requiere.
        SceneManager.LoadScene("PantallaDeCarga");
    }
}
