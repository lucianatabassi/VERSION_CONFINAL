using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VENDA_INTERACCION : MonoBehaviour
{
    public GameObject hoja;
    public Transform mano;
    public GameObject venda;
    public GameObject parentObject;
    private GameObject miVenda;
    

    private bool agarroHoja = false;
    public bool joystick = false;


    public AudioClip sonidoAgarrar;
    public AudioSource audioSource;


    void Start()
    {
       // venda.SetActive(false);

    }
    void Update()
    {


        if (!agarroHoja && Input.GetKeyDown("mouse 0") && enRango(hoja)) //DESCOMENTALO PARA PC Y COMENTALO PARA APK
        //if (!agarroHoja &&  Input.GetButtonDown("Fire1") && enRango(hoja)) //DESCOMENTALO PARA APK COMENTALO PARA PC 
        {
            Destroy(hoja);
            agarroHoja = true;
            audioSource.PlayOneShot(sonidoAgarrar);
        }


        if (agarroHoja && venda != null)
        {

            venda.transform.SetParent(mano); // La flor es hija de la mano
            venda.transform.position = mano.position;
            venda.GetComponent<Rigidbody>().isKinematic = true;
            // venda.SetActive(true);
            joystick = true;
            // venda.tag = "Venda";

        }




    }



    private bool enRango(GameObject obj)
    {
        float distance = Vector3.Distance(obj.transform.position, transform.position);
        return distance <= 5f; // Adjust the range as needed
    }





}
