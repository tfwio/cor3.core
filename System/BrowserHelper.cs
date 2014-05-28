/* User: oIo * Date: 8/18/2010 * Time: 4:27 AM */
using System;
using System.Collections.Generic;
namespace System
{

	public class BrowserHelper
	{
		public static Dictionary<string,string> Alias
		{
			get
			{
				Dictionary<string, string> dic = new Dictionary<string, string>();
				dic.Add("$(Month)",MySqlDateTime(ThirtyDaysBack));
				dic.Add("$(DateTimeMonth)",MySqlDateTime(ThirtyDaysBack));
				dic.Add("$(30Days)",MySqlDateTime(ThirtyDaysBack));
				dic.Add("$(Week)",MySqlDateTime(WeekBack));
				dic.Add("$(DateTimeWeek)",MySqlDateTime(WeekBack));
				dic.Add("$(Now)",MySqlDateTime(DateTime.Now));
				dic.Add("$(DateTimeNow)",MySqlDateTime(DateTime.Now));
				return dic;
			}
		}
		
		static public string MySqlDate(DateTime value)
		{
			return string.Format("{0}-{1}-{2}",value.Year,value.Month,value.Day);
		}
		static public string MySqlDateTime(DateTime value)
		{
			return string.Format("{0}-{1}-{2} {3}:{4}:{5:00#}",value.Year,value.Month,value.Day,value.Hour,value.Minute,value.Second);
		}

		/// <summary>
		/// Filters as follows to default format for DateTime.ToString()
		/// <para>• $(DateTimeNow)</para>
		/// <para>• $(LastWeek), $(-7Days), $(PastWeek)</para>
		/// <para>• $(NextWeek), $(+7Days)</para>
		/// <para>• $(BusinessWeek), $(+5Days)</para>
		/// <para>• $(-BusinessWeek), $(-5Days)</para>
		/// <para>• $(ThirtyDaysBack), $(DateTimeMinus30Days), $(-30Days)</para>
		/// <para>• $(ThirtyDaysAhead), $(DateTimePlus30Days), $(+30Days)</para>
		/// </summary>
		static public object DateTimeFilter(string inputstring)
		{
			return inputstring
				.Replace("$(DateTimeNow)",string.Format("{0}",Now))
				
				.Replace("$(PastWeek)",string.Format("{0}",WeekBack))
				.Replace("$(LastWeek)",string.Format("{0}",WeekBack))
				.Replace("$(-7Days)",string.Format("{0}",WeekBack))
				
				.Replace("$(NextWeek)",string.Format("{0}",NextWeek))
				.Replace("$(+7Days)",string.Format("{0}",NextWeek))

				.Replace("$(FiveDaysAhead)",string.Format("{0}",FiveDaysAhead))
				.Replace("$(+5Days)",string.Format("{0}",FiveDaysAhead))

				.Replace("$(-5Days)",string.Format("{0}",FiveDaysBack))
				.Replace("$(FiveDaysBack)",string.Format("{0}",FiveDaysBack))
				
				.Replace("$(ThirtyDaysBack)",string.Format("{0}",ThirtyDaysBack))
				.Replace("$(DateTimeMinus30Days)",string.Format("{0}",ThirtyDaysBack))
				.Replace("$(-30Days)",string.Format("{0}",ThirtyDaysBack))
				
				.Replace("$(ThirtyDaysAhead)",string.Format("{0}",ThirtyDaysAhead))
				.Replace("$(DateTimePlus30Days)",string.Format("{0}",ThirtyDaysAhead))
				.Replace("$(+30Days)",string.Format("{0}",ThirtyDaysAhead))
				;
		}
		static public DateTime GetDate(object invalue)
		{
			string newValue = invalue.ToString().ToLower()
				.Replace("$(datetimenow)",string.Format("{0}",Now))
				.Replace("$(now)",string.Format("{0}",Now))
				.Replace("now",string.Format("{0}",Now))
				
				.Replace("$(pastweek)",string.Format("{0}",WeekBack))
				.Replace("$(lastweek)",string.Format("{0}",WeekBack))
				.Replace("lastweek",string.Format("{0}",WeekBack))
				.Replace("pastweek",string.Format("{0}",WeekBack))
				.Replace("$(-7days)",string.Format("{0}",WeekBack))
				
				.Replace("nextweek",string.Format("{0}",NextWeek))
				.Replace("$(nextweek)",string.Format("{0}",NextWeek))
				.Replace("$(+7Days)".ToLower(),string.Format("{0}",NextWeek))
				.Replace("+7Days".ToLower(),string.Format("{0}",NextWeek))

				.Replace("$(FiveDaysAhead).ToLower()",string.Format("{0}",FiveDaysAhead))
				.Replace("$(+5Days)".ToLower(),string.Format("{0}",FiveDaysAhead))
				.Replace("+5Days".ToLower(),string.Format("{0}",FiveDaysAhead))

				.Replace("$(-5Days)".ToLower(),string.Format("{0}",FiveDaysBack))
				.Replace("$(FiveDaysBack)".ToLower(),string.Format("{0}",FiveDaysBack))
				.Replace("FiveDaysBack".ToLower(),string.Format("{0}",FiveDaysBack))
				.Replace("-5Days".ToLower(),string.Format("{0}",FiveDaysBack))
				
				.Replace("$(ThirtyDaysBack)".ToLower(),string.Format("{0}",ThirtyDaysBack))
				.Replace("$(DateTimeMinus30Days)".ToLower(),string.Format("{0}",ThirtyDaysBack))
				.Replace("$(-30Days)".ToLower(),string.Format("{0}",ThirtyDaysBack))
				.Replace("-30Days".ToLower(),string.Format("{0}",ThirtyDaysBack))
				
				.Replace("$(ThirtyDaysAhead)".ToLower(),string.Format("{0}",ThirtyDaysAhead))
				.Replace("$(DateTimePlus30Days)".ToLower(),string.Format("{0}",ThirtyDaysAhead))
				.Replace("$(+30Days)".ToLower(),string.Format("{0}",ThirtyDaysAhead))
				.Replace("+30Days".ToLower(),string.Format("{0}",ThirtyDaysAhead))
				;
			DateTime newDt = DateTime.MinValue;
			try {
				newDt=DateTime.Parse(newValue.Trim());
			} catch (Exception) {
				
			}
			return newDt;
		}
		
		static public  DateTime Now { get { return DateTime.Now; } }
		static public  DateTime WeekBack { get { return DateTime.Now.Subtract(TimeSpan.FromDays(7)); } }
		static public  DateTime NextWeek { get { return DateTime.Now.AddDays(7); } }
		static public  DateTime FiveDaysBack { get { return DateTime.Now.Subtract(TimeSpan.FromDays(5)); } }
		static public  DateTime FiveDaysAhead { get { return DateTime.Now.AddDays(5); } }
		static public  DateTime ThirtyDaysBack { get { return DateTime.Now.Subtract(TimeSpan.FromDays(30)); } }
		static public  DateTime ThirtyDaysAhead { get { return DateTime.Now.AddDays(30); } }
	}
}
