using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MonedaCambiar : MonoBehaviour
{
    private Transform highlight;
    private Transform selected;
    public Transform m1,m2,m3,m4,m5;
    private int monedaActual;
    private bool isActive;
    private bool completado=false;
    public Material outlineMatHighlight;
    public Material outlineMatSelected;
    public UnityEvent exitPuzzle;
    public UnityEvent completedPuzzle;
    void Start()
    {
        monedaActual = 0;
        highlight = transform.GetChild(monedaActual).transform;
        m1=transform.GetChild(0).transform; //posicion=1
        m2=transform.GetChild(1).transform;
        m3=transform.GetChild(2).transform;
        m4=transform.GetChild(3).transform;
        m5=transform.GetChild(4).transform;
    }

    // Update is called once per frame
    void Update()
    {
         if(isActive){
            if(/*Gamepad.current.leftStick.left.wasPressedThisFrame || */Keyboard.current.aKey.wasPressedThisFrame){
                if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                if(monedaActual == 0){monedaActual = 4;}
                else{monedaActual--;}
                highlight = transform.GetChild(monedaActual).transform;
                setOutline(highlight);
            }

            if(/* Gamepad.current.leftStick.right.wasPressedThisFrame || */ Keyboard.current.dKey.wasPressedThisFrame){
                if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                monedaActual = (monedaActual+1) % 5;
                highlight = transform.GetChild(monedaActual).transform;
                setOutline(highlight);
            }
        }
    }

     public void setOutline(Transform highlight){
        if(highlight.gameObject.GetComponent<Outline>() != null){
            highlight.gameObject.GetComponent<Outline>().enabled = true;
        }
        // Si no se le añade el componente 
        else{
            Outline outline = highlight.gameObject.AddComponent<Outline>();
            outline.enabled = true;
            highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineMatHighlight.GetColor("_OutlineColor");
            highlight.gameObject.GetComponent<Outline>().OutlineWidth = outlineMatHighlight.GetFloat("_OutlineWidth");
        }
    }

     public void SwitchActive(){
        isActive = !isActive;
        if(isActive)
            setOutline(highlight);
        else
            highlight.gameObject.GetComponent<Outline>().enabled = false;
    }

     public void Activar(InputAction.CallbackContext callbackContext){
        if(callbackContext.performed && isActive){
        
            if(selected == null){
                selected = highlight;
                selected.gameObject.GetComponent<Outline>().enabled = true;
                selected.gameObject.GetComponent<Outline>().OutlineColor = outlineMatSelected.GetColor("_OutlineColor");
            }
            else if(selected != null && selected == highlight){
                selected.gameObject.GetComponent<Outline>().OutlineColor = outlineMatHighlight.GetColor("_OutlineColor");
                selected = null;
            }
            else{
                int index1 = highlight.GetSiblingIndex();
                int index2 = selected.GetSiblingIndex();
                Vector3 posicion = highlight.position;
                highlight.position=selected.position;
                selected.position=posicion;
                highlight.SetSiblingIndex(index2);
                selected.SetSiblingIndex(index1);
                

                selected.gameObject.GetComponent<Outline>().OutlineColor = outlineMatHighlight.GetColor("_OutlineColor");
                selected.gameObject.GetComponent<Outline>().enabled = false;
                selected = null;
            
            }
            if((m1.position==transform.GetChild(2).transform.position || m1.position==transform.GetChild(4).transform.position)&&
            (m2.position==transform.GetChild(3).transform.position)&&
            (m3.position==transform.GetChild(1).transform.position)&&
            (m4.position==transform.GetChild(2).transform.position || m4.position==transform.GetChild(4).transform.position)&&
            (m5.position==transform.GetChild(0).transform.position)){
                Debug.Log("bienpicha");
                completado=true;
                Salir();
            }
        }
    }

    public void Salir(){
            if(isActive){
            if(selected != null){
                selected.gameObject.GetComponent<Outline>().enabled = false;
                selected = null;
            }
            SwitchActive();
            if(completado)
                completedPuzzle.Invoke();
            exitPuzzle.Invoke();
            }
    }
}


