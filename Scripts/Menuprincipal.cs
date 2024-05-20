using UnityEngine;
using UnityEngine.SceneManagement;

public class Menuprincipal : MonoBehaviour
{
    // Este método se llama cuando se hace clic en el botón de "Jugar"
    public void Jugar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Este método se llama cuando se hace clic en el botón de "Salir"
    public void Salir()
    {   
        Application.Quit();
    }
}
