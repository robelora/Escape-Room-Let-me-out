using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuprincipal : MonoBehaviour
{
    //[SerializeField] private GameObject menuPausa;
    // Este método se llama cuando se hace clic en el botón de "Jugar"
    public void Jugar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Este método se llama cuando se hace clic en el botón de "Salir"
    public void Salir()
    {   
        Application.Quit();
    }
}
