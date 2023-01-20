using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerAnim : MonoBehaviour
{

    public Animator animator;
    private bool serverIsOpen = false;
    public string slotName;

    public GameObject gui;
    private BoxCollider boxCollider;

    void OnEnable() {
        //gui = GameObject.FindWithTag("SlateUGUI");
    }
    void Start() {
        //animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void OpenServerAnim(){
        if (serverIsOpen)
            Debug.Log("Cerrando server");
        else
            Debug.Log("Abriendo server");
        animator.SetBool("ServerIsOpen", !serverIsOpen);
        serverIsOpen = !serverIsOpen;
        boxCollider.enabled = !boxCollider.enabled;
        //AsignSlotNameToGUI();
    }

    private void AsignSlotNameToGUI(){
        gui = GameObject.FindWithTag("SlateUGUI");
        gui.GetComponent<ListOfComponentsGUI>().changeSlotName(slotName);

    }
    
}
