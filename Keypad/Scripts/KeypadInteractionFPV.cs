using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
namespace NavKeypad { 

public class KeypadInteractionFPV : MonoBehaviour

{
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