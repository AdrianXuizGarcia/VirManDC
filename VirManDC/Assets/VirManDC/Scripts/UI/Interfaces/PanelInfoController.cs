// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfoController : MonoBehaviour {

	// Only one instance per slot. Clear and adds elements to the panel
	// of data, and also inform of change of VM to SlotRackInteraction
    public int buttonActive = -1;
    public GameObject listaElementos;
    public GameObject elementoLista;
	public GameObject listElementButton;
    private GameObject elementoAñadido;
    private DataPanelElementController elementController;
	
    public void ClearList()
    {
        foreach (Transform child in listaElementos.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddElement(string key_, string description, string lastValue)
    {
        elementoAñadido = Instantiate(elementoLista) as GameObject;
        elementController = elementoAñadido.GetComponent<DataPanelElementController>();
        elementController.SetValues(key_, description, lastValue);
        elementoAñadido.transform.SetParent(listaElementos.transform,false);
    }
}
