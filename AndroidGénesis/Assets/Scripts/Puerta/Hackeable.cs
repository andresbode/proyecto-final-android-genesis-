using UnityEngine;

public class Hackeable : MonoBehaviour
{
    public GameObject textoInteraccion;
    public GameObject barraMiniJuego;
    public RectTransform punteroBarra;
    public RectTransform zonaObjetivo;
    public PuertaCorrediza puertaIzquierda;
    public PuertaCorrediza puertaDerecha;
    public JugadorMovimiento movimientoJugador;
    public JugadorVida jugadorVida;
    public float danioPorFallo = 20f;
    public float anchoBarra = 500f;
    public float velocidad = 400f;
    private bool hackeada = false;

    private bool jugadorCerca = false;
    private bool minijuegoActivo = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            textoInteraccion.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            textoInteraccion.SetActive(false);
        }
    }

    void Update()
    {
        if (jugadorCerca && !minijuegoActivo && !hackeada && Input.GetKeyDown(KeyCode.E))
        {
            minijuegoActivo = true;
            barraMiniJuego.SetActive(true);
            movimientoJugador.enabled = false;
        }

        if (minijuegoActivo)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Frenar();
            }
            else
            {
                float x = Mathf.PingPong(Time.time * velocidad, anchoBarra) - anchoBarra / 2f;
                punteroBarra.anchoredPosition = new Vector2(x, 0);
            }
        }
    }

    void Frenar()
    {
        minijuegoActivo = false;

        float distancia = Mathf.Abs(punteroBarra.anchoredPosition.x - zonaObjetivo.anchoredPosition.x);
        bool acierto = distancia <= zonaObjetivo.rect.width / 2f;

        barraMiniJuego.SetActive(false);

        if (acierto)
        {
            hackeada = true;
            textoInteraccion.SetActive(false);
            puertaIzquierda.Abrir();
            puertaDerecha.Abrir();

        }
        else
        {
            jugadorVida.RecibirDanio(danioPorFallo);   
        }

        Invoke(nameof(ReactivarJugador), 0.1f);
    }
    void ReactivarJugador()
    {
        movimientoJugador.enabled = true;
    }
}



