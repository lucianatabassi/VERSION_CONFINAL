using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class prueba : MonoBehaviour
{
        public float walkSpeed = 5f;
        private CharacterController player;
        private Gyroscope gyro;

        private void Awake()
        {
            player = GetComponent<CharacterController>();

            // Comprobar si el dispositivo admite el giroscopio
          if (SystemInfo.supportsGyroscope)
            {
                gyro = Input.gyro;
                gyro.enabled = true; // Habilitamos el giroscopio

                // Ajusta la orientación inicial del objeto al giroscopio
                transform.rotation = Quaternion.Euler(90, 90, 0) * (new Quaternion(-gyro.attitude.x, -gyro.attitude.y, gyro.attitude.z, gyro.attitude.w));
            }
          /*   else
            {
                Debug.LogError("El dispositivo no es compatible con el giroscopio.");
            } */ // SI TE TIRA QUE EL GIROSCOPIO NO ES COMPATIBLE COMENTA TODO ESTE CONDICIONAL
        }

        private void Update()
        {
            // Obtén las entradas de movimiento del jugador
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Calcula la dirección de movimiento en relación con la cámara
            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraRight = Camera.main.transform.right;
            cameraForward.y = 0f; // Mantén la dirección horizontal
            cameraRight.y = 0f;   // Mantén la dirección horizontal
            Vector3 moveDirection = cameraForward.normalized * verticalInput + cameraRight.normalized * horizontalInput;

            // Mueve al jugador en la dirección calculada
            player.Move(moveDirection.normalized * walkSpeed * Time.deltaTime);

            if (gyro != null)
            {
                // Actualiza la rotación del objeto basada en los datos del giroscopio
                transform.rotation = Quaternion.Euler(90, 90, 0) * (new Quaternion(-gyro.attitude.x, -gyro.attitude.y, gyro.attitude.z, gyro.attitude.w));
            }
        }
    }
