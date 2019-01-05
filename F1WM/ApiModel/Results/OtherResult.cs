using System;
using System.Collections.Generic;

namespace F1WM.ApiModel
{
	public class OtherResult
	{
		public int EventId { get; set; }
		public string EventName { get; set; }
		public SeriesSummary Series { get; set; }
		public IEnumerable<OtherResultPosition> Results { get; set; }
		public OtherFastestLapResultSummary FastestLapResult { get; set; }
		public OtherLapResultSummary PolePositionLapResult { get; set; }
		public IEnumerable<OtherAdditionalPoints> AdditionalPoints { get; set; }
	}
}