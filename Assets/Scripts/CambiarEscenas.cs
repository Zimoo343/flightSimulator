using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Este script maneja el cambio entre diferentes escenas del juego.
public class CambiarEscenas : MonoBehaviour
{
    public string mainMenu; // Nombre de la escena del men� principal.
    public string juego; // Nombre de la escena del juego.

    // M�todo para cambiar a la escena del men� principal.
    public void CambiarAEscenaMainMenu()
    {
        SceneManager.LoadScene(mainMenu); // Cargar la escena del men� principal.
    }

    // M�todo para cambiar a la escena del juego.
    public void CambiarAEscenaJuego()
    {
        SceneManager.LoadScene(juego); // Cargar la escena del juego.
    }
}
