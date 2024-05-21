using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TorreTrigger : MonoBehaviour, Interactable
{
    public GameObject Papel;
    public UnityEvent interactAction;
    public CinemachineVirtualCamera puzzleCam;
    public Image puntero;

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
        Papel.SetActive(true);
        puzzleCam.enabled = false;
        puntero.enabled = true;
    }

}

