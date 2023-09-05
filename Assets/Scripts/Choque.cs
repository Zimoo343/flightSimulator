using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase controla el comportamiento de colisión de un objeto con el tag "Choque".
public class Choque : MonoBehaviour
{
    // Variables públicas accesibles desde el inspector.
    public AudioClip explosionSound; // Sonido de explosión.
    public GameObject gameOverUI; // Referencia al objeto de la interfaz de Game Over.
    public ParticleSystem explosionParticles; // Partícula de explosión.

    // Variables privadas para almacenar componentes y referencias.
    private MeshRenderer naveRenderer; // Referencia al MeshRenderer de la nave.
    private AudioSource audioSource; // Referencia al componente AudioSource.

    // Método que se llama al inicio de la ejecución.
    void Start()
    {
        // Obtener el MeshRenderer y AudioSource adjuntos al objeto.
        naveRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Método que se llama cuando ocurre una colisión con otro objeto.
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionamos tiene el tag "Choque".
        if (collision.gameObject.CompareTag("Choque"))
        {
            // Detener el movimiento de la nave.
            GetComponent<Nave>().isMoving = false;

            // Detener el sonido de movimiento.
            audioSource.Stop();

            // Detener las partículas de fuego de la nave.
            GetComponent<Nave>().fuego1.Stop();
            GetComponent<Nave>().fuego2.Stop();

            // Desactivar el Mesh Renderer de la nave para que no sea visible.
            naveRenderer.enabled = false;

            // Reproducir el sonido de explosión si se proporcionó.
            if (explosionSound != null)
            {
                audioSource.PlayOneShot(explosionSound);
            }

            // Reproducir la partícula de explosión si se proporcionó.
            if (explosionParticles != null)
            {
                explosionParticles.Play();
            }

            // Mostrar la interfaz de Game Over si se proporcionó.
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }

            // Deshabilitar este script después de la colisión para que no se ejecute nuevamente.
            enabled = false;
        }
    }
}
