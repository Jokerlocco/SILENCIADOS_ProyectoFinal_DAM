using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [SerializeField] GameObject[] enemigos;
    [SerializeField] float tiempoParaSpawnearUnEnemigo = 5f;
    [SerializeField] Transform[] spawnPoints;
    private GameObject enemigoAleatorioASpawnear;

    private int numEnemigosMax = 15;
    private int numEnemigosExistentes = 0;

    // Limitamos las apariciones de los caballos y torres:
    private int numCaballosMax = 2;
    private int numCaballosExistentes = 0;
    private int numTorresMax = 2;
    private int numTorresExistentes = 0;

    private void Start()
    {
        // Repite la función "SpawnearEnemigo"
        InvokeRepeating("SpawnearEnemigo",
            0.8f, // Se llamará cuando pase este tiempo
            tiempoParaSpawnearUnEnemigo); // Se vuelve a llamar pasado este tiempo
    }

    private void SpawnearEnemigo()
    {
        if (FindObjectOfType<EstadoDelJugador>().JugadorVivo &&
            numEnemigosExistentes <= numEnemigosMax)
        {
            int spawnPointAleatorioParaSpawnear = 
                Random.Range(0, spawnPoints.Length);
            DeterminarEnemigoASpawnear();

            FindObjectOfType<OjoZesimov>().MostrarOjoZesimov();

            Instantiate(enemigoAleatorioASpawnear,
                spawnPoints[spawnPointAleatorioParaSpawnear].position,
                spawnPoints[spawnPointAleatorioParaSpawnear].rotation);
        }
    }

    private void DeterminarEnemigoASpawnear()
    {
        enemigoAleatorioASpawnear = 
            enemigos[Random.Range(0, enemigos.Length)];
        numEnemigosExistentes++;

        if (enemigoAleatorioASpawnear.CompareTag("EnemigoCaballo"))
        {
            if (numCaballosExistentes >= numCaballosMax)
            {
                while (enemigoAleatorioASpawnear.CompareTag("EnemigoCaballo"))
                {
                    enemigoAleatorioASpawnear =
                        enemigos[Random.Range(0, enemigos.Length)];
                }
            }
            else
            {
                numCaballosExistentes++;
            }
        }

        if (enemigoAleatorioASpawnear.CompareTag("EnemigoTorre"))
        {
            if (numTorresExistentes >= numTorresMax)
            {
                while (enemigoAleatorioASpawnear.CompareTag("EnemigoTorre"))
                {
                    enemigoAleatorioASpawnear = 
                        enemigos[Random.Range(0, enemigos.Length)];
                }
            }
            else
            {
                numTorresExistentes++;
            }
        }
    }

    public void ApuntarEnemigoDespawneado(string tagDelEnemigoDespawneado)
    {
        numEnemigosExistentes--;

        if (tagDelEnemigoDespawneado == "EnemigoCaballo")
        {
            numCaballosExistentes--;
        }
        
        if (tagDelEnemigoDespawneado == "EnemigoTorre")
        {
            numTorresExistentes--;
        }
    }
}
