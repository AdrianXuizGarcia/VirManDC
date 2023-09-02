using System;
using UnityEngine;

public class NotificationManager
//TODO
{
    public static NotificationManager Instance;
    public static event Action<bool> HostsDataRetrieved;

    void Awake(){
        Instance = this;
    }

    public static void HostsDataHasBeenRetrieved(bool state)
	{
        Debug.Log("New search with state "+state);
        HostsDataRetrieved?.Invoke(state);
    }
}
