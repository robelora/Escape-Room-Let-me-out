using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbrirPuerta : MonoBehaviour, Interactable
{
    public GameObject p;
 
    public void Interact(){
        transform.gameObject.tag = "Untagged";
        p.SetActive(false);
    }  
/*
GameObject p1,p2,b1,b2;

    void Start(){
        b1=GameObject.Find("Boton1");
        b2=GameObject.Find("KeypadStandard");
        p1=GameObject.Find("Puerta1");
        p2=GameObject.Find("Puerta2");
    }
    void Update(){
        Abrirpuerta();
    }
    
      
    public void Abrirpuerta(){
        if(Input.GetKeyDown(KeyCode.E)){
            if(puerta1==false){
                p1.SetActive(false);
                puerta1=true;
                b1.transform.tag="Untagged";
            }
            else{
                if(puerta2==false){
                p2.SetActive(false);
                b2.transform.tag="Untagged";
                puerta2=true;
                }
                else{}
            }
        }
    }*/

}
