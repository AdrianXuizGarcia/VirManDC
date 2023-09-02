using System;
using UnityEngine;

public class RackPositionManager : MonoBehaviour
{
    public static RackPositionManager Instance;
    public static event Action OnResetPositionsRack;

    void Awake(){
        Instance = this;
    }

    public static void ResetPositionsRack()
	{
        Debug.Log("Reseting position");
        OnResetPositionsRack?.Invoke();
    }
}
