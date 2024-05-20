using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Cubasdeagua : MonoBehaviour
{
    private Vector2 puzleMovement;
    private Transform highlight;
    private Transform selected;

    private Valorescubas cuba1, cuba2;
    private Color colorBaseDigito;
    
    private bool isActive;
    private int cubaActual;

    public List<Image> imgsBarrasLlenado;

    public List<TextMeshPro> texto;
    public Material outlineMatHighlight;
    public Material outlineMatSelected;

    public UnityEvent exitPuzzle;
    public UnityEvent completePuzzle;

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

                    imgsBarrasLlenado[cuba1.id].transform.localScale = new Vector3(1, (float)cuba1.valorActual/cuba1.capacidad, 1);
                    imgsBarrasLlenado[cuba2.id].transform.localScale = new Vector3(1, (float)cuba2.valorActual/cuba2.capacidad, 1);
                }

                selected.gameObject.GetComponent<Outline>().OutlineColor = outlineMatHighlight.GetColor("_OutlineColor");
                selected.gameObject.GetComponent<Outline>().enabled = false;
                selected = null;
            }

            if(texto[0].text == "8"){texto[0].color = Color.green;}
            else{texto[0].color = colorBaseDigito;}

            if(texto[1].text == "8"){texto[1].color = Color.green;}
            else{texto[1].color = colorBaseDigito;}

            if(texto[2].text == "0"){texto[2].color = Color.green;}
            else{texto[2].color = colorBaseDigito;}

            if(texto[0].text == "8" && texto[1].text == "8"){
                Completado();
            }
        }
    }

    public void Salir(){
        if(isActive){
            for(int i = 0; i < 3; i++){
                Valorescubas cuba = transform.GetChild(i).transform.gameObject.GetComponent<Valorescubas>();
                cuba.valorActual = cuba.valorInicial;
                texto[i].text = cuba.valorInicial.ToString();
                imgsBarrasLlenado[i].transform.localScale = new Vector3(1, (float)cuba.valorInicial/cuba.capacidad, 1);
            }
            texto[2].color = Color.green;
            cubaActual = 0;

            if(selected != null){
                selected.gameObject.GetComponent<Outline>().enabled = false;
                selected = null;
            }
            SwitchActive();
            exitPuzzle.Invoke();
        }
    }

    public void Completado(){
        SwitchActive();
        completePuzzle.Invoke();
    }

    public void setOutline(Transform highlight){
        if(highlight.gameObject.GetComponent<Outline>() != null){
            highlight.gameObject.GetComponent<Outline>().enabled = true;
        }
        else{
            Outline outline = highlight.gameObject.AddComponent<Outline>();
            outline.enabled = true;
            highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineMatHighlight.GetColor("_OutlineColor");
            highlight.gameObject.GetComponent<Outline>().OutlineWidth = outlineMatHighlight.GetFloat("_OutlineWidth");
        }
    }

    public void SwitchActive(){
        isActive = !isActive;
        if(isActive){
            setOutline(highlight);
        }
        else{
            highlight.gameObject.GetComponent<Outline>().enabled = false;
        }
    }

    public void PuzleMove(InputAction.CallbackContext callbackContext){
        if(callbackContext.performed){
            if(isActive){
                puzleMovement = callbackContext.ReadValue<Vector2>();

                if(puzleMovement.x < -0.4f){
                    if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                    if(cubaActual == 0){cubaActual = 2;}
                    else{cubaActual--;}
                    highlight = transform.GetChild(cubaActual).transform;
                    setOutline(highlight);
                }
                else if(puzleMovement.x > 0.4f){
                    if(selected != highlight){highlight.gameObject.GetComponent<Outline>().enabled = false;}
                    cubaActual = (cubaActual+1) % 3;
                    highlight = transform.GetChild(cubaActual).transform;
                    setOutline(highlight);
                }
            }
        }
    }
}
