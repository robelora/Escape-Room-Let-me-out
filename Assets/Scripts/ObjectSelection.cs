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
        if(highlight != null){
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit)){
            highlight = hit.transform;

            float dist2object = Vector3.Distance(highlight.position, transform.position);

            if(highlight.CompareTag("Interactable") && dist2object < maxDistance){
                if(highlight.gameObject.GetComponent<Outline>() != null){
                    highlight.gameObject.GetComponent<Outline>().enabled = true;
                }
                else{
                    Outline outline = highlight.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlight.gameObject.GetComponent<Outline>().OutlineColor = outlineMat.GetColor("_OutlineColor");
                    highlight.gameObject.GetComponent<Outline>().OutlineWidth = outlineMat.GetFloat("_OutlineWidth");
                }

                if(Input.GetMouseButtonDown(0)){
                    Debug.Log(highlight.gameObject.name);
                }
            }
            else{
                highlight = null;
            }
        }
    }
}
