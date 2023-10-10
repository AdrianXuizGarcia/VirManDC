// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System;
using UnityEngine;

public class RackNameManager : MonoBehaviour
{
    public static RackNameManager Instance;
    public static event Action<bool> OnChangeNameRacks;

    private static bool namesAreShown = true;

    void Awake(){
        Instance = this;
    }

    public static void ChangeStateLabelsRacks()
	{
        namesAreShown = !namesAreShown;
        //Debug.Log("Change state of labels to: "+namesAreShown);
        OnChangeNameRacks?.Invoke(namesAreShown);
    }
}
