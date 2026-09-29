using UnityEngine;

public class DronAtaque : MonoBehaviour
{
    public Transform jugador;
    public float distanciaAtaque = 10f;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1f;
    private float contadorDisparo = 0f;

    void Update()
    {
        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaAtaque)
        {
            contadorDisparo += Time.deltaTime;

            if (contadorDisparo >= tiempoEntreDisparos)
            {
                contadorDisparo = 0f;

                RaycastHit hit;

                Vector3 direccion = jugador.position - puntoDisparo.position;

                if (Physics.Raycast(puntoDisparo.position, direccion, out hit, distanciaAtaque))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("¡Dron disparó al jugador!");
                    }
                }
            }
        }
    }
}