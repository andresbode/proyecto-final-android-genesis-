using UnityEngine;
using UnityEngine.AI;

public class DronPersecucion : MonoBehaviour
{
    public Transform jugador;
    private NavMeshAgent agent;
    public float distanciaAtaque = 10f;
    public bool persiguiendo = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (persiguiendo)
        {
            float distancia = Vector3.Distance(transform.position, jugador.position);

            if (distancia <= distanciaAtaque)
            {
                agent.isStopped = true;

                Vector3 direccion = jugador.position - transform.position;

                transform.rotation = Quaternion.LookRotation(direccion) * Quaternion.Euler(-90, 0, 0);
            }
            else
            {
                agent.isStopped = false;
                agent.SetDestination(jugador.position);
            }

            if (agent.velocity.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity) * Quaternion.Euler(-90, 0, 0);
            }
        }
    }
}