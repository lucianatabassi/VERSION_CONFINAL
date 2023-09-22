using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGUA1_ENTREGAR : MonoBehaviour
{
  public GameObject objetoA;
    public GameObject objetoB;
    public GameObject objetoA2;
    public AudioClip sonidoMovimiento;

    private bool objetoAMovido = false;
    private bool objetosProcesados = false; // Bandera para evitar procesamiento repetido
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!objetosProcesados && (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1")))
        {
            // Comprueba si el objeto A está cerca del objeto B
            float distanciaAB = Vector3.Distance(objetoA.transform.position, objetoB.transform.position);

            if (distanciaAB < 1.0f)
            {
                // Destruye el objeto B
                Destroy(objetoA);

                // Mueve el objeto A2 a la posición de objeto A
                if (!objetoAMovido)
                {
                    objetoA2.transform.position = objetoA.transform.position;
                    objetoA2.transform.rotation = objetoA.transform.rotation;
                    objetoAMovido = true;

                    // Reproduce el sonido de movimiento
                    if (audioSource != null && sonidoMovimiento != null)
                    {
                        audioSource.PlayOneShot(sonidoMovimiento);
                    }
                }

                objetosProcesados = true; // Marcamos los objetos como procesados
            }
        }
    }
}