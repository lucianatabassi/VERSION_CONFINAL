using UnityEngine;

public class SpawnerDeObjetos : MonoBehaviour
{
    public Transform[] puntosDestino; // Un array de puntos de destino que puedes configurar en el inspector
    public GameObject objetoPrefab; // El prefab que deseas instanciar
    public float tiempoMaximoDeSpawn = 5.0f; // Tiempo máximo para spawneear objetos
    public float tiempoEntreSpawns = 2.0f; // Tiempo entre cada spawn
    private float tiempoUltimoSpawn;
    private float tiempoInicio;

    private void Start()
    {
        tiempoUltimoSpawn = Time.time;
        tiempoInicio = Time.time;
    }

    private void Update()
    {
        // Verifica si ha pasado el tiempo máximo de spawn
        if (Time.time - tiempoInicio >= tiempoMaximoDeSpawn)
        {
            // Detiene el spawneo de objetos
            return;
        }

        // Verifica si es tiempo de spawneear un nuevo objeto
        if (Time.time - tiempoUltimoSpawn >= tiempoEntreSpawns)
        {
            SpawnObjeto();
            tiempoUltimoSpawn = Time.time;
        }
    }

    private void SpawnObjeto()
    {
        // Elige un punto de destino al azar del array
        Transform puntoDestino = puntosDestino[Random.Range(0, puntosDestino.Length)];

        // Instancia el objeto y configura su punto de destino
        GameObject nuevoObjeto = Instantiate(objetoPrefab, transform.position, Quaternion.identity);
        ObjetoMovible objetoMovible = nuevoObjeto.GetComponent<ObjetoMovible>();

        if (objetoMovible != null)
        {
            objetoMovible.destino = puntoDestino;
        }
    }
}
