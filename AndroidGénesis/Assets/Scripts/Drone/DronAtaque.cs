using UnityEngine;

public class DronAtaque : MonoBehaviour
{
    public Transform jugador;
    public float distanciaAtaque = 10f;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1f;
    private float contadorDisparo = 0f;
    private DronPersecucion persecucion;

    void Start() 
    {
        persecucion = GetComponent<DronPersecucion>();
    }

    void Update()
    {
        if (jugador == null || !persecucion.persiguiendo) return;
        
        float distancia = Vector3.Distance(transform.position, jugador.position);

        if (distancia <= distanciaAtaque)
        {
            contadorDisparo += Time.deltaTime;

            if (contadorDisparo >= tiempoEntreDisparos)
            {
                contadorDisparo = 0f;

                RaycastHit hit;

                Vector3 direccion = jugador.position - puntoDisparo.position;
                Debug.DrawRay(puntoDisparo.position, direccion.normalized * distanciaAtaque, Color.red, 0.5f);
                if (Physics.Raycast(puntoDisparo.position, direccion, out hit, distanciaAtaque))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        Debug.Log("¡Dron disparó al jugador!");

                        JugadorVida vidaJugador = hit.collider.GetComponent<JugadorVida>();

                        if (vidaJugador != null)
                        {
                            vidaJugador.RecibirDanio(25f);
                        }
                    }
                }
            }
        }
    }
}