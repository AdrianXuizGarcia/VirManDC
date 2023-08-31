using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.AuxiliarConfiguration;

public class ZabbixServerStatusIconController : MonoBehaviour
{
    public GameObject loadingIcon;
    public GameObject okIcon;
    public GameObject errorIcon;

    public void SetStatus(bool statusOk){
        loadingIcon.SetActive(false);
        okIcon.SetActive(statusOk);
        errorIcon.SetActive(!statusOk);
    }

    public void ResetStatus(){
        loadingIcon.SetActive(true);
        okIcon.SetActive(false);
        errorIcon.SetActive(false);
    }
}
