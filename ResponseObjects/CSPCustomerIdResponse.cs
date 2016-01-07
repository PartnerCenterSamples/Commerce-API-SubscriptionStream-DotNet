/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

namespace Microsoft.Partner.CSP.APISamples.SubscriptionStream
{
	class CSPCustomerIdResponse
	{
		public string id;
		public string customer_type;
		public CSPCustomerIdResponseIdentity identity;
		public bool is_test;
		public object links; // {"self": {"href": "customers/e9894580-3eea-4256-a1f6-bf943fe74a25","method": "GET"},
		public object profiles; // {"href": "e9894580-3eea-4256-a1f6-bf943fe74a25/profiles","method": "GET"},
		public object addresses; // {"href": "e9894580-3eea-4256-a1f6-bf943fe74a25/addresses","method": "GET"},
		public object delete; // {"href": "customers/e9894580-3eea-4256-a1f6-bf943fe74a25","method": "DELETE"}
		public string object_type;
		public string resource_status;
	}


	class CSPCustomerIdResponseIdentity
	{
		public string provider;
		public string type;
		public object data;   //": { "tid": "e9894580-3eea-4256-a1f6-bf943fe74a25" }
	}
}
