using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class MonedaCambiar : MonoBehaviour
{
    private Vector2 puzleMovement;
    private GameObject highlight;
    private GameObject selected;
    private GameObject m1,m2,m3,m4,m5;
    private int monedaActual;
    private bool isActive;
    public Material outlineMatHighlight;
    public Material outlineMatSelected;
    public UnityEvent exitPuzzle;
    public UnityEvent completedPuzzle;
    private valoresMoneda moneda1,moneda2;
    void Start()
    {
        monedaActual = 0;
        highlight = transform.GetChild(monedaActual).gameObject;
        m1=transform.GetChild(0).gameObject; //serpiente
        m2=transform.GetChild(1).gameObject;
        m3=transform.GetChild(2).gameObject;
        m4=transform.GetChild(3).gameObject;
        m5=transform.GetChild(4).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        /*  if(isActive){
            if(Gamepad.current.leftStick.left.wasPressedThisFrame ||Keyboard.current.aKey.wasPressedThisFrame){
                if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                if(monedaActual == 0){monedaActual = 4;}
                else{monedaActual--;}
                highlight = transform.GetChild(monedaActual).gameObject;
                setOutline(highlight);
            }

            if(Gamepad.current.leftStick.right.wasPressedThisFrame || Keyboard.current.dKey.wasPressedThisFrame){
                if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                monedaActual = (monedaActual+1) % 5;
                highlight = transform.GetChild(monedaActual).gameObject;
                setOutline(highlight);
            }
        } */
    }

     public void setOutline(GameObject highlight){
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
                moneda1=selected.GetComponent<valoresMoneda>();
                moneda2=highlight.GetComponent<valoresMoneda>();
                int aux=moneda1.posicionActual;
                moneda1.posicionActual=moneda2.posicionActual;
                moneda2.posicionActual=aux;
                int index1 = highlight.transform.GetSiblingIndex();
                int index2 = selected.transform.GetSiblingIndex(); 
                Vector3 posicion = highlight.transform.position;
                highlight.transform.position=selected.transform.position;
                selected.transform.position=posicion;
                highlight.transform.SetSiblingIndex(index2);
                selected.transform.SetSiblingIndex(index1);
                

                selected.gameObject.GetComponent<Outline>().OutlineColor = outlineMatHighlight.GetColor("_OutlineColor");
                selected.gameObject.GetComponent<Outline>().enabled = false;
                highlight.gameObject.GetComponent<Outline>().enabled = false;
                highlight = selected;
                setOutline(highlight);
                selected = null;

                //Debug.Log(m1==transform.GetChild(2).gameObject || m1==transform.GetChild(4).gameObject);
                // Debug.Log(m2==transform.GetChild(3).gameObject);
                //Debug.Log(m3==transform.GetChild(1).gameObject);
                //Debug.Log(m4==transform.GetChild(2).gameObject || m4==transform.GetChild(4).gameObject);

            }
            

            
            /* if((m1==transform.GetChild(2).gameObject || m1==transform.GetChild(4).gameObject)&&
            (m2==transform.GetChild(3).gameObject)&&
            (m3==transform.GetChild(1).gameObject)&&
            (m4==transform.GetChild(2).gameObject || m4==transform.GetChild(4).gameObject)&&
            (m5==transform.GetChild(0).gameObject)) */
            if(m2.GetComponent<valoresMoneda>().posicionActual==m2.GetComponent<valoresMoneda>().posicionCorrecta &&
            m3.GetComponent<valoresMoneda>().posicionActual==m3.GetComponent<valoresMoneda>().posicionCorrecta &&
            m5.GetComponent<valoresMoneda>().posicionActual==m5.GetComponent<valoresMoneda>().posicionCorrecta){
                Completado();
            }
        }
    }

    public void Completado(){
        SwitchActive(); 
        completedPuzzle.Invoke();
    }
    public void Salir(){
            if(isActive){
            if(selected != null){
                selected.gameObject.GetComponent<Outline>().enabled = false;
                selected = null;
            }
            SwitchActive();   
            exitPuzzle.Invoke();
            }
    }

    public void PuzleMove(InputAction.CallbackContext callbackContext){
        if(callbackContext.performed){
            if(isActive){
                puzleMovement = callbackContext.ReadValue<Vector2>();

                if(puzleMovement.x < -0.4f){
                    if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                    if(monedaActual == 0){monedaActual = 4;}
                    else{monedaActual--;}
                    highlight = transform.GetChild(monedaActual).gameObject;
                    setOutline(highlight);
                }
                else if(puzleMovement.x > 0.4f){
                    if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                    monedaActual = (monedaActual+1) % 5;
                    highlight = transform.GetChild(monedaActual).gameObject;
                    setOutline(highlight);
                }
            }
        }
    }
}


