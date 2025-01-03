// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotListManager : MonoBehaviour
{
    public List<GameObject> slotReferencesList = new List<GameObject>();

    public void AddNewSlotReference(GameObject slotReference){
        slotReferencesList.Add(slotReference);
    }
}
