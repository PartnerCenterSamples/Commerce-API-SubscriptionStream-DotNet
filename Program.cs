/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System.Configuration;

namespace Microsoft.Partner.CSP.APISamples.SubscriptionStream
{
	class Program
	{
		static void Main(string[] args)
		{
			// This is the Microsoft ID of the reseller
			// Please work with your Admin Agent to get it from https://partnercenter.microsoft.com/en-us/pc/AccountSettings/TenantProfile
			string microsoftId = ConfigurationManager.AppSettings["MicrosoftId"];

			// This is the default domain of the reseller
			// Please work with your Admin Agent to get it from https://partnercenter.microsoft.com/en-us/pc/AccountSettings/TenantProfile
			string defaultDomain = ConfigurationManager.AppSettings["DefaultDomain"];

			// This is the appid that is registered for this application in Azure Active Directory (AAD)
			// Please work with your Admin Agent to get it from  https://partnercenter.microsoft.com/en-us/pc/ApiIntegration/Overview 
			string appId = ConfigurationManager.AppSettings["AppId"];

			// This is the key for this application in Azure Active Directory
			// This is only available at the time your admin agent has created a new app at https://partnercenter.microsoft.com/en-us/pc/ApiIntegration/Overview
			// You could alternatively goto Azure Active Directory and generate a new key, and use that.
			string key = ConfigurationManager.AppSettings["Key"];


			// Prompts the user to edit the config parametres if its not already done.
			Utilities.ValidateConfiguration(microsoftId, defaultDomain, appId, key);

			FileSystemLocalCSPStreamManager mgr = new FileSystemLocalCSPStreamManager();
			Orchestrator orch = new Orchestrator(mgr, defaultDomain, appId, key, microsoftId);

			orch.StartProcessing();

			orch.GetAndProcessPage();

			orch.EndProcessing();
		}
	}
}
