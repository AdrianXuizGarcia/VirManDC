using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualButtonBehaviour : MonoBehaviour
{
	public Color color;
	
	public void CheckVMActive(bool IsVMActive){
		//Debug.Log("Is active:"+IsVMActive);
		if(IsVMActive)
			GetComponent<Image>().color = color;
	}
}
