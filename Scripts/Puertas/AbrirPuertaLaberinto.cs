using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbrirPuertaLaberinto : MonoBehaviour
{
    GameObject p1,b1,b2,b3;
    int puerta1 = 0;
    void Start()
    {
        b1=GameObject.Find("BotonV1");
        b2=GameObject.Find("BotonV2");
        b3=GameObject.Find("BotonV3");
        p1=GameObject.Find("Puerta4");
    }

    // Update is called once per frame
    void Update()
    {
        Abrirpuerta();
    }

    void Abrirpuerta(){
        if(Input.GetKeyDown(KeyCode.E)){
            if(puerta1==0){
                b1.transform.tag="Untagged";
                puerta1++;
            }else if(puerta1==1){
                b2.transform.tag="Untagged";
                puerta1++;
            }
            else if(puerta1==2){
                b3.transform.tag="Untagged";
                p1.SetActive(false);
            }
        }
    }
}
