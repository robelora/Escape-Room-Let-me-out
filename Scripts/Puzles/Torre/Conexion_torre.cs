using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Conexion_torre : MonoBehaviour
{
    private string targetTag = "ConexionTorre";
    public Material notConnected;
    public Material connected;

    void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == targetTag){
            int num_piso_other = other.gameObject.GetComponent<EstadoCable>().piso;
            bool conectado_other = other.gameObject.GetComponent<EstadoCable>().conectado;
            if(num_piso_other < GetComponent<EstadoCable>().piso && conectado_other){
                GetComponent<Renderer>().material = connected;
                GetComponent<EstadoCable>().conectado = true;
            }
        }
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject.tag == targetTag){
            int num_piso_other = other.gameObject.GetComponent<EstadoCable>().piso;
            bool conectado_other = other.gameObject.GetComponent<EstadoCable>().conectado;
            if (num_piso_other < GetComponent<EstadoCable>().piso && GetComponent<EstadoCable>().conectado != conectado_other){
                GetComponent<Renderer>().material = other.gameObject.GetComponent<Renderer>().material;
                GetComponent<EstadoCable>().conectado = conectado_other;
            }
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.tag == targetTag){
            int num_piso_other = other.gameObject.GetComponent<EstadoCable>().piso;
            if(num_piso_other < GetComponent<EstadoCable>().piso){
                GetComponent<Renderer>().material = notConnected;
                GetComponent<EstadoCable>().conectado = false;
            }
        }
    }
}
