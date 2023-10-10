// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAppMode : MonoBehaviour
{
    public void SignInIsOk()
    {
        StaticDataHolder.architectureMode = false;
    }
}
