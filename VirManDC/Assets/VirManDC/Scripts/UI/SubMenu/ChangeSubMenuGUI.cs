// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.UI.Events;

public class ChangeSubMenuGUI : MonoBehaviour
{

    public List<Button> buttonsList = new List<Button>();


    private List<ChangeSubmenuEvent> changeSubmenuEventList = new List<ChangeSubmenuEvent>();
    public List<ChangeSubmenuEvent> GetSubmenuEventsList() { return changeSubmenuEventList;}

    private void OnEnable() {
         foreach(Button button in buttonsList){
            // Get the submenu change event for the rest of components
            SubMenuButtonFunctionality buttonInvoke;
            buttonInvoke = button.gameObject.GetComponent<SubMenuButtonFunctionality>();
            //Debug.Log(buttonInvoke);
            changeSubmenuEventList.Add(buttonInvoke.onChangeSubmenuEvent);
            // Add listeners to the buttons
            button.gameObject.GetComponent<SubMenuButtonFunctionality>().onChangeSubmenuButtonEvent.AddListener(OnChangeSubmenuButtonEvent);
            //button.gameObject.GetComponent<SubMenuButtonFunctionality>().onChangeSubmenuEvent.AddListener(onChangeSubmenuButtonEvent);
        }
    }

    private void ResetButtons(){
        foreach(Button button in buttonsList){
            AssignNewButtonColor(button,Color.white);
        }
    }

    private void AssignNewButtonColor(Button button, Color color){
        //Debug.Log("COLOR : "+color);
        var colors = button.colors;
        colors.normalColor = color;
        button.colors = colors;
    }

    public void OnChangeSubmenuButtonEvent(Button button,MenuEventType buttonType, Color color){
        //Debug.Log("RECIBIDO : "+buttonType);
        ResetButtons();
        AssignNewButtonColor(button, color);
    }
}
