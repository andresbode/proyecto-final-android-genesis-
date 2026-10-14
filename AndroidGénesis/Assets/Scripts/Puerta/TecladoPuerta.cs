using UnityEngine;
using TMPro;

public class TecladoPuerta : MonoBehaviour
{
    public GameObject textoInteraccion;
    public TMP_Text txtCodigo;
    public GameObject panelTeclado;
    public JugadorMovimiento movimientoJugador;
    public PuertaCorrediza puerta;
    public string codigoCorrecto = "2607";
    public int largoMaximo = 4;

    private bool jugadorCerca = false;
    private bool panelAbierto = false;
    private string codigoIngresado = "";

    void Start()
    {
        ActualizarPantalla();
    }

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
        if (jugadorCerca && Input.GetKeyDown(KeyCode.E))
        {
            panelAbierto = !panelAbierto;
            panelTeclado.SetActive(panelAbierto);
            movimientoJugador.enabled = !panelAbierto;

            Cursor.visible = panelAbierto;
            Cursor.lockState = panelAbierto ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void AgregarNumero(string numero)
    {
        if (codigoIngresado.Length >= largoMaximo) return;

        codigoIngresado += numero;
        ActualizarPantalla();

        if (codigoIngresado.Length == largoMaximo)
        {
            if (codigoIngresado == codigoCorrecto)
            {
                puerta.Abrir();
                CerrarPanel();
            }
            else
            {
                codigoIngresado = "";
                ActualizarPantalla();
            }
        }
    }

    void CerrarPanel()
    {
        panelAbierto = false;
        panelTeclado.SetActive(false);
        movimientoJugador.enabled = true;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        codigoIngresado = "";
        ActualizarPantalla();
    }

    void ActualizarPantalla()
    {
        txtCodigo.text = codigoIngresado == "" ? "----" : codigoIngresado;
    }
}