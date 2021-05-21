using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [SerializeField] GameObject[] enemigos;
    [SerializeField] float tiempoParaSpawnearUnEnemigo = 0f;
    [SerializeField] Transform[] spawnPoints;
    private GameObject enemigoAleatorioASpawnear;

    private int numEnemigosMax = 15;
    private int numEnemigosExistentes = 0;

    // Limitamos las apariciones de los caballos y torres:
    private int numAlfilesMax = 3;
    private int numAlfilesExistentes = 0;
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
                UnityEngine.Random.Range(0, spawnPoints.Length);
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
            enemigos[UnityEngine.Random.Range(0, enemigos.Length)];
        numEnemigosExistentes++;

        if (enemigoAleatorioASpawnear.CompareTag("EnemigoAlfil"))
        {
            if (numAlfilesExistentes >= numAlfilesMax)
            {
                GameObject enemigoAleatorioASpawnearAux = 
                    enemigoAleatorioASpawnear;

                while (enemigoAleatorioASpawnearAux.CompareTag("EnemigoAlfil"))
                {
                    enemigoAleatorioASpawnearAux =
                        enemigos[UnityEngine.Random.Range(0, enemigos.Length)];
                }

                enemigoAleatorioASpawnear = enemigoAleatorioASpawnearAux;
            }
            else
            {
                numAlfilesExistentes++;
            }
        }

        if (enemigoAleatorioASpawnear.CompareTag("EnemigoCaballo"))
        {
            if (numCaballosExistentes >= numCaballosMax)
            {
                GameObject enemigoAleatorioASpawnearAux =
                    enemigoAleatorioASpawnear;

                while (enemigoAleatorioASpawnearAux.CompareTag("EnemigoCaballo"))
                {
                    enemigoAleatorioASpawnearAux =
                        enemigos[UnityEngine.Random.Range(0, enemigos.Length)];
                }

                enemigoAleatorioASpawnear = enemigoAleatorioASpawnearAux;
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
                GameObject enemigoAleatorioASpawnearAux =
                    enemigoAleatorioASpawnear;

                while (enemigoAleatorioASpawnearAux.CompareTag("EnemigoTorre"))
                {
                    enemigoAleatorioASpawnearAux = 
                        enemigos[UnityEngine.Random.Range(0, enemigos.Length)];
                }

                enemigoAleatorioASpawnear = enemigoAleatorioASpawnearAux;
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

        if (tagDelEnemigoDespawneado == "EnemigoAlfil")
        {
            numAlfilesExistentes--;
        }

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
