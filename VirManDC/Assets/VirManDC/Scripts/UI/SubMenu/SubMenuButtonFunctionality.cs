// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.UI.Events;
using UnityEngine.UI;

public class SubMenuButtonFunctionality : MonoBehaviour
{
    [SerializeField]
    private MenuEventType buttonType;
    [SerializeField]
    private Color newButtonColor;

    public bool isDefaultMode;

    void Start() {
        if (isDefaultMode){
            onChangeSubmenuButtonEvent.Invoke(this.gameObject.GetComponent<Button>(),buttonType,newButtonColor);
            onChangeSubmenuEvent.Invoke(buttonType);
            //Debug.Log("Enviado start");
        }
    }

    void FixedUpdate() {
        
    }
    
    public void SendOnClickEvent(){
        onChangeSubmenuButtonEvent.Invoke(this.gameObject.GetComponent<Button>(),buttonType,newButtonColor);
        onChangeSubmenuEvent.Invoke(buttonType);
        //Debug.Log("Enviado");
    }

    #region Events
    public ChangeSubmenuButtonEvent onChangeSubmenuButtonEvent;
    public ChangeSubmenuEvent onChangeSubmenuEvent;

    /*#region Events
    private readonly ChangeSubmenuEvent onChangeSubmenuEvent = new ChangeSubmenuEvent();

    public UnityEvent<ButtonType> OnChangeSubmenuEvent => onChangeSubmenuEvent;
    */
    #endregion
}
