using UnityEngine;

public class DronVida : MonoBehaviour
{
    public float vida = 100f;

    public void RecibirDanio(float danio)
    {
        vida -= danio;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }
}