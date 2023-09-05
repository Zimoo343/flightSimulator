using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignarTag : MonoBehaviour
{
    public string targetTag = "Choque"; // Etiqueta que deseas asignar

    void Start()
    {
        // Recorre todos los hijos del objeto actual
        foreach (Transform child in transform)
        {
            // Asigna la etiqueta al hijo
            child.gameObject.tag = targetTag;
        }
    }
}
