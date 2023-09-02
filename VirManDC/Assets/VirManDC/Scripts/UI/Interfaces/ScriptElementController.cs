using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScriptElementController : MonoBehaviour
{
    public TextMeshProUGUI scriptName;
    public int scriptid;
    public string hostid;
    private SlotComponentsReferences slotComponentsReferences;
    public GameObject loadingPanel;

    void Start(){
        loadingPanel.SetActive(false);
        slotComponentsReferences = this.GetComponentInParent<SlotComponentsReferences>();
    }

    public void SetValues(string scriptName, int scriptid, string hostid)
    {
        this.scriptName.text = scriptName;
        this.scriptid = scriptid;
        this.hostid = hostid;
    }
    /// <summary>
    /// Called from Click interaction in button
    /// </summary>
    public void ExecuteScript()
    {
        StartCoroutine(ExecuteScript_Co());
    }

    public IEnumerator ExecuteScript_Co(){
        loadingPanel.SetActive(true);
        yield return StartCoroutine(slotComponentsReferences.NewPetitionForScriptExecution(hostid, scriptid));
        loadingPanel.SetActive(false);
    }
}
