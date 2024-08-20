using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Configuracion de Seguimiento")]
    [SerializeField] float velocidadSeguimiento = 5f;
    [SerializeField] Transform jugador;

    // referenciar otro componente del objeto
    private Rigidbody2D miRigidbody2D;
    private Vector2 direccionSeguimiento;

    private void Awake()
    {
        miRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Dirección hacia el jugador
        direccionSeguimiento = (jugador.position - transform.position).normalized;

        // Movimiento hacia el jugador
        miRigidbody2D.MovePosition(miRigidbody2D.position + direccionSeguimiento * (velocidadSeguimiento * Time.fixedDeltaTime));
    }
}