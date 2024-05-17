using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Cubasdeagua : MonoBehaviour
{
    private RaycastHit hit;
    private Transform highlight;
    private Transform selected;

    private Valorescubas cuba1, cuba2;
    
    private bool isActive;
    public List<TextMeshPro> texto;

    public UnityEvent exitPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        texto[0].text = "16";
        texto[1].text = "0";
        texto[2].text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        if(isActive){
            if(highlight != null){highlight = null;}
            if(selected != null){selected.gameObject.GetComponent<Outline>().enabled = true;}
        }
    }

    public void Activar(InputAction.CallbackContext callbackContext){
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
    }

    public void Salir(){
        if(isActive){
            for(int i = 0; i < 3; i++){
                    Valorescubas cuba = transform.GetChild(i).transform.gameObject.GetComponent<Valorescubas>();
                    cuba.valorActual = cuba.valorInicial;
                    texto[i].text = cuba.valorInicial.ToString();
                }
            SwitchActive();
            exitPuzzle.Invoke();
        }
    }

    public void SwitchActive(){
        for(int i = 0; i < 3; i++){
            if(!isActive){
                transform.GetChild(i).transform.gameObject.tag = "Interactable";
            }
            else{
                transform.GetChild(i).transform.gameObject.tag = "Untagged";
            }
        }
        isActive = !isActive;
    }
}
