using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    private bool jugadorVivo = true;
    public GameObject enemigo;
    public float tiempoParaSpawnearUnEnemigo = 5f;
    public Transform[] spawnPoints;

    private void Start()
    {
        // Repite la función "SpawnearEnemigo"
        InvokeRepeating("SpawnearEnemigo",
            1.5f, // Se llamará cuando pase este tiempo
            tiempoParaSpawnearUnEnemigo); // Se vuelve a llamar pasado este tiempo
    }

    private void SpawnearEnemigo()
    {
        if (jugadorVivo)
        {
            int spawnPointAleatorioParaSpawnear = 
                Random.Range(0, spawnPoints.Length);

            Instantiate(enemigo,
                spawnPoints[spawnPointAleatorioParaSpawnear].position,
                spawnPoints[spawnPointAleatorioParaSpawnear].rotation);
        }
    }
}
