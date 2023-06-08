using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VMButtonController : MonoBehaviour
{
    public GameObject prefabToInstance;

    public void InstanceVMInterface(){
        Instantiate(prefabToInstance);
        //GameObject ob = Instantiate(prefabToInstance);
        //ob.transform.position += new Vector3(-1, 0, 0);
    }
}
