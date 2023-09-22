using UnityEngine;

public class ObjetoMovible : MonoBehaviour
{
    public Transform destino; // El punto al que el objeto se moverá
    public float velocidad = 1.0f; // Velocidad de movimiento

    private void Update()
    {
        // Mueve el objeto hacia el destino a una velocidad constante
        transform.position = Vector3.MoveTowards(transform.position, destino.position, velocidad * Time.deltaTime);

        // Puedes agregar lógica adicional aquí, como destruir el objeto cuando llegue a su destino.
    }
}
