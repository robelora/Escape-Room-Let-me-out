using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

interface Interactable{
    public void Interact();
}

public class ObjectSelection : MonoBehaviour
{
    private RaycastHit hit;
    private Transform highlight;

    public Material outlineMat;
    public float maxDistance = 2f;

    private PlayerInput playerInput;
    private bool interaction = false;

    public UnityEvent exitPuzzle;

    void Start(){
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update(){

        // Se elimina el resaltado actual si existe
        if(highlight != null){
            highlight.gameObject.GetComponent<Outline>().enabled = false;
            highlight = null;
        }

        // Se lanza un rayo desde la posición del ratón (el centro de la pantalla)
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        // Si golpea con algo
        if(Physics.Raycast(ray, out hit, maxDistance)){
            highlight = hit.transform;

            float dist2object = Vector3.Distance(highlight.position, transform.position);

            // Si está marcado como "Interactable" y estamos suficientemente cerca
            if(highlight.CompareTag("Interactable") && dist2object <= maxDistance){
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

                // Al pulsar el botón del ratón se obtiene su componente de interacción y se llama a su función Interact
                if(interaction && highlight.gameObject.TryGetComponent(out Interactable interactObj)){
                    playerInput.SwitchCurrentActionMap("Puzzle");
                    interactObj.Interact();
                }
            }
            // Si no se elimina la referencia al objeto golpeado
            else{
                highlight = null;
            }
        }

        interaction = false;
    }

    public void Activar(InputAction.CallbackContext callbackContext){
        if(callbackContext.performed){
            interaction = true;
        }
    }

    public void Salir(InputAction.CallbackContext callbackContext){
        if(callbackContext.performed && playerInput.currentActionMap == playerInput.actions.FindActionMap("Puzzle")){
            playerInput.SwitchCurrentActionMap("Explore");
            Debug.Log("Salir");
            exitPuzzle.Invoke();
        }
    }

    public void PuzleCompletado(){
        playerInput.SwitchCurrentActionMap("Explore");
    }
}
