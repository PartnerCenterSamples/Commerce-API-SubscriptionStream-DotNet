/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;

namespace Microsoft.Partner.CSP.APISamples.SubscriptionStream
{
	interface ILocalCSPStreamManager
	{
		DateTime GetLastCSPEventTime();
		void SaveLastCSPEventTime(DateTime lastCSPEventTime);
		void WriteCSPStreamEvent(CSPStreamEvent cspEvent);

	}
}
