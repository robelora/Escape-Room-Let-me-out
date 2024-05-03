using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Cubasdeagua : MonoBehaviour
{
    private RaycastHit hit;
    private Transform highlight;
    private Transform selected;

    private Valorescubas cuba1, cuba2;
    
    public List<TextMeshPro> texto;

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
        if(highlight != null){highlight = null;}
        if(selected != null){selected.gameObject.GetComponent<Outline>().enabled = true;}

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit)){
            highlight = hit.transform;

            if(highlight.CompareTag("Interactable") && Input.GetMouseButtonDown(0)){
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

        if(Input.GetMouseButtonDown(1)){
  
            for(int i = 0; i < 3; i++){
                Valorescubas cuba = transform.GetChild(i).transform.gameObject.GetComponent<Valorescubas>();
                cuba.valorActual = cuba.valorInicial;
                texto[i].text = cuba.valorInicial.ToString();
            }
        }
    }
}
