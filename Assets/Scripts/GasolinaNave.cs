using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Este script maneja el sistema de gasolina de la nave, así como el Game Over en caso de quedarse sin gasolina.
public class GasolinaNave : MonoBehaviour
{
    public float maxGasolina = 100.0f; // La cantidad máxima de gasolina.
    public float gasolinaDisminuirRate = 2.0f; // La velocidad a la que disminuye la gasolina.
    public Text gasolinaText; // Referencia al texto que muestra la gasolina.
    public GameObject gameOverUI; // Referencia al objeto de Game Over UI.
    public AudioClip gameOverSound; // Sonido de Game Over al quedarse sin gasolina.
    public AudioClip sonidoGas; // Sonido al recoger gasolina.

    public ParticleSystem explosionParticles; // Partícula de explosión en caso de Game Over.

    public float gasolinaActual; // La cantidad actual de gasolina.
    private Nave nave; // Referencia al script de la nave.
    private AudioSource audioSource; // AudioSource de la nave.
    private ParticleSystem fuego1; // Partícula de fuego de la nave.
    private ParticleSystem fuego2; // Otra partícula de fuego de la nave.
    private MeshRenderer naveRenderer; // Renderer de la nave.
    private bool gasolinaVacia = false; // Indicador de si la gasolina se ha agotado.
    private bool gameEnded = false; // Indicador de si el juego ha terminado.

    // Método que se llama al inicio de la ejecución.
    void Start()
    {
        gasolinaActual = maxGasolina; // Inicializar la gasolina al máximo.
        UpdateGasolinaText(); // Actualizar el texto de gasolina.

        nave = GetComponent<Nave>(); // Obtener el script de la nave.
        audioSource = GetComponent<AudioSource>(); // Obtener el AudioSource.

        // Asignar las partículas de fuego si están disponibles.
        if (nave.fuego1 != null)
        {
            fuego1 = nave.fuego1;
            fuego1.Stop();
        }

        if (nave.fuego2 != null)
        {
            fuego2 = nave.fuego2;
            fuego2.Stop();
        }

        // Detener las partículas de explosión al inicio.
        if (explosionParticles != null)
        {
            explosionParticles.Stop();
        }

        naveRenderer = GetComponent<MeshRenderer>(); // Obtener el Renderer de la nave.
        audioSource.clip = nave.flyingEngineSound; // Asignar el sonido del motor de vuelo.
    }

    // Método que se llama en cada cuadro.
    void Update()
    {
        // Si el juego no ha terminado y la nave está en movimiento, disminuir la gasolina.
        if (!gameEnded && nave.isMoving)
        {
            DisminuirGasolina();
        }
    }

    // Método para disminuir la gasolina con el tiempo.
    void DisminuirGasolina()
    {
        gasolinaActual -= gasolinaDisminuirRate * Time.deltaTime;
        gasolinaActual = Mathf.Clamp(gasolinaActual, 0f, maxGasolina);
        UpdateGasolinaText();

        // Si la gasolina llega a cero, manejar el Game Over.
        if (gasolinaActual <= 0f)
        {
            HandleGasolinaEmpty();
        }
    }

    // Método para actualizar el texto que muestra la gasolina.
    void UpdateGasolinaText()
    {
        if (gasolinaText != null)
        {
            gasolinaText.text = $"{gasolinaActual:F0} / {maxGasolina:F0}";
        }
    }

    // Método para manejar el Game Over por falta de gasolina.
    void HandleGasolinaEmpty()
    {
        gasolinaVacia = true;
        nave.isMoving = false; // Detener la nave.

        // Actualizar el texto de gasolina a cero.
        gasolinaText.text = "0 / " + maxGasolina.ToString("F0");

        // Mostrar el Game Over UI si está disponible.
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Detener el sonido del motor de vuelo si está reproduciéndose.
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Reproducir el sonido de Game Over si está disponible.
        if (gameOverSound != null)
        {
            audioSource.PlayOneShot(gameOverSound);
        }

        // Reproducir la partícula de explosión si está disponible.
        if (explosionParticles != null)
        {
            explosionParticles.Play();
        }

        // Detener las partículas de fuego si están disponibles.
        if (fuego1 != null)
        {
            fuego1.Stop();
        }

        if (fuego2 != null)
        {
            fuego2.Stop();
        }

        // Deshabilitar el renderer de la nave.
        if (naveRenderer != null)
        {
            naveRenderer.enabled = false;
        }

        gameEnded = true; // Indicar que el juego ha terminado.

        enabled = false; // Deshabilitar este script después del Game Over.
    }

    // Método para manejar colisiones.
    void OnCollisionEnter(Collision collision)
    {
        // Si colisiona con un objeto de tag "Gasolina", aumentar la gasolina y reproducir el sonido.
        if (collision.gameObject.CompareTag("Gasolina"))
        {
            gasolinaActual += 20.0f;
            gasolinaActual = Mathf.Clamp(gasolinaActual, 0f, maxGasolina);
            UpdateGasolinaText();

            AudioSource audioSourceObjeto = collision.gameObject.GetComponent<AudioSource>();
            if (audioSourceObjeto != null)
            {
                audioSource.PlayOneShot(sonidoGas); // Reproducir el sonido del objeto de gasolina.
            }

            Destroy(collision.gameObject); // Destruir el objeto de gasolina.
        }
    }
}
