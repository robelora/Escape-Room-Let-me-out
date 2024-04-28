using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSelection : MonoBehaviour
{
    private RaycastHit hit;
    private Transform highlight;

    public Material outlineMat;
    public float maxDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update(){

        // Se elimina el resaltado actual si existe
        if(highlight != null){
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        // Se lanza un rayo desde la posición del ratón (el centro de la pantalla)
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Si golpea con algo
        if(Physics.Raycast(ray, out hit)){
            highlight = hit.transform;

            float dist2object = Vector3.Distance(highlight.position, transform.position);

            // Si está marcado como "Interactable" y estamos suficientemente cerca
            if(highlight.CompareTag("Interactable") && dist2object < maxDistance){
                // Si tiene outline se activa
                if(highlight.gameObject.GetComponent<Outline>() != null){
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                // Si no se le añade el componente 
                else{
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineMat.GetColor("_OutlineColor");
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = outlineMat.GetFloat("_OutlineWidth");
                }

                // ACCIÓN A REALIZAR AL PULSAR EL BOTÓN
                if(Input.GetMouseButtonDown(0)){
                    Debug.Log(highlight.gameObject.name);
                }
            }
            // Si no se elimina la referencia al objeto golpeado
            else{
                highlight = null;
            }
        }
    }
}
