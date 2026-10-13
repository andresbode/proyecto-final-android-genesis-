using UnityEngine;

public class JugadorDisparo : MonoBehaviour
{
    public Camera camara;
    public float distanciaDisparo = 100f;
    public float danio = 25f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Debug.DrawRay(camara.transform.position, camara.transform.forward * distanciaDisparo, Color.green, 1f); 


            if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, distanciaDisparo))
            {
                Debug.Log("Disparo pegó en: " + hit.collider.name); 

                DronVida vidaDron = hit.collider.GetComponent<DronVida>();

                if (vidaDron != null)
                {
                    vidaDron.RecibirDanio(danio);
                }
            }
        }
    }
}
