// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using UnityEngine;
using VMDC.UI.Events;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SubMenuButton_ScriptableObject", order = 1)]
public class SubMenuButton : ScriptableObject
{
    public MenuEventType buttonType;
    public Color color;
}