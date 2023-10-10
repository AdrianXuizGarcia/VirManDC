// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBasicRackController : MonoBehaviour
{
    //public static event Action<RackBasicData> OnRackFocus;
    private ArchitectureObjectsManager objectsManager;

    public int rackID;

    void Start(){
        objectsManager = GameObject.FindGameObjectWithTag("ArchitectureController").GetComponent<ArchitectureObjectsManager>();
    }
}
