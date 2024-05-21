using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeypadTrigger : MonoBehaviour, Interactable
{
    public CinemachineVirtualCamera puzzleCam;
    public Image puntero;
    public GameObject puerta;

    public void Interact(){
        transform.gameObject.tag = "Untagged";
        transform.GetComponent<BoxCollider>().enabled = false;
        puzzleCam.enabled = true;
        puntero.enabled = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Salir(){
        transform.gameObject.tag = "Interactable";
        transform.GetComponent<BoxCollider>().enabled = true;
        puzzleCam.enabled = false;
        puntero.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Completado(){
        Cursor.lockState = CursorLockMode.Locked;
        puzzleCam.enabled = false;
        puntero.enabled = true;
        puerta.SetActive(false);
    }

}
