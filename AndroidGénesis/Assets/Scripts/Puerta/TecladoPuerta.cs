using UnityEngine;
using TMPro;

public class TecladoPuerta : MonoBehaviour
{
    public GameObject textoInteraccion;
    public TMP_Text txtCodigo;              
    public int largoMaximo = 4;
    public GameObject panelTeclado;                  
    public JugadorMovimiento JugadorMovimiento;       
    private bool panelAbierto = false;               

    private bool jugadorCerca = false;
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
            JugadorMovimiento.enabled = !panelAbierto;

            Cursor.visible = panelAbierto;
            Cursor.lockState = panelAbierto ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }

    public void AgregarNumero(string numero)    
    {
        if (codigoIngresado.Length >= largoMaximo) return;

        codigoIngresado += numero;
        ActualizarPantalla();
    }

    void ActualizarPantalla()                  
    {
        txtCodigo.text = codigoIngresado == "" ? "----" : codigoIngresado;
    }
}