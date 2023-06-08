using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeArchitectureToLoad : MonoBehaviour
{
    public void ArchitectureFileName(string filename)
    {
        StaticDataHolder.architectureName = filename;
    }
}
