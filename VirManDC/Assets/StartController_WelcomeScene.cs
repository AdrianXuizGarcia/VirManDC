using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartController_WelcomeScene : MonoBehaviour
{
    public GameObject guiInfoPanel;
    public GameObject loadingBar;
    public float rotationGui = 24;

    void Start()
    {
        guiInfoPanel.SetActive(false);
        loadingBar.SetActive(false);
        guiInfoPanel.transform.Rotate(0.0f, rotationGui, 0.0f);
    }
}
