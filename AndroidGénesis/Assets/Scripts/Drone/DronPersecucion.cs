using UnityEngine;
using UnityEngine.AI;

public class DronPersecucion : MonoBehaviour
{
    public Transform jugador;

    private NavMeshAgent agent;

    public bool persiguiendo = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (persiguiendo)
        {
            agent.SetDestination(jugador.position);

            if (agent.velocity.magnitude > 0.1f)
            {
                transform.rotation = Quaternion.LookRotation(agent.velocity) * Quaternion.Euler(-90, 0, 0);
            }
        }
    }
}