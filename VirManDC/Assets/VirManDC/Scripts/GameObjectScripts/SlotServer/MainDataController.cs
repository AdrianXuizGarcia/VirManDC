// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDataController : MonoBehaviour
{
    /// <summary>
    /// Since this controller is in the panel, this petition avoids
    /// a dependance with the slotController. Should be one reference on
    /// main parent, so this call returns something
    /// </summary>
    public void NewPetitionForMainData()
    {
        GetComponentInParent<SlotComponentsReferences>().NewPetitionForMainData();
    }
}