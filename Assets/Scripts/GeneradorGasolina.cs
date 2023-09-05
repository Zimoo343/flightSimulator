using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Este script maneja la generaci�n aleatoria de objetos en un espacio limitado.
public class GeneradorGasolina : MonoBehaviour
{
    public GameObject prefabObjeto; // Prefab del objeto a generar.
    public int cantidadObjetos = 10; // Cantidad de objetos a generar.
    public GameObject cuboSinMeshRenderer; // Cubo que define el l�mite del espacio.
    public AudioClip sonidoPrefab; // AudioClip del sonido que se reproducir� al generar el objeto.

    private Bounds limiteEspacio; // �rea en la que se generan los objetos.

    // M�todo que se llama al inicio de la ejecuci�n.
    void Start()
    {
        // Verificar si se ha asignado el cubo sin Mesh Renderer para definir el l�mite del espacio.
        if (cuboSinMeshRenderer != null)
        {
            limiteEspacio = new Bounds(cuboSinMeshRenderer.transform.position, cuboSinMeshRenderer.transform.localScale);
            GenerarObjetosAleatorios();
        }
        else
        {
            Debug.LogError("El cubo sin Mesh Renderer no est� asignado.");
        }
    }

    // M�todo para generar objetos aleatorios en el espacio definido.
    void GenerarObjetosAleatorios()
    {
        for (int i = 0; i < cantidadObjetos; i++)
        {
            // Generar una posici�n aleatoria dentro del l�mite del espacio.
            Vector3 posicionAleatoria = new Vector3(
                Random.Range(limiteEspacio.min.x, limiteEspacio.max.x),
                Random.Range(limiteEspacio.min.y, limiteEspacio.max.y),
                Random.Range(limiteEspacio.min.z, limiteEspacio.max.z)
            );

            // Instanciar el objeto generado en la posici�n aleatoria.
            GameObject objetoGenerado = Instantiate(prefabObjeto, posicionAleatoria, Quaternion.identity);

            // Asignar el AudioClip al AudioSource del objeto generado si est� disponible.
            AudioSource audioSourceObjeto = objetoGenerado.GetComponent<AudioSource>();
            if (audioSourceObjeto != null)
            {
                audioSourceObjeto.clip = sonidoPrefab; // Asignar el sonido al AudioSource.
            }
        }
    }
}
