using UnityEngine;

public class JugadorVida : MonoBehaviour
{
    public float vida = 100f;

    public void RecibirDanio(float danio)
    {
        vida -= danio;
        Debug.Log("Vida restante: " + vida); // <-- línea nueva

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}