using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CUENCO_AGARRAR : MonoBehaviour
{public GameObject flor;
    public Transform mano;

    private bool activo;
    private bool sonidoReproducido; // Bandera para controlar si el sonido ya se reprodujo.

    public AudioClip sonidoAgarrar;
    public AudioSource audioSource;

    void Update()
    {
        if (activo == true)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1")) // Agarra con el botón izquierdo del ratón o un botón del joystick
            {
                flor.transform.SetParent(mano); // La flor es hija de la mano
                flor.transform.position = mano.position;
                flor.GetComponent<Rigidbody>().isKinematic = true;

                // Reproduce el sonido de agarre solo si no se ha reproducido aún
                if (!sonidoReproducido)
                {
                    audioSource.PlayOneShot(sonidoAgarrar);
                    sonidoReproducido = true; // Marca que el sonido se ha reproducido
                }
            }
        }
        else
        {
            // Resetea la bandera si el objeto no está activo
            sonidoReproducido = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Si el jugador entra en el área de detección, puede agarrar la flor
        {
            activo = true;
            Debug.Log("aaa");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") // Si sale del área de detección, ya no puede agarrar la flor
        {
            activo = false;
        }
    }
}