using UnityEngine;
using System.IO;

namespace VMDC.Constants
{
	public static class VMDCPaths {
		//public static string extraFilesDirPath = "ConfigurationFiles";
		public static string extraFilesDirName = "ConfigurationFiles";
		//public static string extraFilesDirPath = Application.persistentDataPath+extraFilesDirName;
		public static string extraFilesDirPath = Path.Combine(Application.persistentDataPath,extraFilesDirName);
		// Path for the logFile output
		public static string logFilePath = Path.Combine(extraFilesDirPath,"logData.log");
		
		public static string architectureName = "Architecture_SalaCPD.xml";
		public static string architecturePath = Path.Combine(extraFilesDirPath,architectureName);
		//public static string architecturePath = Path.Combine(extraFilesDirPath,StaticDataHolder.architectureFileName);
		public static string modelsRackName = "RackDataModels.xml";
		public static string modelsRackPath = Path.Combine(extraFilesDirPath,modelsRackName);
		
		//public static string configFilePath = extraFilesDirPath+"/VMDCConfiguration.config";
		public static string configFilePath = Path.Combine(Application.persistentDataPath,"VMDCConfiguration.config");
		public static string defaultConfigFilePath = Path.Combine(Application.streamingAssetsPath,"VMDCConfigurationDefault.config");
		
		
		public static string racksModelsPath = "RacksModels/";
		public static string slotsModelsPath = "SlotModels/";
		public static string othersModelsPath = "OthersModels/";
		public static string slotsSpritesPath = "SlotSprites/";
		
		public static string KeyValueModelsName = "KeyValueModels.xml";
		public static string KeyValueModelsPath = Path.Combine(extraFilesDirPath,KeyValueModelsName);
		
		public static string panelSemaforoModelsName = "PanelSemaforoModels.xml";
		public static string panelSemaforoModelsPath = Path.Combine(extraFilesDirPath,panelSemaforoModelsName);
		public static string interfacesSemaforoModelsName = "InterfacesSemaforoModels.xml";
		public static string interfacesSemaforoModelsPath = Path.Combine(extraFilesDirPath,interfacesSemaforoModelsName);
		public static string semaforoPrefabPath = "OthersModels/semaforoModel"; //deprecated
		public static string indicatorPrefabPath = "UI/Indicator_space_v2";

	}
	public static class VMDCErrorCodes {
		public const string NON_RESPONSE_API_ERROR = "<color=red>ERROR</color>: No response from API Zabbix";
	}
	
	public static class VMDCTags {
		public const string VIRTUAL_MACHINE_TAG = "virtualMachine";
	}
	
}
