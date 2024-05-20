using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EstadoCable : MonoBehaviour
{
    public bool conectado;
    public int piso;

    void Start(){
        int.TryParse(transform.parent.gameObject.name, out piso);
    }
}

