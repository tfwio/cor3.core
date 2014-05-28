/**
 * oIo * 2/23/2011 3:15 AM
 **/
using System;
namespace System
{
	public class DateHelper
	{
		#region region
		
		static public string GetDayNo(int dayno, int mode)
		{
			switch (mode)
			{
					case 0: return Days[dayno];
					case 1: return days[dayno];
					case 2: return dy[dayno];
					case 3: return dys[dayno];
					case 4: return Days[dayno].ToUpper();
					default: return "ERROR";
			}
		}
		static public string GetDayNo(DayOfWeek dayno, int mode)
		{
			return GetDayNo((int)dayno,mode);
		}
		static public string GetMonthNo(int monthNo, int mode)
		{
			switch (mode)
			{
					case 0: return Months[monthNo];
					case 1: return months[monthNo];
					case 2: return mts[monthNo];
					case 3: return mts[monthNo];
					case 4: return Months[monthNo].ToUpper();
					default: return "ERROR";
			}
		}

		internal static string DAYFILTER(string input, int day)
		{
			return input
				.Replace("@Days",	Days[day])
				.Replace("@days",	days[day])
				.Replace("@dys",	dys[day])
				.Replace("@dy",		dy[day])
				;
		}
		internal static string MONTHFILTER(string input, int mo)
		{
			return input
				.Replace("@Months",	Months[mo-1])
				.Replace("@months",	months[mo-1])
				.Replace("@mts",	mts[mo-1])
				;
		}
		
		static public string DAYMONTHFILTER(string input, int dayno, int monthno)
		{
			return MONTHFILTER(DAYFILTER(input,dayno),monthno);
		}
		
		static public string DAYMONTHFILTER(string input, DateTime date)
		{
			return DAYMONTHFILTER(input,(int)date.DayOfWeek,date.Month);
		}
		
		static public string PrintDayMonth(string input)
		{
			return DAYMONTHFILTER(input,DateTime.Now);
		}
		
		static public string ToDayOfWeek(DateTime day, string format, int mode)
		{
			return string.Format(format,GetDayNo(day.DayOfWeek,mode));
		}

		
		#endregion
		
		#region static
		static public explicit operator DateTime(DateHelper helper) { return helper.dateREF; }
		static public explicit operator DateHelper(DateTime dt) { return new DateHelper(dt); }
		
		static public readonly string[]
			Days =	{"Sunday","Monday","Tuesday","Wednesday","Thursday","Friday","Saturday"},
		days =	{"sunday","monday","tuesday","wednesday","thursday","friday","saturday"},
		dys =	{"sun","mon","tue","wed","thu","fri","sat"},
		dy =	{"su","mo","tu","we","th","fr","sa"},
		day_single =	{"s","m","t","w","t","f","s"};
		//
		static public readonly string[]
			Months =	{"January","February","March","April","May","June","July","August","September","October","November","December"},
		months =	{"january","february","march","april","may","june","july","august","september","october","november","december"},
		mts =		{"jan","feb","mar","apr","may","jun","jul","aug","sep","oct","nov","dec"},
		mt =		{"jn","fe","ma","ap","my","jn","jl","ag","sp","ot","nv","dc"};
		#endregion
		
		public int DaysInMonth { get {

				try
				{
					return DateTime.DaysInMonth(dateREF.Year,dateREF.Month);
				}
				catch
				{
					throw new ArgumentException(dateREF.ToString("yyyy-MM-dd"));
				}

			} }
		public int dayOfWeek { get { return (int)dateREF.DayOfWeek; } }
		
		public string str_Days { get { return Days[(int)dateREF.DayOfWeek]; } }
		public string str_days { get { return days[(int)dateREF.DayOfWeek]; } }
		public string str_dys { get { return dys[(int)dateREF.DayOfWeek]; } }
		public string str_dy { get { return dy[(int)dateREF.DayOfWeek]; } }
		public string str_Months { get { return Months[dateREF.Month-1]; } }
		public string str_months { get { return months[dateREF.Month-1]; } }
		public string str_mts { get { return mts[dateREF.Month-1]; } }
		
