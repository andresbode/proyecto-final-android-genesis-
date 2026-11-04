using UnityEngine;
using UnityEngine.AI;

public class DronPiranha : MonoBehaviour
{
    public Transform jugador;
    public float distanciaVision = 10f;
    public float anguloVision = 90f;
    public float alturaOjos = 1f;
    public float velocidadPersecucion = 6f;
    public float danioEmbestida = 20f;
    public float distanciaImpacto = 1.5f;
    public float distanciaEmbestida = 5f;
    public float velocidadEmbestida = 12f;

    private NavMeshAgent agent;
    private DronPatrulla patrulla;

    private bool detectado = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrulla = GetComponent<DronPatrulla>();
    }

    void Update()
    {
        if (jugador == null) return;

        if (detectado)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);   

            agent.speed = distancia <= distanciaEmbestida ? velocidadEmbestida : velocidadPersecucion;  
            agent.SetDestination(jugador.position);

            if (distancia <= distanciaImpacto)
            {
                JugadorVida vida = jugador.GetComponent<JugadorVida>();
                if (vida != null) vida.RecibirDanio(danioEmbestida);
                Destroy(gameObject);
            }
            return;
        }

        Vector3 origen = transform.position + Vector3.up * alturaOjos;
        Vector3 direccion = jugador.position - origen;

        if (direccion.magnitude > distanciaVision) return;

        Vector3 direccionPlana = new Vector3(direccion.x, 0, direccion.z);
        float angulo = Vector3.Angle(transform.forward, direccionPlana);
        if (angulo > anguloVision / 2f) return;

        Debug.DrawRay(origen, direccion.normalized * distanciaVision, Color.red, 1f);
        if (Physics.Raycast(origen, direccion, out RaycastHit hit, distanciaVision))
        {
            if (hit.collider.CompareTag("Player"))
            {
                detectado = true;
                patrulla.enabled = false;               
                agent.speed = velocidadPersecucion;     
                Debug.Log("Piraña detectó al jugador");
            }
        }
    }

    void OnDrawGizmos()
    {
        Vector3 origen = transform.position + Vector3.up * alturaOjos;
        float mitad = anguloVision / 2f;

        Vector3 izq = Quaternion.Euler(0, -mitad, 0) * transform.forward;
        Vector3 der = Quaternion.Euler(0, mitad, 0) * transform.forward;

        Gizmos.color = detectado ? Color.red : Color.yellow;
        Gizmos.DrawRay(origen, izq * distanciaVision);
        Gizmos.DrawRay(origen, der * distanciaVision);
        Gizmos.DrawLine(origen + izq * distanciaVision, origen + der * distanciaVision);
    }
}