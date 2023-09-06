using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Esta clase controla el comportamiento de colisi�n de un objeto con el tag "Choque".
public class Choque : MonoBehaviour
{
    // Variables p�blicas accesibles desde el inspector.
    public AudioClip explosionSound; // Sonido de explosi�n.
    public GameObject gameOverUI; // Referencia al objeto de la interfaz de Game Over.
    public ParticleSystem explosionParticles; // Part�cula de explosi�n.

    // Variables privadas para almacenar componentes y referencias.
    private MeshRenderer naveRenderer; // Referencia al MeshRenderer de la nave.
    private AudioSource audioSource; // Referencia al componente AudioSource.

    // M�todo que se llama al inicio de la ejecuci�n.
    void Start()
    {
        // Obtener el MeshRenderer y AudioSource adjuntos al objeto.
        naveRenderer = GetComponent<MeshRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // M�todo que se llama cuando ocurre una colisi�n con otro objeto.
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el objeto con el que colisionamos tiene el tag "Choque".
        if (collision.gameObject.CompareTag("Choque"))
        {
            // Detener el movimiento de la nave.
            GetComponent<Nave>().isMoving = false;

            // Detener el sonido de movimiento.
            audioSource.Stop();

            // Detener las part�culas de fuego de la nave.
            GetComponent<Nave>().fuego1.Stop();
            GetComponent<Nave>().fuego2.Stop();
            GetComponent<Nave>().fuego3.Stop();
            GetComponent<Nave>().fuego4.Stop();

            // Desactivar el Mesh Renderer de la nave para que no sea visible.
            naveRenderer.enabled = false;

            // Reproducir el sonido de explosi�n si se proporcion�.
            if (explosionSound != null)
            {
                audioSource.PlayOneShot(explosionSound);
            }

            // Reproducir la part�cula de explosi�n si se proporcion�.
            if (explosionParticles != null)
            {
                explosionParticles.Play();
            }

            // Mostrar la interfaz de Game Over si se proporcion�.
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true);
            }

            // Deshabilitar este script despu�s de la colisi�n para que no se ejecute nuevamente.
            enabled = false;
        }
    }
}
