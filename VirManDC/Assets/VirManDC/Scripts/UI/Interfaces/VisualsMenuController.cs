using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualsMenuController : MonoBehaviour
{
    public GameObject visualsMenuPanel;

    void Start(){
        visualsMenuPanel.SetActive(false);
    }

    public void SwapVisualsMenuState(){
        visualsMenuPanel.SetActive(!visualsMenuPanel.activeSelf);
    }
}
