// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using TMPro;
using UnityEngine;

public class LoadingSceneController : MonoBehaviour
    {
    public string sceneToLoad;
    private AsyncOperation loadingOperation;
    public TextMeshPro percentLoaded;
    public GameObject loadingBar;
    private bool isLoading = false;

    public void LoadNewScene(string newScene){
        loadingOperation = VMDC_SceneManager.LoadSceneWithCallBack(newScene);
        isLoading = true;
    }
    
    void Update()
    {
        if (isLoading){
            float progressValue = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            percentLoaded.text = Mathf.Round(progressValue * 100) + "%";
            //Debug.Log("progress: " + progressValue);
            loadingBar.transform.localScale += new Vector3(progressValue,0,0);
            if(loadingBar.transform.localScale.x>=1){
                loadingBar.transform.localScale = new Vector3(1,1,1);
                isLoading = false;
            }
                
        }
    }
}
