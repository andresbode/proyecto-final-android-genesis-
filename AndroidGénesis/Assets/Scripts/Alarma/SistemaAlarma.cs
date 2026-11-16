using UnityEngine;

public class SistemaAlarma : MonoBehaviour
{
    public GameObject[] refuerzos;
    public bool alarmaActiva = false;

    [ContextMenu("Activar alarma")]
    public void Activar()
    {
        if (alarmaActiva) return;
        alarmaActiva = true;
        Debug.Log("¡ALARMA ACTIVADA!");

        foreach (GameObject dron in refuerzos)
        {
            if (dron == null) continue;

            DronPatrulla patrulla = dron.GetComponent<DronPatrulla>();
            if (patrulla != null) patrulla.patrullando = false;

            DronPersecucion persecucion = dron.GetComponent<DronPersecucion>();
            if (persecucion != null) persecucion.persiguiendo = true;
        }
    }
}