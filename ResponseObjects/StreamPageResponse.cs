/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

namespace Microsoft.Partner.CSP.APISamples.SubscriptionStream
{
	class StreamPageResponse
	{
		public StreamEntryResponse[] items;
		public string object_type;
		public string contract_version;
		public StreamPageLinksResponse links;


		public class StreamEntryResponse
		{
			public string type;
			public string subscription_uri;
			public string recipient_customer_uri;
			public string event_date;
			public object data;

		}

		public class StreamPageLinksResponse
		{
			public StreamPageLink completion;
		}

		public class StreamPageLink
		{
			public string href;
			public string method;
		}
	}
}
