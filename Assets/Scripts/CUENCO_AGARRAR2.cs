using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CUENCO_AGARRAR2 : MonoBehaviour
{
    public GameObject objetoA2; // El objeto A2 que debe acercarse a objeto C.
    public GameObject objetoC; // El objeto C que debe ser destruido al hacer clic o presionar un botón del joystick.
    public GameObject objetoD; // El objeto D que aparecerá en lugar de objeto C.
    public AudioClip sonidoAparicionD; // El clip de sonido a reproducir cuando aparece el objeto D.

    private bool objetosDestruidos = false;
    private AudioSource audioSource;

    private void Start()
    {
        // Obtener el componente AudioSource adjunto al GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Verifica si se hizo clic izquierdo o se presionó un botón del joystick
        if (Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0)) // Ajusta el nombre del botón según tu configuración en Input Manager
        {
            // Raycast desde la cámara al puntero del mouse
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Realiza el raycast
            if (Physics.Raycast(ray, out hit))
            {
                // Comprueba si el objeto A2 está cerca del objeto C
                float distanciaAC = Vector3.Distance(objetoA2.transform.position, objetoC.transform.position);

                // Depuración: Muestra un mensaje en la consola para verificar la distancia.
                Debug.Log("Distancia AC: " + distanciaAC);

                if (distanciaAC < 1.0f) // Ajusta la distancia según tu necesidad
                {
                    // Depuración: Muestra un mensaje en la consola cuando se cumple la condición.
                    Debug.Log("Condición cumplida. Destruyendo objetos.");

                    // Destruye el objeto C
                    Destroy(objetoC);

                    // Destruye el objeto A2
                    Destroy(objetoA2);

                    // Marca que los objetos se han destruido
                    objetosDestruidos = true;
                }
            }
        }

        // Si ambos objetos A2 y C han sido destruidos, instanciar el objeto D en la posición de C
        if (objetosDestruidos)
        {
            Instantiate(objetoD, objetoC.transform.position, objetoC.transform.rotation);
            objetosDestruidos = false; // Reinicia la bandera para que no se repita la creación de D

            // Reproduce el sonido de aparición de D
            if (audioSource != null && sonidoAparicionD != null)
            {
                audioSource.PlayOneShot(sonidoAparicionD);
            }
        }
    }
}
