using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

public class RackPositionController : MonoBehaviour
{
    private Transform originalTransform;


    void Awake()
	{
        RackPositionManager.OnResetPositionsRack += OnResetPositionsRack;
        //ArchitectureObjectsManager.OnRackPicked += OnRackPicked;
    }

	void OnDestroy() {
		RackPositionManager.OnResetPositionsRack -= OnResetPositionsRack;
        //ArchitectureObjectsManager.OnRackPicked -= OnRackPicked;
	}

    private void OnResetPositionsRack(){
        // TODO
        gameObject.transform.parent.transform.position = originalTransform.position;
        //gameObject.transform.parent.transform.position = new Vector3(1, 1, 1);
    }

    private void OnRackPicked(Transform originalTransform){
        this.originalTransform = originalTransform;
    }
}
