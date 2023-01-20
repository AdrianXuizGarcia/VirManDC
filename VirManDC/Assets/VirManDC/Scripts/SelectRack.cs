using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectRack : MonoBehaviour
{

    private Material originalMat;
    private bool original = true;

    void Start(){
        originalMat = gameObject.GetComponent<Renderer>().material;
    }
    public void ChangeColor(Material newMat){
        if (original){
            //Material newMat = Resources.Load("Assets/VirManDC/Art/Materials/"+newColor, typeof(Material)) as Material;
            gameObject.GetComponent<Renderer>().material = newMat;
        } else 
         gameObject.GetComponent<Renderer>().material = originalMat;
        
        original = !original;
    }
}
