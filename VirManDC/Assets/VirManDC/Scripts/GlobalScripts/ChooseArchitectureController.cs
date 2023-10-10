// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChooseArchitectureController : MonoBehaviour
{
    public TextMeshPro actualArchitectureText;
    private string originalText;

    void Start(){
        originalText = actualArchitectureText.text;
        ChangeActualArchitectureText();
    }
    public void ChangeArchitectureFileName(string filename)
    {
        StaticDataHolder.architectureName = filename;
        ChangeActualArchitectureText();
    }

    private void ChangeActualArchitectureText(){
        actualArchitectureText.text = originalText + StaticDataHolder.architectureName;
    }
}