		public int DaysUntilFriday	{ get { return 5-((int)dateREF.DayOfWeek); } }
		public int DaysUntilMonday	{ get { return 1-((int)dateREF.DayOfWeek); } }
		public int DaysUntilSaturday	{ get { return 6-((int)dateREF.DayOfWeek); } }
		
		public bool IsJanuary { get { return this.dateREF.Month==1; } }
		public bool IsDecember { get { return this.dateREF.Month==12; } }

		static public DateTime GetOffset(DateTime relation, int years, int months, int days)
		{
			try
			{
				
			return new DateTime(
				relation.Year+years,
				relation.Month+months,
				relation.Day+days);
			}
			catch
			{
				return relation;
			}
		}
		
		public DateHelper MonthNext { get { return (DateHelper)GetOffset(new DateTime(dateREF.Year,dateREF.Month,1),0,1,0); } }
		public DateHelper MonthPrev { get { return (DateHelper)GetOffset(new DateTime(dateREF.Year,dateREF.Month,1),0,-1,0); } }
		
		public bool IsMonday { get { return dayOfWeek==(int)DayOfWeek.Monday; } }
		public bool IsFriday { get { return dayOfWeek==(int)DayOfWeek.Friday; } }
		public bool IsSaturday { get { return dayOfWeek==(int)DayOfWeek.Saturday; } }
		public bool IsSunday { get { return dayOfWeek==(int)DayOfWeek.Sunday; } }
		
		#if FALSE
		void nodo()
		{
			int dayOfWeek = (int) n.DayOfWeek;
			int daysInMonth = DateTime.DaysInMonth(n.Year,n.Month);
			int firstDayOfMonth = GetFirstDayOfMonth(n), no2 = firstDayOfMonth;
			int daysInFirstWeek = (7-firstDayOfMonth);
			
			controller.ViewData["dim"] = dim;
			controller.ViewData["dow"] = newdata.DayOfWeek;
			controller.ViewData["idow"] = DateHelper.days[(int)newdata.DayOfWeek];
			controller.ViewData["idows"] = DateHelper.days[(int)newdata.DayOfWeek];
			
			controller.ViewData["dlw"] = 6-((int)newdata.DayOfWeek);
			controller.ViewData["dtm"] = 1-((int)newdata.DayOfWeek);
			controller.ViewData["dtf"] = 5-((int)newdata.DayOfWeek);
			
			controller.ViewData["d-json"] = dfx;
		}
		#endif
		
		public DateTime DateREF {
			get { return dateREF; }
			set { dateREF = value; }
		} DateTime dateREF;
		public DateTime DateRELATION {
			get { return dateRELATION; }
			set { dateRELATION = value; }
		} DateTime dateRELATION;

		public DateHelper(DateTime dateREF)
		{
			this.dateRELATION = DateTime.Now;
			this.dateREF = dateREF;
		}
		public DateHelper(DateTime dateREF, DateTime dateRELATION)
		{
			this.dateREF		= dateREF;
			this.dateRELATION	= dateRELATION;
		}
		public DateHelper(string YYYY, string MM, string DD) : this(DateTime.Now,DateTime.Now,YYYY,MM,DD)
		{
		}
		public DateHelper(DateTime dateREF, string YYYY, string MM, string DD) : this(DateTime.Now,dateREF,YYYY,MM,DD)
		{
		}
		public DateHelper(DateTime dateRELATION, DateTime dateREF, string YYYY, string MM, string DD)
		{
			this.dateRELATION = dateRELATION;
			this.dateREF = new DateTime(
				string.IsNullOrEmpty(YYYY) ? int.Parse(YYYY) : this.dateRELATION.Year,
				string.IsNullOrEmpty(MM) ? int.Parse(MM) : this.dateRELATION.Month,
				string.IsNullOrEmpty(DD) ? int.Parse(DD) : this.dateRELATION.Day
			);
		}

	}


}
