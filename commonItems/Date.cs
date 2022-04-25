﻿using commonItems.Serialization;
using System;
using System.Text;

namespace commonItems; 

public class Date : IComparable<Date>, IPDXSerializable {
	public int Year { get; private set; } = 1;
	public int Month { get; private set; } = 1;
	public int Day { get; private set; } = 1;

	public Date() { }
	public Date(Date otherDate) {
		Year = otherDate.Year;
		Month = otherDate.Month;
		Day = otherDate.Day;
	}
	public Date(int year, int month, int day, bool AUC) {
		Year = AUC ? ConvertAUCToAD(year) : year;
		Month = month;
		Day = day;
	}
	public Date(int year, int month, int day) : this(year, month, day, false) { }
	public Date(string init) : this(init, false) { }
	public Date(string init, bool AUC) {
		if (init.Length < 1) {
			return;
		}
		init = StringUtils.RemQuotes(init);

		var firstDot = init.IndexOf('.');
		var lastDot = init.LastIndexOf('.');
		try {
			Year = int.Parse(init.Substring(0, firstDot));
			if (AUC) {
				Year = ConvertAUCToAD(Year);
			}
			Month = int.Parse(init.Substring(firstDot + 1, lastDot - firstDot - 1));
			Day = int.Parse(init.Substring(lastDot + 1));
		} catch (Exception e) {
			Logger.Warn("Problem inputting date: " + e);
			Year = 1;
			Month = 1;
			Day = 1;
		}
	}
	private static int DaysInMonth(int month) {
		if (month == 12) {
			return 31;
		}

		return daysByMonth[month] - daysByMonth[month - 1];
	}

	public void ChangeByDays(int days) {
		if (days > 0) {
			do {
				var currentMonthIndex = Month - 1;
				bool doesMonthChange;
				var currentDayInYear = daysByMonth[currentMonthIndex] + Day + days;
				if (Month < 12) {
					var nextMonthIndex = Month;
					doesMonthChange = currentDayInYear > daysByMonth[nextMonthIndex];
				} else {
					doesMonthChange = currentDayInYear > 365;
				}

				if (doesMonthChange) {
					var daysInMonth = DaysInMonth(Month);
					ChangeByMonths(1);

					var daysForward = daysInMonth - Day + 1;
					Day = 1;
					days -= daysForward;
				} else {
					Day += days;
					days = 0;
				}
			}
			while (days > 0);
		} else if (days < 0) {
			do {
				var currentMonthIndex = Month - 1;
				bool doesMonthChange;
				var currentDayInYear = daysByMonth[currentMonthIndex] + Day + days;
				if (Month > 1) {
					doesMonthChange = currentDayInYear <= daysByMonth[currentMonthIndex];
				} else {
					doesMonthChange = currentDayInYear <= 0;
				}

				if (doesMonthChange) {
					ChangeByMonths(-1);
					var daysInMonth = DaysInMonth(Month);
					var daysBackward = Day;
					Day = daysInMonth;
					days += daysBackward;
				} else {
					Day += days;
					days = 0;
				}
			}
			while (days < 0);
		}
	}

	public void ChangeByMonths(int months) {
		Year += months / 12;
		Month += months % 12;
		if (Month > 12) {
			++Year;
			Month -= 12;
		} else if (Month < 1) {
			--Year;
			Month += 12;
		}
	}

	public void ChangeByYears(int years) {
		Year += years;
	}

	private static int ConvertAUCToAD(int yearAUC) {
		var yearAD = yearAUC - 753;
		if (yearAD <= 0) {
			--yearAD;
		}
		return yearAD;
	}

	public double DiffInYears(Date rhs) {
		double years = Year - rhs.Year;
		years += (double)(CalculateDayInYear() - rhs.CalculateDayInYear()) / 365;

		return years;
	}

	public bool IsSet() {
		return !Equals(new Date());
	}

	public override string ToString() {
		var sb = new StringBuilder();
		sb.Append(Year);
		sb.Append('.');
		sb.Append(Month);
		sb.Append('.');
		sb.Append(Day);
		return sb.ToString();
	}

	public string Serialize(string indent, bool withBraces) {
		return ToString();
	}

	private static readonly int[] daysByMonth = {
		0,	// January
		31,	// February
		59,	// March
		90,	// April
		120, // May
		151, // June
		181, // July
		212, // August
		243, // September
		273, // October
		304, // November
		334	// December
	};

	private int CalculateDayInYear() {
		if (Month is >= 1 and <= 12) {
			return Day + daysByMonth[Month - 1];
		}
		return Day;
	}

	public override bool Equals(object? obj) {
		return obj is Date date &&
		       Year == date.Year &&
		       Month == date.Month &&
		       Day == date.Day;
	}

	public override int GetHashCode() {
		return HashCode.Combine(Year, Month, Day);
	}

	public static bool operator <(Date lhs, Date rhs) {
		return ((lhs.Year < rhs.Year) || ((lhs.Year == rhs.Year) && (lhs.Month < rhs.Month)) ||
		        ((lhs.Year == rhs.Year) && (lhs.Month == rhs.Month) && (lhs.Day < rhs.Day)));
	}
	public static bool operator >(Date lhs, Date rhs) {
		return ((lhs.Year > rhs.Year) || ((lhs.Year == rhs.Year) && (lhs.Month > rhs.Month)) ||
		        ((lhs.Year == rhs.Year) && (lhs.Month == rhs.Month) && (lhs.Day > rhs.Day)));
	}
	public static bool operator <=(Date lhs, Date rhs) {
		return (lhs.Equals(rhs) || (lhs < rhs));
	}
	public static bool operator >=(Date lhs, Date rhs) {
		return (lhs.Equals(rhs) || (lhs > rhs));
	}

	public int CompareTo(Date? obj) {
		if (obj is null) {
			return 1;
		}

		var result = Year.CompareTo(obj.Year);
		if (result != 0) {
			return result;
		}
		result = Month.CompareTo(obj.Month);
		if (result != 0) {
			return result;
		}
		return Day.CompareTo(obj.Day);
	}
	public static bool operator ==(Date? left, Date? right) {
		if (left is null) {
			return right is null;
		}
		return left.Equals(right);
	}
	public static bool operator !=(Date? left, Date? right) {
		return !(left == right);
	}
}