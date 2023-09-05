using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Este script maneja el cambio entre diferentes escenas del juego.
public class CambiarEscenas : MonoBehaviour
{
    public string mainMenu; // Nombre de la escena del menú principal.
    public string juego; // Nombre de la escena del juego.

    // Método para cambiar a la escena del menú principal.
    public void CambiarAEscenaMainMenu()
    {
        SceneManager.LoadScene(mainMenu); // Cargar la escena del menú principal.
    }

    // Método para cambiar a la escena del juego.
    public void CambiarAEscenaJuego()
    {
        SceneManager.LoadScene(juego); // Cargar la escena del juego.
    }
}
