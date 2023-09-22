using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AGUA2_ENTREGAR : MonoBehaviour
{
   public GameObject objetoA2;
    public GameObject objetoC;
    public GameObject objetoD;
    public AudioClip sonidoAparicionD;

    private bool objetosDestruidos = false;
    private AudioSource audioSource;
   public bool abejacompletado = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                float distanciaAC = Vector3.Distance(objetoA2.transform.position, objetoC.transform.position);

                //Debug.Log("Distancia AC: " + distanciaAC);

                if (distanciaAC < 1.0f && !objetosDestruidos) // Verifica si no se han destruido los objetos antes
                {
                    Debug.Log("Condición cumplida. Destruyendo objetos.");

                    Destroy(objetoC);
                    Destroy(objetoA2);

                    objetosDestruidos = true;
                }
            }
        }

        if (objetosDestruidos)
        {
            // Verifica si objetoC es nulo antes de intentar acceder a su posición
            if (objetoC != null)
            {
                Instantiate(objetoD, objetoC.transform.position, objetoC.transform.rotation);
                abejacompletado=true;
            }
            objetosDestruidos = false;

            if (audioSource != null && sonidoAparicionD != null)
            {
                audioSource.PlayOneShot(sonidoAparicionD);
            }
        }
    }
}
