using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character_controller : MonoBehaviour
{
    private Rigidbody2D miRigidbody;

    [Header("Configuración del Rigidbody")]
    public float gravedadEscala = 1f;          // Escala de la gravedad
    public float velocidadMovimiento = 5f;     // Velocidad de movimiento
    public float fuerzaSalto = 10f;            // Fuerza del salto
    public bool freezeRotation = true;         // Congelar la rotación en el eje Z

    private bool puedoSaltar = true;

    // Se ejecuta al iniciar el juego
    void Start()
    {
        // Obtener el Rigidbody2D adjunto al GameObject
        miRigidbody = GetComponent<Rigidbody2D>();

        // Aplicar la configuración inicial del Rigidbody2D
        miRigidbody.gravityScale = gravedadEscala;
        miRigidbody.freezeRotation = freezeRotation;
    }

    // Se ejecuta en cada frame del juego
    void Update()
    {
        // Capturar la entrada horizontal (A/D o flechas izquierda/derecha)
        float movimientoHorizontal = Input.GetAxis("Horizontal");

        // Aplicar la velocidad de movimiento al Rigidbody2D
        miRigidbody.velocity = new Vector2(movimientoHorizontal * velocidadMovimiento, miRigidbody.velocity.y);

        // Detectar si se presiona la barra espaciadora para saltar
        if (Input.GetKeyDown(KeyCode.Space) && puedoSaltar)
        {
            // Aplicar fuerza de salto en el eje Y
            miRigidbody.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            puedoSaltar = false; // Deshabilitar salto hasta que toque el suelo nuevamente
        }
    }

    // Detectar colisiones para permitir saltar de nuevo
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión es con el suelo
        if (collision.contacts[0].normal.y > 0.5f)
        {
            puedoSaltar = true;
        }
    }
}