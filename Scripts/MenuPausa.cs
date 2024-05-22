using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public Button primaryButton;
    public Image puntero;

    void Start(){
        primaryButton.Select();
    }

    public void Pausa(InputAction.CallbackContext callbackContext){
        if(callbackContext.performed){
            if(GameIsPaused){
                Resume();
            }else{
                Pause();
            }
        }
    }

    public void Resume(){
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        puntero.enabled = true;
    }

    public void Pause(){
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        puntero.enabled = false;
    }

    public void QuitGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
