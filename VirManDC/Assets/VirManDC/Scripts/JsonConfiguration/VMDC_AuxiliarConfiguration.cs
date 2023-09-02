using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using VMDC.Dtos;
using VMDC.Constants;
using ResponseAPIClasses;


namespace VMDC.AuxiliarConfiguration
{
	public class ZabbixConfigInfo
	{
		public ZabbixConfigInfo (string ip, string urlAPI, string urlFiles, string eUser, string ePass){
			ipServer = ip;
			urlZabbixAPI = urlAPI;
			urlConfigurationFiles = urlFiles;
			encryptedUser = eUser;
			encryptedPass = ePass;
		}

        public string ipServer;
        public string urlZabbixAPI;
        public string urlConfigurationFiles;
		public string encryptedUser;
		public string encryptedPass;
    }
	
    public static class ZabbixConfigFile
    {
		
        public static bool SetConfig()
		/*
			This function returns false if the file didnt existed (in which case its created)
			or if there is a problem with the reading of it (in which case its recreated).
			Otherwise (everything is ok) it returns true.
		*/
        {
			/*if (!CheckIfFileExistsAndCreateIfNot())
				return false;*/
			try
            {
				string jsonConfig = ReadFile(VMDCPaths.configFilePath);
				ZabbixConfigInfo aux = JsonUtility.FromJson<ZabbixConfigInfo>(jsonConfig);
				SetValues(aux.ipServer,aux.urlZabbixAPI,aux.urlConfigurationFiles,aux.encryptedUser,aux.encryptedPass);
				return true;
            }
            catch (Exception e)
            {
				Debug.Log(e.Message);
				// Reset the default config
				SetConfigFileToDefault();
                return false;
            }
        }
		
		private static void SetValues(string ipServer, string urlZabbixAPI, string urlConfigurationFiles, string eUser, string ePass)
		{
			ZabbixConfig.ipServer = ipServer;
			ZabbixConfig.urlZabbixAPI = urlZabbixAPI;
			ZabbixConfig.urlConfigurationFiles = urlConfigurationFiles;
			ZabbixConfig.encryptedUser = eUser;
			ZabbixConfig.encryptedPass = ePass;
		}
		
		private static bool CheckIfFileExistsAndCreateIfNot()
		{
			if (!File.Exists(VMDCPaths.configFilePath)){
				//FileStream fs = File.Create(file);
				//fs.Close();
				SetConfigFileToDefault();
				return false;
			}
			return true;
		}
		
		public static bool SaveAndWriteNewConfiguration(string ipServer, string urlZabbixAPI, string urlConfigurationFiles,string eUser, string ePass)
		{
			SetValues(ipServer,urlZabbixAPI,urlConfigurationFiles, eUser, ePass);
			return WriteVMDCConfigToFile(VMDCPaths.configFilePath);
		}
		
		private static string ReadFile(string file)
        {	
            FileStream F = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);
            string line;
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(F))
                {
                    // Read the stream to a string, and write the string to the console.
                    line = sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Debug.Log("The file could not be read:");
                Debug.Log(e.Message);
                throw new ExitGUIException();
            }
            finally
            {
                F.Close();
            }
            return line;
        }
		
		public static void SetConfigFileToDefault(){
			File.Copy(VMDCPaths.defaultConfigFilePath,VMDCPaths.configFilePath,true);
		}
		
		private static bool WriteVMDCConfigToFile(string path)
        {	
			ZabbixConfigInfo aux = new ZabbixConfigInfo(ZabbixConfig.ipServer,
			ZabbixConfig.urlZabbixAPI,ZabbixConfig.urlConfigurationFiles,ZabbixConfig.encryptedUser,ZabbixConfig.encryptedPass);
			try {
				using (StreamWriter file = File.CreateText(path))
				{
                    //JsonSerializer serializer = new JsonSerializer();
                    //serialize object directly into file stream
                    //serializer.Serialize(file, aux);
                    file.Write(JsonUtility.ToJson(aux,true));
                }
				Debug.Log("Done writing file!");
				return true;
			} catch (Exception e){
				Debug.Log("The file could not be writed: "+e);
				return false;
			}
        }
		
		public static void SetDefaultConfigFromDefaultFile(){
			try
            {
				string jsonConfig = ReadFile(VMDCPaths.defaultConfigFilePath);
                //Debug.Log("config:"+jsonConfig);
                ZabbixConfigInfo aux = JsonUtility.FromJson<ZabbixConfigInfo>(jsonConfig);
				//Debug.Log(aux.urlZabbixAPI);
				SetDefaultValues(aux.ipServer,aux.urlZabbixAPI,aux.urlConfigurationFiles);
            }
            catch (Exception e)
            {
				Debug.Log(e.Message);
            }
		}
		
		private static void SetDefaultValues(string ipServer, string urlZabbixAPI, string urlConfigurationFiles)
		{
			ZabbixConfigDefault.ipServer = ipServer;
			ZabbixConfigDefault.urlZabbixAPI = urlZabbixAPI;
			ZabbixConfigDefault.urlConfigurationFiles = urlConfigurationFiles;
		}
    }
	
	
	
	public static class ZabbixConfig
	/*
		This class store the configuration data:
		- authKey comes from the API Zabbix petition
		- ipServer from ZabbixConfigFile
		- urlZabbixAPI from ZabbixConfigFile
		- urlConfigurationFiles from ZabbixConfigFile
	*/
	{
		public static string authKey { get; set; }
        public static string ipServer { get; set; }
		public static string urlZabbixAPI { get; set;}
		public static string urlConfigurationFiles { get; set;}	
		public static string encryptedUser { get; set;}	
		public static string encryptedPass { get; set;}	
	}
	
	public static class ZabbixConfigDefault
	{
        public static string ipServer { get; set; }
		public static string urlZabbixAPI { get; set;}
		public static string urlConfigurationFiles { get; set;}	
	}

}