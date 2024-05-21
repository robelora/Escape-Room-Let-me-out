using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class NotaTrigger : MonoBehaviour, Interactable
{
    public GameObject notaVisual;
    
    public void Interact(){
        transform.gameObject.tag = "Untagged";
        notaVisual.SetActive(true);
    }  

    public void Salir(){
        transform.gameObject.tag = "Interactable";
        notaVisual.SetActive(false);
    }
}
