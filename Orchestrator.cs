/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using System;
using System.Threading;

namespace Microsoft.Partner.CSP.APISamples.SubscriptionStream
{
	class Orchestrator
	{
		ILocalCSPStreamManager localManager;
		string resellerCid;
		AuthorizationToken adAuthorizationToken = null;
		AuthorizationToken saAuthorizationToken = null;

		private DateTime lastCSPEventTime;

		public Orchestrator() { }

		public Orchestrator(ILocalCSPStreamManager mgr)
		{
			localManager = mgr;
		}

		public Orchestrator(ILocalCSPStreamManager mgr, string defaultDomain, string ptrCtrAppId, string ptrCtrAppSecret, string resellerMicrosoftId)
		{
			localManager = mgr;
			Initialize(defaultDomain, ptrCtrAppId, ptrCtrAppSecret, resellerMicrosoftId);
		}

		void Initialize(string defaultDomain, string ptrCtrAppId, string ptrCtrAppSecret, string resellerMicrosoftId)
		{
			// Get Active Directory token first
			adAuthorizationToken = Reseller.GetAD_Token(defaultDomain, ptrCtrAppId, ptrCtrAppSecret);

			// Using the ADToken get the sales agent token
			saAuthorizationToken = Reseller.GetSA_Token(adAuthorizationToken);

			// Get the Reseller Cid, you can cache this value
			resellerCid = Reseller.GetCid(resellerMicrosoftId, saAuthorizationToken.AccessToken);
		}

		public void StartProcessing()
		{
			if (String.IsNullOrEmpty(resellerCid) ||
					adAuthorizationToken == null ||
					saAuthorizationToken == null)
			{
				throw new ArgumentException("Call Initialize() before processing");
			}

			lastCSPEventTime = localManager.GetLastCSPEventTime();
			Stream.CreateStream(lastCSPEventTime, resellerCid, saAuthorizationToken.AccessToken);

		}

		public void GetAndProcessPage()
		{
			if (String.IsNullOrEmpty(resellerCid) ||
					adAuthorizationToken == null ||
					saAuthorizationToken == null)
			{
				throw new ArgumentException("Call Initialize() before processing");
			}

			int retryAfter = 60;   // seconds  - Make a new call after this interval
			TimeSpan abortAfter = new TimeSpan(0, 10, 0);  // stop waiting after this interval

			DateTime start = DateTime.Now;

			do
			{
				StreamPageResponse subscriptionPage = Stream.GetStreamPage(resellerCid, saAuthorizationToken.AccessToken);
				foreach (var subscriptionEvent in subscriptionPage.items)
				{
					CSPStreamEvent streamEvent = new CSPStreamEvent(subscriptionEvent);
					lastCSPEventTime = streamEvent.EventDate;

					Console.WriteLine("Entry: " + streamEvent.SubscriptionEventType);
					localManager.WriteCSPStreamEvent(streamEvent);
				}

				// event not found - mark page as read
				Stream.MarkStreamPageComplete(subscriptionPage.links.completion.href, subscriptionPage.links.completion.method, saAuthorizationToken.AccessToken);

				Console.WriteLine("sleeping");
				Thread.Sleep(retryAfter * 1000);
			} while (DateTime.Now < start + abortAfter);

		}

		public void EndProcessing()
		{
			if (String.IsNullOrEmpty(resellerCid) ||
					adAuthorizationToken == null ||
					saAuthorizationToken == null)
			{
				throw new ArgumentException("Call Initialize() before processing");
			}

			Stream.DeleteStream(resellerCid, saAuthorizationToken.AccessToken);

			localManager.SaveLastCSPEventTime(lastCSPEventTime.AddMinutes(1));
		}
	}
}
