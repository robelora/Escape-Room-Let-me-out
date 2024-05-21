using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFin : MonoBehaviour
{
    public GameObject finMenuUI;
    public Button primaryButton;

    void Start(){
        primaryButton.Select();
    }

    public void QuitGame(){
        Debug.Log("Fin.Pa casa.");
        Application.Quit();
    }
}
