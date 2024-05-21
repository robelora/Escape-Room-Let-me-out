using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbrirPuerta : MonoBehaviour, Interactable
{
    public GameObject p;
    public UnityEvent exitPuzzle;
 
    public void Interact(){
        transform.gameObject.tag = "Untagged";
        p.SetActive(false);
        exitPuzzle.Invoke();
    }  
}
