using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Cubasdeagua : MonoBehaviour
{
    private Transform highlight;
    private Transform selected;

    private Valorescubas cuba1, cuba2;
    private Color colorBaseDigito;
    
    private bool isActive;
    private int cubaActual;
    public List<TextMeshPro> texto;
    public Material outlineMatHighlight;
    public Material outlineMatSelected;

    public UnityEvent exitPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        texto[0].text = "16";
        texto[1].text = "0";
        texto[2].text = "0";

        colorBaseDigito = texto[0].color;

        cubaActual = 0;
        highlight = transform.GetChild(cubaActual).transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            if(/*Gamepad.current.leftStick.left.wasPressedThisFrame || */Keyboard.current.aKey.wasPressedThisFrame){
                if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                if(cubaActual == 0){cubaActual = 2;}
                else{cubaActual--;}
                highlight = transform.GetChild(cubaActual).transform;
                setOutline(highlight);
            }

            if(/* Gamepad.current.leftStick.right.wasPressedThisFrame || */ Keyboard.current.dKey.wasPressedThisFrame){
                if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                cubaActual = (cubaActual+1) % 3;
                highlight = transform.GetChild(cubaActual).transform;
                setOutline(highlight);
            }
        }
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
                cuba1 = selected.gameObject.GetComponent<Valorescubas>();
                cuba2 = highlight.gameObject.GetComponent<Valorescubas>();

                if(cuba2.valorActual < cuba2.capacidad && cuba1.valorActual > 0){
                    cuba2.valorActual += cuba1.valorActual;
                    if(cuba2.valorActual > cuba2.capacidad){
                        cuba1.valorActual = cuba2.valorActual - cuba2.capacidad;
                        cuba2.valorActual = cuba2.capacidad;
                    }
                    else{cuba1.valorActual = 0;}

                    texto[cuba1.id].text = cuba1.valorActual.ToString();
                    texto[cuba2.id].text = cuba2.valorActual.ToString();
                }

                selected.gameObject.GetComponent<Outline>().OutlineColor = outlineMatHighlight.GetColor("_OutlineColor");
                selected.gameObject.GetComponent<Outline>().enabled = false;
                selected = null;
            }

            if(texto[0].text == "8"){texto[0].color = Color.green;}
            else{texto[0].color = colorBaseDigito;}

            if(texto[1].text == "8"){texto[1].color = Color.green;}
            else{texto[1].color = colorBaseDigito;}
        }
    }

    /* public void Activar(InputAction.CallbackContext callbackContext){
        if(callbackContext.performed && isActive){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit)){
                highlight = hit.transform;

                if(highlight.CompareTag("Interactable")){
                    if(selected == null){
                        selected = hit.transform;
                        selected.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else if(selected != null && selected == highlight){
                        selected.gameObject.GetComponent<Outline>().enabled = false;
                        selected = null;
                    }
                    else{
                        cuba1 = selected.gameObject.GetComponent<Valorescubas>();
                        cuba2 = highlight.gameObject.GetComponent<Valorescubas>();

                        if(cuba2.valorActual < cuba2.capacidad && cuba1.valorActual > 0){
                            cuba2.valorActual += cuba1.valorActual;
                            if(cuba2.valorActual > cuba2.capacidad){
                                cuba1.valorActual = cuba2.valorActual - cuba2.capacidad;
                                cuba2.valorActual = cuba2.capacidad;
                            }
                            else{cuba1.valorActual = 0;}

                            texto[cuba1.id].text = cuba1.valorActual.ToString();
                            texto[cuba2.id].text = cuba2.valorActual.ToString();
                        }

                        selected.gameObject.GetComponent<Outline>().enabled = false;
                        selected = null;
                    }
                }
            }
        }
    } */

    public void Salir(){
        if(isActive){
            for(int i = 0; i < 3; i++){
                    Valorescubas cuba = transform.GetChild(i).transform.gameObject.GetComponent<Valorescubas>();
                    cuba.valorActual = cuba.valorInicial;
                    texto[i].text = cuba.valorInicial.ToString();
                }
            cubaActual = 0;
            if(selected != null){
                selected.gameObject.GetComponent<Outline>().enabled = false;
                selected = null;
            }
            SwitchActive();
            exitPuzzle.Invoke();
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
        /* for(int i = 0; i < 3; i++){
            if(!isActive){
                transform.GetChild(i).transform.gameObject.tag = "Interactable";
            }
            else{
                transform.GetChild(i).transform.gameObject.tag = "Untagged";
            }
        } */
        isActive = !isActive;
        if(isActive){
            setOutline(highlight);
        }
        else{
            highlight.gameObject.GetComponent<Outline>().enabled = false;
        }
    }
}
