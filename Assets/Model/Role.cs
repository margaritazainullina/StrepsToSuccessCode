using System;
using System.Collections.Generic;

namespace Model
{
	[Serializable]
	public class Role
	{
		public Int64 Id { get; set; }
		public String Title { get; set; } 
		public Decimal Min_salary { get; set; }
		public Decimal Max_salary { get; set; }
		
		
		public virtual List<Employee> Employees {get; set;}
		
		public Role(Int64 id, String title, Decimal min_salary, Decimal max_salary)
		{
			Id = id;
			Title = title;
			Min_salary = min_salary;
			Max_salary = max_salary;
			
		}
	}
}