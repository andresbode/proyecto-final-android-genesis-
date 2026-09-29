using UnityEngine;

public class DronAtaque : MonoBehaviour
{
    public Transform jugador;
    public float distanciaAtaque = 10f;
    public Transform puntoDisparo;

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaAtaque)
        {
            RaycastHit hit;

            if (Physics.Raycast(puntoDisparo.position, puntoDisparo.forward, out hit, distanciaAtaque))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("¡Dron disparó al jugador!");
                }
            }
        }
    }
}