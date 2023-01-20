using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inicio : MonoBehaviour
{
    public GameObject selectRack;
    public GameObject guiSlot;

    public GameObject guiSlotServerTest;
    public float rotationGui = 290;

    void Start()
    {
        selectRack.SetActive(false);
        guiSlot.SetActive(false);

        //guiSlotServerTest.SetActive(false);
        guiSlotServerTest.transform.Rotate(0.0f, rotationGui, 0.0f);



    }

}
