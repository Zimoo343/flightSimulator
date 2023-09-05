using System.Collections.Generic;
using UnityEngine;

public class Nave : MonoBehaviour
{
    // Velocidad de movimiento configurable en el inspector
    public float moveSpeed = 30.0f;

    // Velocidad de rotaci�n y multiplicador de rotaci�n configurables en el inspector
    public float rotationSpeed = 100.0f;
    public float rotationMultiplier = 2.0f;

    // Velocidad de inclinaci�n configurable en el inspector
    public float tiltSpeed = 5f;

    // Sonidos para el encendido y el vuelo
    public AudioClip startEngineSound;
    public AudioClip flyingEngineSound;

    // Variables para rastrear el estado de la nave y el movimiento
    public bool isPoweredOn = false;
    public bool isMoving = false;


    // Direcci�n inicial de la nave y rotaci�n inicial
    private Vector3 initialForwardDirection;
    private Quaternion initialRotation;

    // Componentes de audio y transformaci�n de la nave
    private AudioSource audioSource;
    private Transform naveTransform;

    // �ngulo de rotaci�n actual y rotaci�n objetivo
    private float currentRotation = 0.0f;
    private Quaternion targetRotation;

    // Referencia al sistema de part�culas en el inspector
    public ParticleSystem fuego1;
    public ParticleSystem fuego2;
    public ParticleSystem fuego3;
    public ParticleSystem fuego4;

    void Start()
    {
        // Guardar la direcci�n inicial de la nave y la transformaci�n inicial
        initialForwardDirection = transform.forward;
        naveTransform = transform;
        initialRotation = transform.rotation;
        targetRotation = initialRotation; // Inicializar la rotaci�n objetivo

        // Obtener el componente AudioSource adjunto a este objeto
        audioSource = GetComponent<AudioSource>();

        // Detener el sistema de part�culas al inicio para evitar su reproducci�n
        fuego1.Stop();
        fuego2.Stop();
        fuego3.Stop();
        fuego4.Stop();
    }

    void Update()
    {
        // Cambiar el estado de la nave al presionar la tecla "P"
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPoweredOn = !isPoweredOn;
            Debug.Log("Nave " + (isPoweredOn ? "encendida" : "apagada"));

            if (!isPoweredOn)
            {
                isMoving = false;
                audioSource.Stop(); // Detener el sonido de movimiento al apagar la nave
                fuego1.Stop(); // Detener las part�culas al apagar la nave
                fuego2.Stop(); // Detener las part�culas al apagar la nave
                fuego3.Stop(); // Detener las part�culas al apagar la nave
                fuego4.Stop(); // Detener las part�culas al apagar la nave
            }
            else
            {
                audioSource.PlayOneShot(startEngineSound); // Reproducir el sonido de encendido
            }
        }

        // Iniciar el movimiento y sonido al presionar la tecla "M"
        if (Input.GetKeyDown(KeyCode.M) && isPoweredOn && !isMoving)
        {
            isMoving = true;
            audioSource.clip = flyingEngineSound;
            audioSource.loop = true; // Reproducir el sonido en bucle
            audioSource.Play();
            fuego1.Play(); // Iniciar la emisi�n de part�culas al presionar "M"
            fuego2.Play(); // Iniciar la emisi�n de part�culas al presionar "M"
            fuego3.Play(); // Iniciar la emisi�n de part�culas al presionar "M"
            fuego4.Play(); // Iniciar la emisi�n de part�culas al presionar "M"
        }

        // Actualizar el movimiento y la rotaci�n mientras la nave est� en movimiento
        if (isMoving)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Mover hacia adelante autom�ticamente en relaci�n con la velocidad
            Vector3 moveDirection = transform.forward * moveSpeed * Time.fixedDeltaTime;
            transform.Translate(moveDirection, Space.World);

            // Actualizar la rotaci�n actual en funci�n del input horizontal
            currentRotation += horizontalInput * rotationSpeed * Time.fixedDeltaTime;

            // Calcular la rotaci�n objetivo en base a la rotaci�n actual
            targetRotation = Quaternion.Euler(0, currentRotation, 0);

            // Realizar una rotaci�n suave hacia la derecha o izquierda
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationMultiplier * Time.fixedDeltaTime);

            // Inclinar la nave en funci�n del input vertical y horizontal
            Quaternion targetTilt = Quaternion.Euler(verticalInput * 30, 0, -horizontalInput * 30);
            naveTransform.localRotation = Quaternion.Lerp(naveTransform.localRotation, targetTilt, tiltSpeed * Time.fixedDeltaTime);
        }
    }
}
