using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Final : MonoBehaviour
{
    private string targetTag = "Player";
    public GameObject finMenuUI;
    public Image puntero;

    void OnTriggerEnter(Collider other){
        Debug.Log("Entra");
        if (other.gameObject.tag == targetTag){
            Cursor.lockState = CursorLockMode.Confined;
            puntero.enabled = false;
            finMenuUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}