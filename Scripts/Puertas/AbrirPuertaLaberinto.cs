using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbrirPuertaLaberinto : MonoBehaviour, Interactable
{
    bool puerta = false;
    public GameObject p;
    public Renderer l1,l2,l3;
    static int activos = 0;
    public Material lighton;
    public UnityEvent exitPuzzle;
   
   public void Interact(){
        transform.gameObject.tag = "Untagged";
        exitPuzzle.Invoke();
        activos++;
        if(activos==1)
            l1.material = lighton;
        if(activos==2)
            l2.material = lighton;
        if(puerta==false && activos==3){
            l3.material = lighton;
            p.SetActive(false);
            puerta=true;        
        }  
    } 
}
