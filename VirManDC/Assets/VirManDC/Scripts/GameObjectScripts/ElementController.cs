using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementController : MonoBehaviour {

    private Text key_;
    private Text description;
    private Text lastValue;
	
	private ScriptPanelInfo scriptPanelInfo;
	private string vmHostname;
	private string vmHostID;
	
	public VirtualButtonBehaviour vmButton;

    // Use this for initialization
    void OnEnable () {
        key_ = transform.GetChild(0).GetComponent<Text>();
        description = transform.GetChild(1).GetComponent<Text>();
        lastValue = transform.GetChild(2).GetComponent<Text>();
		
	}

    public void SetValues (string key_, string description, string lastValue)
    {
        this.key_.text = key_;
        this.lastValue.text = lastValue;
        this.description.text = description;
    }
	
	public void SetVMValues (string name, string hostid, bool isVmActive)
	{
		this.key_.text = name;
		this.vmHostID = hostid;
		this.vmHostname = name;
		vmButton.CheckVMActive(isVmActive);
	}
	
	void OnMouseEnter()
    {
		Entering();
    }
	
	public void Entering(){
		// WIP
		if (scriptPanelInfo==null)
			scriptPanelInfo = gameObject.GetComponentInParent<ScriptPanelInfo>();
		scriptPanelInfo.EnterVMFromButton(vmHostID,vmHostname);
		//Debug.Log("Entering");
	}
}
