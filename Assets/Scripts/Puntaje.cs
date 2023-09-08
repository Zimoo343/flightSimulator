using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Este script maneja el sistema de puntuaci�n y victoria del juego.
public class Puntaje : MonoBehaviour
{
    public Text puntosText; // Referencia al objeto de texto para mostrar el puntaje.
    public AudioClip sonidoPowerUp; // Sonido del Power Up.
    public AudioClip victoriaSound; // Sonido de victoria.
    public GameObject victoriaUI; // Referencia al objeto de la interfaz de victoria.
    public GameObject winZone; // Objeto que representa la zona de victoria.

    private int puntos = 0; // Puntaje actual.
    private AudioSource audioSource; // Componente AudioSource para reproducir sonidos.

    public Nave nave; // Referencia a la nave del jugador.
    public GasolinaNave gasolinaNave; // Referencia al script de GasolinaNave para verificar la gasolina.

    private bool victoryAchieved = false; // Bandera para verificar si se logr� la victoria.

    // M�todo que se llama al inicio de la ejecuci�n.
    void Start()
    {
        // Obtener el componente AudioSource adjunto a este objeto.
        audioSource = GetComponent<AudioSource>();

        // Actualizar el texto del puntaje en el inicio.
        UpdatePuntosText();

        // Desactivar la WinZone al inicio del juego.
        if (winZone != null)
        {
            winZone.SetActive(false);
        }

        // Desactivar la interfaz de victoria al inicio del juego.
        if (victoriaUI != null)
        {
            victoriaUI.SetActive(false);
        }
    }

    // M�todo que se llama en cada cuadro de la ejecuci�n.
    void Update()
    {
        // Verificar si se han recolectado todos los puntos y a�n hay gasolina.
        if (puntos >= 10 && gasolinaNave.gasolinaActual > 0)
        {
            // Si se cumplen las condiciones y la victoria no ha sido lograda, activar la WinZone.
            if (!victoryAchieved)
            {
                victoryAchieved = true;
                if (winZone != null)
                {
                    winZone.SetActive(true);
                }
            }
        }
    }

    // M�todo para aumentar el puntaje.
    public void AumentarPuntos(int cantidad)
    {
        puntos += cantidad;
        UpdatePuntosText();

        // Reproducir el sonido del Power Up si est� disponible.
        if (audioSource != null && sonidoPowerUp != null)
        {
            audioSource.PlayOneShot(sonidoPowerUp);
        }
    }

    // M�todo para actualizar el texto del puntaje en la interfaz.
    void UpdatePuntosText()
    {
        if (puntosText != null)
        {
            puntosText.text = puntos.ToString() + " / 10";
        }
    }

    // M�todo que se llama cuando hay colisiones.
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si colisionamos con un objeto con el tag "PowerUp".
        if (collision.gameObject.CompareTag("credit"))
        {
            AumentarPuntos(1); // Aumentar el puntaje al colisionar con el Power Up.
            Destroy(collision.gameObject); // Destruir el objeto PowerUp.
        }
        // Verificar si colisionamos con un objeto con el tag "WinZone" y si se cumpli� la victoria.
        else if (collision.gameObject.CompareTag("WinZone") && victoryAchieved)
        {
            // Detener la nave y los efectos de movimiento.
            nave.isMoving = false;
            audioSource.Stop(); // Detener el sonido de movimiento.
            nave.fuego1.Stop(); // Detener las part�culas de fuego.
            nave.fuego2.Stop(); // Detener las part�culas de fuego.
            nave.fuego3.Stop(); // Detener las part�culas de fuego.
            nave.fuego4.Stop(); // Detener las part�culas de fuego.

            // Reproducir el sonido de victoria si est� disponible.
            if (victoriaSound != null)
            {
                audioSource.PlayOneShot(victoriaSound);
            }

            // Mostrar la interfaz de victoria si est� disponible.
            if (victoriaUI != null)
            {
                victoriaUI.SetActive(true);
            }
        }
    }
}
