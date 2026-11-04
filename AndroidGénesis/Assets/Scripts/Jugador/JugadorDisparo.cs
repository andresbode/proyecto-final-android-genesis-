using UnityEngine;
using TMPro;                                      

public class JugadorDisparo : MonoBehaviour
{
    public Camera camara;
    public float distanciaDisparo = 100f;
    public float danio = 25f;

    public int tamanoTambor = 6;                   
    public int balasEnTambor = 6;                  
    public int balasReserva = 12;                 
    public TMP_Text txtBalas;                      

    void Start()                                 
    {
        ActualizarTexto();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))          
        {
            Recargar();
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (balasEnTambor <= 0)                
            {
                Debug.Log("Sin balas, recargá con R");
                return;
            }

            balasEnTambor--;                       
            ActualizarTexto();                     

            RaycastHit hit;

            if (Physics.Raycast(camara.transform.position, camara.transform.forward, out hit, distanciaDisparo))
            {
                DronVida vidaDron = hit.collider.GetComponentInParent<DronVida>();

                if (vidaDron != null)
                {
                    vidaDron.RecibirDanio(danio);
                }
            }
        }
    }

    void Recargar()                                
    {
        int faltan = tamanoTambor - balasEnTambor;
        int aCargar = Mathf.Min(faltan, balasReserva);

        balasEnTambor += aCargar;
        balasReserva -= aCargar;
        ActualizarTexto();
    }

    public void AgregarMunicion(int cantidad)      
    {
        balasReserva += cantidad;
        ActualizarTexto();
    }

    void ActualizarTexto()                        
    {
        if (txtBalas != null)
            txtBalas.text = "Balas: " + balasEnTambor + " / " + balasReserva;
    }
}