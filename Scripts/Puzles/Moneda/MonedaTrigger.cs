using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;


public class MonedaTrigger : MonoBehaviour, Interactable
{
    public UnityEvent interactAction;
    //public CinemachineVirtualCamera puzzleCam;
    public GameObject puerta;
    public void Interact(){
        transform.gameObject.tag = "Untagged";
        interactAction.Invoke();  
        //puzzleCam.enabled = true;
    }  

    public void Salir(){
        transform.gameObject.tag = "Interactable";
        //puzzleCam.enabled = false;
    }

    public void Completado(){
        puerta.SetActive(false);
        //puzzleCam.enabled = false;
    }
}
