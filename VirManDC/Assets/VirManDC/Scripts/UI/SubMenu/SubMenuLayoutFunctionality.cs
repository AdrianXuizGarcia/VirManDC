using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VMDC.UI.Events;

public class SubMenuLayoutFunctionality : MonoBehaviour  
{

    public MenuEventType panelType;
    private ChangeSubMenuGUI _controller;

    void Start(){
        _controller = GameObject.FindGameObjectWithTag("ControllerSubmenusGUI").GetComponent<ChangeSubMenuGUI>();
        //Debug.Log(_controller.GetSubmenuEventsList());
        foreach(var menuEvent in _controller.GetSubmenuEventsList()){
            menuEvent.AddListener(OnChangeSubmenu);
        }
        
    }
    public void OnChangeSubmenu(MenuEventType buttonType){
        //Debug.Log("Actualizando : "+buttonType);
        gameObject.SetActive(buttonType == panelType);

    }
}
