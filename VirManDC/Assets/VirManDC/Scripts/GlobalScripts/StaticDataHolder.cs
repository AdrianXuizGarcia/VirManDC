// Copyright (c) 2023 Adrián Xuíz García.
// Licensed under the MIT License.

using VMDC.Constants;

public static class StaticDataHolder
{
	public static int DebugMode = -1;
    public static bool architectureMode = true;

    public static bool IndicatorPanelManagerIsInitialized = false;

    public static string architectureName = VMDCPaths.architectureName;
    public static string ZabbixServerAPIversion;

}
