using UnityEngine;
using TMPro;                                   

public class JugadorVida : MonoBehaviour
{
    public float vida = 100f;
    public TMP_Text txtVida;                   

    void Start()                               
    {
        ActualizarTexto();
    }

    public void RecibirDanio(float danio)
    {
        vida -= danio;
        ActualizarTexto();                     

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ActualizarTexto()                    
    {
        if (txtVida != null)
            txtVida.text = "Vida: " + Mathf.Max(0, Mathf.CeilToInt(vida));
    }
}