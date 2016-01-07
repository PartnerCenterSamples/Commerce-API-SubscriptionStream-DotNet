/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;

namespace Microsoft.Partner.CSP.APISamples.SubscriptionStream
{
	class CSPStreamEvent
	{
		public string SubscriptionUri { get; set; }
		public string SubscriptionEventType { get; set; }
		public string RecipientCustomerUri { get; set; }
		public DateTime EventDate { get; set; }
		public string Data { get; set; }

		public CSPStreamEvent() { }

		public CSPStreamEvent(StreamPageResponse.StreamEntryResponse response)
		{
			this.SubscriptionUri = response.subscription_uri;
			this.SubscriptionEventType = response.type;
			this.RecipientCustomerUri = response.recipient_customer_uri;
			this.Data = response.data.ToString().Replace(Environment.NewLine, "");

			DateTime temp;
			if (DateTime.TryParse(response.event_date, out temp))
			{
				this.EventDate = temp;
			}
		}

	}
}

