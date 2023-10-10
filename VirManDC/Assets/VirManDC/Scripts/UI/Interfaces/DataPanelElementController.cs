// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using TMPro;
using UnityEngine;

public class DataPanelElementController : MonoBehaviour {

    public TextMeshProUGUI keyName;
    public TextMeshProUGUI description;
    public TextMeshProUGUI lastValue;

    /* Use this for initialization
    void OnEnable () {
        key_ = transform.GetChild(0).GetComponent<Text>();
        description = transform.GetChild(1).GetComponent<Text>();
        lastValue = transform.GetChild(2).GetComponent<Text>();
		
	}*/

    public void SetValues (string keyName, string description, string lastValue)
    {
        this.keyName.text = keyName;
        this.lastValue.text = lastValue;
        this.description.text = description;
    }
}
