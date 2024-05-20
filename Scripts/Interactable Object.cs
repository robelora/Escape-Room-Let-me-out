using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle", menuName = "Puzzle")]
public class InteractableObject : ScriptableObject
{
    public new string name;
    public string type;

    public int code;
}
