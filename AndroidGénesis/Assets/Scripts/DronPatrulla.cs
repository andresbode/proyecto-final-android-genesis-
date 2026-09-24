using UnityEngine;
using UnityEngine.AI;

public class DronPatrulla : MonoBehaviour
{
    public Transform[] puntosPatrulla;

    private NavMeshAgent agent;
    private int indiceActual = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.updateRotation = false;

        IrAlSiguientePunto();
    }

    void Update()
    {
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            indiceActual++;

            if (indiceActual >= puntosPatrulla.Length)
            {
                indiceActual = 0;
            }

            IrAlSiguientePunto();
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity) * Quaternion.Euler(-90, 0, 0);
        }
    }

    void IrAlSiguientePunto()
    {
        agent.SetDestination(puntosPatrulla[indiceActual].position);
    }
}