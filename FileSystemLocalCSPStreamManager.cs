/********************************************************
*                                                        *
*   Copyright (C) Microsoft. All rights reserved.        *
*                                                        *
*********************************************************/

using CsvHelper;
using System;
using System.Globalization;
using System.IO;

namespace Microsoft.Partner.CSP.APISamples.SubscriptionStream
{
	class FileSystemLocalCSPStreamManager : ILocalCSPStreamManager
	{
		const string stateFile = "stream.state";
		const string logFile = "stream.log";

		private DateTime? lastCSPEventTime = null;

		public DateTime GetLastCSPEventTime()
		{
			if (!lastCSPEventTime.HasValue)
			{
				if (File.Exists(stateFile))
				{
					using (var sr = new StreamReader(stateFile))
					{
						string contents = sr.ReadToEnd();
						DateTime lastTime;
						if (DateTime.TryParse(contents, null, DateTimeStyles.RoundtripKind, out lastTime))
						{
							lastCSPEventTime = lastTime;
						}
					}
				}
				else
				{
					lastCSPEventTime = DateTime.Now;
				}
			}

			return lastCSPEventTime.Value;
		}

		public void SaveLastCSPEventTime(DateTime lastCSPEventTime)
		{
			using (var sw = new StreamWriter(stateFile, false))
			{
				DateTime dateToSave = DateTime.SpecifyKind(lastCSPEventTime, DateTimeKind.Local);
				sw.Write(dateToSave.ToString("o"));
			}
		}

		public void WriteCSPStreamEvent(CSPStreamEvent cspEvent)
		{
			using (var sw = new StreamWriter(logFile, true))
			using (var csv = new CsvWriter(sw))
			{
				csv.WriteRecord(cspEvent);
			}

		}
	}
}
