using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uLipSync;

public class BakedVoice : MonoBehaviour
{
    public BakedData[] bakedData;
    private uLipSyncBakedDataPlayer bakedPlayer;
    // Start is called before the first frame update
    void Start()
    {
        bakedPlayer = gameObject.GetComponent<uLipSyncBakedDataPlayer>();
    }

    public void Saludo(){
        bakedPlayer.bakedData = bakedData[0];
        bakedPlayer.Play();
    }

    public void PuzleResuelto(){
        bakedPlayer.bakedData = bakedData[1];
        bakedPlayer.Play();
    }

    public void JuegoResuelto(){
        bakedPlayer.bakedData = bakedData[2];
        bakedPlayer.Play();
    }
}
