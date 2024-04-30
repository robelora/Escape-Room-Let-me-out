using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleTrigger : MonoBehaviour
{
    public InteractableObject item;

    public void activar(){
        if(item.type == "Item"){
            // Recoger item (Por si hacemos que sea necesario recoger)
        }
        else if(item.type == "Puzle"){
            switch(item.code){
                case 0:
                    break;
            }
        }
        else{
            Debug.Log("Tipo de objeto erróneo");
        }
    }

}
