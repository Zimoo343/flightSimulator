using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string SampleScene; // Nombre de la escena que deseas reiniciar

    // Funci�n p�blica para reiniciar la escena
    public void RestartScene()
    {
        // Carga nuevamente la misma escena por su nombre
        SceneManager.LoadScene(SampleScene);
    }
}
