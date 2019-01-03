using System;
using F1WM.ApiModel;
using F1WM.DatabaseModel;

public static class OtherEntryExtensions
{
	public static CarSummary GetCarInfo(this OtherEntry entry)
	{
		return new CarSummary() { Name = entry.CarName };
	}
}