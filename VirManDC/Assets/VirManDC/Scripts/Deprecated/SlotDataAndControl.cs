// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VMDC.Dtos;

/// <summary>
/// This class is used by the components to access the slot data that
/// comes from SlotRackDto, and control the behaviour of the slot
/// It is part of the 'raw data' from RackSlotDto,
/// and part of the HostsData.
/// The SlotDataAndControl takes only the necessary data for the display of the info,
/// nothing of the physical part of the slot should be here.
/// </summary>
public class SlotDataAndControl : MonoBehaviour
{
	[Header("DATA")]
	public string hostID;
	public string ip;
	public int slotNum;
	public string slotName;
	public string type;
    public string hostname;
	public string host;
	public string descriptionHost;

	public DataApiSchema dataApiSchema;
	public DataApiContainer dataApiContainer;
	public string hostGroupID;
	public List<VMData> virtualMachinesList;
	
	[Header("CONTROL")]
	public bool checkSlotDemoMode = false;
	public bool slotIsDeactivated = false;
	public bool isRefreshing = true;
	public float refreshTime; //WIP
}
