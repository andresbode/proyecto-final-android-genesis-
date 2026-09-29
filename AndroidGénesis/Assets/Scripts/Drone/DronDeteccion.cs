using UnityEngine;

public class DronDeteccion : MonoBehaviour
{
    public float distanciaVision = 20f;
    public Transform puntoVision;
    public DronPatrulla patrulla;
    private DronPersecucion persecucion;

    void Start()
    {
        persecucion = GetComponent<DronPersecucion>();
    }

    void Update()
    {
        Vector3 origen = puntoVision.position;
        Vector3 direccion = patrulla.direccionMovimiento;

        RaycastHit hit;

        if (Physics.Raycast(origen, direccion, out hit, distanciaVision))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("¡Jugador detectado!");

                persecucion.persiguiendo = true;
                patrulla.patrullando = false;
            }
        }

        Debug.DrawRay(origen, direccion * distanciaVision, Color.red);
    }
}