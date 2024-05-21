using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.InputSystem.Controls;

public class Movimiento_torre : MonoBehaviour
{
    private Transform highlight;
    private int piso_seleccionado;
    private float rotationAngle = 90f;
    private float rotationSpeed = 100f;
    private bool isRotating = false;
    private bool isActive = false;
    private Vector2 puzleMovement;
    public Material connected;
    public Material outlineMatHighlight;
    public UnityEvent exitPuzzle;
    public UnityEvent completePuzzle;


    void Start(){
        //Primer piso
        piso_seleccionado = 0;
        highlight = transform.GetChild(piso_seleccionado).transform;
    }

    private IEnumerator RotateOverTime(Transform highlight ,Vector3 axis, float angle)
    {
        isRotating = true;
        
        Quaternion initialRotation = highlight.rotation;
        Quaternion targetRotation = Quaternion.Euler(highlight.eulerAngles + new Vector3(axis.x * angle, axis.y * angle, axis.z * angle));
        
        float elapsedTime = 0f;
        float duration = Mathf.Abs(angle) / rotationSpeed;

        while (elapsedTime < duration)
        {
            highlight.rotation = Quaternion.Slerp(initialRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ajustar correctamente el angulo de la pieza
        highlight.rotation = targetRotation;

        isRotating = false;
        completado();
    }

    private void completado(){

        GameObject piso_final = transform.GetChild(6).gameObject;
        // Conexion7.1 y Conexion7.2
        GameObject conexion1 = piso_final.transform.GetChild(0).gameObject;
        GameObject conexion2 = piso_final.transform.GetChild(1).gameObject;
        if(conexion1.GetComponent<EstadoCable>().conectado && conexion2.GetComponent<EstadoCable>().conectado){
            Debug.Log("Completado");
            // en GetChild(2) se encuenta el empty Fin del piso 7
            piso_final.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = connected;
            piso_final.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material = connected;
            SwitchActive();
            completePuzzle.Invoke();
        }
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

                // Hacia la izquierda
                if(!isRotating && puzleMovement.x < -0.4f){
                    StartCoroutine(RotateOverTime(highlight, Vector3.up, rotationAngle));
                }
                // Hacia la derecha
                else if(!isRotating && puzleMovement.x > 0.4f){
                    StartCoroutine(RotateOverTime(highlight, Vector3.up, -rotationAngle));
                }
                // Hacia arriba
                if(!isRotating && puzleMovement.y > 0.4f){
                    if (piso_seleccionado < transform.childCount - 1){
                        piso_seleccionado++;
                        highlight.gameObject.GetComponent<Outline>().enabled = false;
                        highlight = transform.GetChild(piso_seleccionado).transform;
                        setOutline(highlight);
                    }
                }
                // Hacia abajo
                else if(!isRotating && puzleMovement.y < -0.4f){
                    if (piso_seleccionado > 0){
                        piso_seleccionado--;
                        highlight.gameObject.GetComponent<Outline>().enabled = false;
                        highlight = transform.GetChild(piso_seleccionado).transform;
                        setOutline(highlight);
                    }
                }
            }
        }
    }

    public void Salir(){
        if(isActive){
            SwitchActive();
            exitPuzzle.Invoke();
        }
    }

}
