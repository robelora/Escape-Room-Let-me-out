using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleTrigger : MonoBehaviour, Interactable
{
    public UnityEvent interactAction;

    public void Interact(){
        interactAction.Invoke();
    }

}
