using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverYSaltarYVida : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidad = 5f;

    [Header("Configuración de Salto")]
    [SerializeField] private float fuerzaSalto = 5f;

    [Header("Configuración de Vida")]
    [SerializeField] private float vida = 5f;

    // Variables de uso interno en el script
    private bool puedoSaltar = true;
    private bool saltando = false;
    private float moverHorizontal;

    // Variable para referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;

    // Código ejecutado cuando el objeto se activa en el nivel
    private void OnEnable()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Código ejecutado en cada frame del juego (Intervalo variable)
    void Update()
    {
        // Capturar la entrada horizontal
        moverHorizontal = Input.GetAxis("Horizontal");

        // Detectar si se presiona la barra espaciadora para saltar
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            miRigidbody2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            puedoSaltar = false;
            saltando = true;
        }

        // Verificar si el jugador sigue vivo
        if (!EstasVivo())
        {
            // Detener el movimiento y cualquier otra acción si el jugador muere
            velocidad = 0f;
            Debug.Log("GAME OVER");
        }
    }

    private void FixedUpdate()
    {
        // Mover el personaje horizontalmente si está vivo
        if (EstasVivo())
        {
            miRigidbody2D.velocity = new Vector2(moverHorizontal * velocidad, miRigidbody2D.velocity.y);
        }
    }

    // Código ejecutado cuando el jugador colisiona con otro objeto
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Permitir saltar de nuevo al tocar el suelo
        puedoSaltar = true;
        saltando = false;
    }

    // Método para modificar la vida del jugador
    public void ModificarVida(float puntos)
    {
        vida += puntos;
        Debug.Log("Vida actual: " + vida);
        Debug.Log(EstasVivo() ? "El jugador está vivo" : "El jugador ha muerto");
    }

    // Verifica si el jugador sigue vivo
    private bool EstasVivo()
    {
        return vida > 0;
    }

    // Detectar colisiones con objetos marcados como "Meta"
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Meta"))
        {
            Debug.Log("¡GANASTE!");
        }
    }
}
