using UnityEngine;
using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace VMDC.JsonDtos
/*
	Here are the DTO's classes used by the jsons to send petitions. These are used
	to avoid the direct knowlegde of the API response system, therefore giving a 
    layer of abstraction.
*/
{
    [Serializable]
    public class BasePetition
    {
        public string jsonrpc;
        public string method;
        public BasePetitionParams @params;
        public string id;
        //public string auth;
    }
    [Serializable]
    public abstract class BasePetitionParams {};
    [Serializable]
    public class LogInPetitionParams : BasePetitionParams { 
        public LogInPetitionParams(string u, string p){
            this.user = u;
            this.password = p;
        }
        public string user;
        public string password;
    };


    [Serializable]
    public class WarningPetition
    {
        public string jsonrpc;
        public string method;
        public BasePetitionParams @params;
        public string id;
        //public string auth;
    }
    [Serializable]
    public class WarningPetitionParams { 
        public WarningPetitionParams(string u, string p){
            this.user = u;
            this.password = p;
        }
        public string user;
        public string password;
    };
}
