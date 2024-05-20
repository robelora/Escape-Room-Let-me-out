using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CubasTrigger : MonoBehaviour, Interactable
{
    public UnityEvent interactAction;
    public CinemachineVirtualCamera puzzleCam;
    public Image puntero;
    public GameObject puerta;

    public void Interact(){
        transform.gameObject.tag = "Untagged";
        interactAction.Invoke();
        puzzleCam.enabled = true;
        puntero.enabled = false;
    }

    public void Salir(){
        transform.gameObject.tag = "Interactable";
        puzzleCam.enabled = false;
        puntero.enabled = true;
    }

    public void Completado(){
        puzzleCam.enabled = false;
        puntero.enabled = true;
        puerta.SetActive(false);
    }

}
