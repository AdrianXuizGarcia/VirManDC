using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VMDC.Constants;

public class VMButtonController : MonoBehaviour
{
    public GameObject prefabToInstance;
    private Transform pivotTransform;
    public TextMeshProUGUI serverName;
    public Image imageButton;
    public Color newButtonColorIfVMActive;
    private bool isVmActive;
    

    public void InstanceVMInterface(){
        pivotTransform = GameObject.FindGameObjectWithTag(VMDCTags.SPAWN_CAMERA_TAG).transform;
        Instantiate(prefabToInstance, pivotTransform);
        //GameObject ob = Instantiate(prefabToInstance);
        //ob.transform.position += new Vector3(-1, 0, 0);
    }

    public void SetValues (string serverName,bool isVmActive)
    {
        this.serverName.text = serverName;
        this.isVmActive = isVmActive;
        AsignColorToImageButton();
    }
	
	public void AsignColorToImageButton(){
		//Debug.Log("Is active:"+IsVMActive);
		if(isVmActive)
			imageButton.color = newButtonColorIfVMActive;
	}

    
}
