using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script maneja la generación aleatoria de objetos en un espacio limitado.
public class GeneradorGasolina : MonoBehaviour
{
    public GameObject prefabObjeto; // Prefab del objeto a generar.
    public int cantidadObjetos = 10; // Cantidad de objetos a generar.
    public GameObject cuboSinMeshRenderer; // Cubo que define el límite del espacio.
    public AudioClip sonidoPrefab; // AudioClip del sonido que se reproducirá al generar el objeto.

    private Bounds limiteEspacio; // Área en la que se generan los objetos.

    // Método que se llama al inicio de la ejecución.
    void Start()
    {
        // Verificar si se ha asignado el cubo sin Mesh Renderer para definir el límite del espacio.
        if (cuboSinMeshRenderer != null)
        {
            limiteEspacio = new Bounds(cuboSinMeshRenderer.transform.position, cuboSinMeshRenderer.transform.localScale);
            GenerarObjetosAleatorios();
        }
        else
        {
            Debug.LogError("El cubo sin Mesh Renderer no está asignado.");
        }
    }

    // Método para generar objetos aleatorios en el espacio definido.
    void GenerarObjetosAleatorios()
    {
        for (int i = 0; i < cantidadObjetos; i++)
        {
            // Generar una posición aleatoria dentro del límite del espacio.
            Vector3 posicionAleatoria = new Vector3(
                Random.Range(limiteEspacio.min.x, limiteEspacio.max.x),
                Random.Range(limiteEspacio.min.y, limiteEspacio.max.y),
                Random.Range(limiteEspacio.min.z, limiteEspacio.max.z)
            );

            // Instanciar el objeto generado en la posición aleatoria.
            GameObject objetoGenerado = Instantiate(prefabObjeto, posicionAleatoria, Quaternion.identity);

            // Asignar el AudioClip al AudioSource del objeto generado si está disponible.
            AudioSource audioSourceObjeto = objetoGenerado.GetComponent<AudioSource>();
            if (audioSourceObjeto != null)
            {
                audioSourceObjeto.clip = sonidoPrefab; // Asignar el sonido al AudioSource.
            }
        }
    }
}
