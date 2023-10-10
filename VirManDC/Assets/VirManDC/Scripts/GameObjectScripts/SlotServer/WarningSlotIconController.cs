// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningSlotIconController : MonoBehaviour
{
    public GameObject warningIcon;

    void Start()
    {
        warningIcon.SetActive(false);
    }

    public void NewStateIcon(bool newState){
        warningIcon.SetActive(newState);
    }

}
