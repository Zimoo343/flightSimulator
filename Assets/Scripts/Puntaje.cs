using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Este script maneja el sistema de puntuación y victoria del juego.
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

    private bool victoryAchieved = false; // Bandera para verificar si se logró la victoria.

    // Método que se llama al inicio de la ejecución.
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

    // Método que se llama en cada cuadro de la ejecución.
    void Update()
    {
        // Verificar si se han recolectado todos los puntos y aún hay gasolina.
        if (puntos >= 1 && gasolinaNave.gasolinaActual > 0)
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

    // Método para aumentar el puntaje.
    public void AumentarPuntos(int cantidad)
    {
        puntos += cantidad;
        UpdatePuntosText();

        // Reproducir el sonido del Power Up si está disponible.
        if (audioSource != null && sonidoPowerUp != null)
        {
            audioSource.PlayOneShot(sonidoPowerUp);
        }
    }

    // Método para actualizar el texto del puntaje en la interfaz.
    void UpdatePuntosText()
    {
        if (puntosText != null)
        {
            puntosText.text = puntos.ToString() + " / 1";
        }
    }

    // Método que se llama cuando hay colisiones.
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si colisionamos con un objeto con el tag "PowerUp".
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            AumentarPuntos(1); // Aumentar el puntaje al colisionar con el Power Up.
            Destroy(collision.gameObject); // Destruir el objeto PowerUp.
        }
        // Verificar si colisionamos con un objeto con el tag "WinZone" y si se cumplió la victoria.
        else if (collision.gameObject.CompareTag("WinZone") && victoryAchieved)
        {
            // Detener la nave y los efectos de movimiento.
            nave.isMoving = false;
            audioSource.Stop(); // Detener el sonido de movimiento.
            nave.fuego1.Stop(); // Detener las partículas de fuego.
            nave.fuego2.Stop(); // Detener las partículas de fuego.

            // Reproducir el sonido de victoria si está disponible.
            if (victoriaSound != null)
            {
                audioSource.PlayOneShot(victoriaSound);
            }

            // Mostrar la interfaz de victoria si está disponible.
            if (victoriaUI != null)
            {
                victoriaUI.SetActive(true);
            }
        }
    }
}
