using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VMDC_SceneManager : MonoBehaviour
    {

    public static VMDC_SceneManager Instance;

    void Awake(){
        Instance = this;
    }

    public static AsyncOperation LoadSceneWithCallBack(string scene){
        return SceneManager.LoadSceneAsync(scene);
    }

    public static void LoadScene(string scene){
        SceneManager.LoadSceneAsync(scene);
    }
}
