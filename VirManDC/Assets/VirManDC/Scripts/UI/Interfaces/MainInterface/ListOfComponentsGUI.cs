// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListOfComponentsGUI : MonoBehaviour
{
    public GameObject slotName;

    public void changeSlotName(string newName){
        slotName.GetComponent<TMP_Text>().text = newName;
    }
}
