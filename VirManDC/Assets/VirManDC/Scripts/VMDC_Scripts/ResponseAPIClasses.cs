using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ResponseAPIClasses
{
	// ***********
	// JSON CLASSES FOR DESERIALIZE
	// ***********
	public class WarningData
	{
		public int priority { get; set; }
		public WarningEvent lastEvent { get; set;}
	}

	public class WarningEvent
	{
		public string name { get; set; }
		public string severity { get; set; }
	}
	
	public class ResponseIdHosts
	{
		public string jsonrpc = "2.0";
		public List<IdHostsData> result { get; set; }
		public int id { get; set; }
	}
	
	public class IdHostsData
	// Note: the names must be the same as the API returns
	{
		public string hostid { get; set; }
		public string name { get; set; }
		public string host { get; set; }
		public string description { get; set; }
		public List<IdHostInterface> interfaces { get; set; }
	}
	
	public class IdHostInterface
	{
		public string interfaceid { get; set; }
		public string ip { get; set; }
	}

	public class ResponseHostGroup
	{
		public string jsonrpc = "2.0";
		public List<HostGroupsData> result { get; set; }
		public int id { get; set; }
	}
	
	public class HostGroupsData
	{
		public string groupid { get; set; }
		public string name { get; set; }
		//public string internal { get; set; }
	}
	
	public class Request
	{
		public string jsonrpc = "2.0";
		public string method { get; set; }
		public Dictionary<string, System.Object> @params { get; set; }
		public int id { get; set; }
		public string auth { get; set; }

		public Request(string method, Dictionary<string, System.Object> @params, int id, string auth)
		{
			this.method = method;
			if (@params != null)
			{
				this.@params = @params;
			}
			else
			{
				this.@params = new Dictionary<string, System.Object>();
			}
			this.id = id;
			this.auth = auth;
		}
		public void addParam(string key, string value)
		{
			@params.Add("key", "value");
		}
	}

	public class Response
	{
		public string jsonrpc = "2.0";
		public List<System.Object> result { get; set; }
		public int id { get; set; }

		public Response(List<System.Object> result, int id)
		{
			if (result != null)
			{
				this.result = result;
			}
			else
			{
				this.result = new List<System.Object>();
			}
			this.id = id;
		}
		public void addResult(List<System.Object> result)
		{
			result.Add(result);
		}
	}

	public class ResponseLoggin
	{
		public string jsonrpc = "2.0";
		public string result { get; set; }
		public int id { get; set; }
	}

	public class ResponseItems
	{
		public string jsonrpc = "2.0";
		public List<ResponseItem> result { get; set; }
		public int id { get; set; }
	}

	public class ResponseItem
	{
		public string description { get; set; }
		public string key_ { get; set; }
		public string name { get; set; }
		public string lastvalue { get; set; }
	}
	
	
}
