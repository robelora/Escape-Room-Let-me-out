using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class DiosesTrigger : MonoBehaviour, Interactable
{
    public UnityEvent interactAction;
    public CinemachineVirtualCamera puzzleCam;

    public void Interact(){
        transform.gameObject.tag = "Untagged";
        interactAction.Invoke();
        puzzleCam.enabled = true;
    }

    public void Salir(){
        transform.gameObject.tag = "Interactable";
        puzzleCam.enabled = false;
    }
}