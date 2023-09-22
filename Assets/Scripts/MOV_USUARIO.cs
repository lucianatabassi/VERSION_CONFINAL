using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOV_USUARIO : MonoBehaviour
{
    public CharacterController controller;
    public float vel = 10f;
    public float gravedad = -9.8f;
    private Vector3 mover;
 
    Vector3 velocity;
  
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        float x = Input.GetAxis("Horizontal"); //con esta linea de codigo unity ya sabe cuales son las teclas por defecto que se usan para moverse
        float z = Input.GetAxis("Vertical"); //lo de arriba

        mover = transform.right * x + transform.forward * z;

        Gravedad();

        controller.Move(mover * vel * Time.deltaTime);

    }

    void Gravedad()
    {
        mover.y = gravedad * Time.deltaTime;
    }
}
