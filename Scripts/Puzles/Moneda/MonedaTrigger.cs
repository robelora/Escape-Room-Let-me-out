using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using UnityEngine.UI;


public class MonedaTrigger : MonoBehaviour, Interactable
{
    public UnityEvent interactAction;
    public CinemachineVirtualCamera puzzleCam;
    //private AudioSource sonido;
    public GameObject puerta;
    public Image puntero;
    public Renderer l1;
    public Material lighton;
    private void Start(){
       // sonido=GetComponent<AudioSource>();
    }
    public void Interact(){
        transform.gameObject.tag = "Untagged";
        interactAction.Invoke();  
        puzzleCam.enabled = true;
        puntero.enabled = false;
    }  

    public void Salir(){
        transform.gameObject.tag = "Interactable";
        puzzleCam.enabled = false;
        puntero.enabled = true;
    }

    public void Completado(){
        puerta.SetActive(false);
        l1.material = lighton;
        //sonido.Play();
        puzzleCam.enabled = false;
        puntero.enabled = true;
    }
}
