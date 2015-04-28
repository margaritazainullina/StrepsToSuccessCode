
using System;
using System.Collections.Generic;

namespace Model
{
		[Serializable]
		public class Salary_payment
		{
				public Int64 Id { get; set; }
				public Int64 Employee_Id { get; set; }
				public DateTime Date { get; set; }
				public Int32? Hours_worked { get; set; }
				public Decimal? Salary { get; set; }
		
				public Salary_payment (Int64 id, Int64 employee_Id, DateTime date, Int32? hours_worked, Decimal? salary)
				{
						Id = id;
						Date = date;
						Hours_worked = hours_worked;
						Salary = salary;
						Employee_Id = employee_Id;
				}
		}
}