using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace NavKeypad { 

public class KeypadInteractionFPV : MonoBehaviour

{

    /* private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {

            if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                {
                    keypadButton.PressButton();
                }
            }
        }
    } */

    public void Activar(InputAction.CallbackContext callbackContext){

        
        if(callbackContext.performed){
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
             if (Physics.Raycast(ray, out var hit))
            {
                if (hit.collider.TryGetComponent(out KeypadButton keypadButton))
                {
                    keypadButton.PressButton();
                }
            }
        }}
}
}