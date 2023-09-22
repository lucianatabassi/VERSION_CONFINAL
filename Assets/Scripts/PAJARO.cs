using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAJARO : MonoBehaviour
{
    public Transform[] waypoints; // Lista de puntos por los que el objeto se mover�
    public float speed = 5.0f; // Velocidad de movimiento del objeto
    public float rotationSpeed = 5.0f; // Velocidad de rotaci�n del objeto
    public float floatAmplitude = 0.5f; // Amplitud de la oscilaci�n vertical
    public float floatFrequency = 1.0f; // Frecuencia de la oscilaci�n vertical
    private int currentWaypointIndex = 0; // �ndice del punto actual
    public GameObject circulo;
    public GameObject corazon;
    public GameObject pajaro;
    public GameObject venda;
    public Camera miCamara;

    private bool pajaroCurado = false;
    private bool curarPajaro = false;

    public AudioClip sonidoDarVenda;  
    public AudioSource audioSource;

    private VENDA_INTERACCION scriptVenda;

    public bool pajaroCompletado = false;


    void Start()
    {
        corazon.SetActive(false);
        scriptVenda = FindObjectOfType<VENDA_INTERACCION>();
    }

  /* private void OnMouseDown()  //ESTO DESCOMENTALO PARA QUE FUNCIONE EN COMPU Y COMENTALO PARA EL APK
    {
        Destroy(GameObject.FindGameObjectWithTag("Venda"));
        pajaroCurado = true;
        sonidoCurar();
        Destroy(circulo);
        corazon.SetActive(true);
        Debug.Log("Pajaro curado");
        pajaroCompletado = true;
    }

    private void OnJoystickButtonDown()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f);


        foreach (Collider collider in colliders)
        {
            // Comprueba si el objeto que colisiona con la rama tiene una etiqueta "Moho".
            if (collider.CompareTag("Venda"))
            {
                // Si es moho, destr�yelo.
                pajaroCurado = true;
                sonidoCurar();
                Destroy(circulo);
                corazon.SetActive(true);
                //Debug.Log("Pajaro curado");
                pajaroCompletado = true;

            }
        }
    }*/

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Fire1")) // 0 representa el botón izquierdo del ratón
        {
            Ray ray = miCamara.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == pajaro) // Verifica si el rayo golpeó al objeto del pájaro
                {
                    // Aquí debes implementar la lógica para verificar si el objeto en la mano es el correcto y destruirlo si es así.
                    Destroy(GameObject.FindGameObjectWithTag("Venda"));
                    pajaroCurado = true;
                    sonidoCurar();
                    Destroy(circulo);
                    corazon.SetActive(true);
                    Debug.Log("Pajaro curado");
                    pajaroCompletado = true;
                }
            }
        }

        if (pajaroCurado)
        {
            if (currentWaypointIndex < waypoints.Length)
            {
                Vector3 targetPosition = waypoints[currentWaypointIndex].position;

                // Mover el objeto hacia el punto objetivo
                float step = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

                // Oscilaci�n vertical
                float verticalOffset = Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;
                Vector3 floatOffset = Vector3.up * verticalOffset;
                transform.position += floatOffset;

                // Rotar el objeto hacia el punto objetivo
                Vector3 directionToTarget = targetPosition - transform.position;
                if (directionToTarget != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                }

                // Comprobar si el objeto est� lo suficientemente cerca del punto objetivo
                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    currentWaypointIndex++; // Avanzar al siguiente punto
                }
            }
            else
            {
                // Si se han recorrido todos los puntos, reiniciar el circuito
                currentWaypointIndex = 0;
            }
        }
        
    }

    private bool enRango(GameObject obj)
    {
        float distance = Vector3.Distance(obj.transform.position, transform.position);
        return distance <= 3f; // Adjust the range as needed
    }

    private void sonidoCurar()
    {
        if (pajaroCurado && sonidoDarVenda != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoDarVenda);
            Debug.Log("AA");
        }
    }


}


