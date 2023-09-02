using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertsPanelController : MonoBehaviour
{
    public GameObject noWarningButton;
    public GameObject warningButton;
    public GameObject noWarningText;
    public GameObject warningText;

    public void Start(){
        noWarningButton.SetActive(true);
        warningButton.SetActive(false);
        noWarningText.SetActive(true);
        warningText.SetActive(false);
    }
    public void ChangeState(){
        noWarningButton.SetActive(!noWarningButton.activeSelf);
        warningButton.SetActive(!warningButton.activeSelf);
        noWarningText.SetActive(!noWarningText.activeSelf);
        warningText.SetActive(!warningText.activeSelf);
    }

}
