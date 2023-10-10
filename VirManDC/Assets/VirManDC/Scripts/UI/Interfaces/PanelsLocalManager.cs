// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PanelsLocalManager : MonoBehaviour
{
    public GameObject warningPanel;
    public GameObject serverDataPanel;
    public GameObject zabbixDataPanel;
    public GameObject vmsListPanel;
    public GameObject scriptListPanel;

    void Start(){
        warningPanel.SetActive(false);
        serverDataPanel.SetActive(false);
        zabbixDataPanel.SetActive(false);
        vmsListPanel.SetActive(false);
        scriptListPanel.SetActive(false);
    }

    public void SwapStateWarningPanel()
    {
        warningPanel.SetActive(!warningPanel.activeSelf);
    }

    public void SwapStateServerDataPanel()
    {
        serverDataPanel.SetActive(!serverDataPanel.activeSelf);
    }

    public void SwapStateZabbixDataPanel()
    {
        zabbixDataPanel.SetActive(!zabbixDataPanel.activeSelf);
    }
    
    public void SwapStateVMsListPanel()
    {
        vmsListPanel.SetActive(!vmsListPanel.activeSelf);
    }

    public void SwapStateScriptListPanel()
    {
        scriptListPanel.SetActive(!scriptListPanel.activeSelf);
    }

}
