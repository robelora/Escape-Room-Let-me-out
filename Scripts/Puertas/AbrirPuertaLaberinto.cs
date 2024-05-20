using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertaLaberinto : MonoBehaviour, Interactable
{
    bool puerta = false;
    public GameObject p;
    static int activos = 0;
   
   public void Interact(){
        transform.gameObject.tag = "Untagged";
        activos++;
        if(puerta==false && activos==3){
            p.SetActive(false);
            puerta=true;        
        }  
    } 
}
