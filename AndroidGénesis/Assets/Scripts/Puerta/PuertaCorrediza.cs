using UnityEngine;

public class PuertaCorrediza : MonoBehaviour
{
    public Vector3 desplazamiento = new Vector3(0, 0, 2f);
    public float velocidad = 2f;
    public bool abierta = false;

    private Vector3 posicionCerrada;
    private Vector3 posicionAbierta;

    void Start()
    {
        posicionCerrada = transform.localPosition;
        posicionAbierta = posicionCerrada + desplazamiento;
    }

    void Update()
    {
        Vector3 destino = abierta ? posicionAbierta : posicionCerrada;
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, destino, velocidad * Time.deltaTime);
    }

    public void Abrir()
    {
        abierta = true;
    }
}