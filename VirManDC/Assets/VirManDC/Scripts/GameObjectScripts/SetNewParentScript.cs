using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNewParentScript : MonoBehaviour
{
    public Transform newParent;
    public GameObject childGameObject;
    public bool setAtStart = false;

    void Start()
    {
        if (setAtStart)
            SetNewParent();
    }
    
    public void SetNewParent(){
        childGameObject.transform.SetParent(newParent,false);
    }

}