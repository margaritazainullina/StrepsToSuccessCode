//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.34014
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Model
{
	[Serializable]
	public class Taxation
	{
		public Int64 Id { get; set; }
		
		public Int16 Taxation_group { get; set; }
		public Decimal Max_revenue { get; set; }
		public Int32 Max_employee { get; set; }
		public Double VAT { get; set; }
		public Double Income_duty { get; set; }
        public Int16 Type { get; set; }
         	
		public Taxation(Int64 id, Int16 taxation_group, Decimal max_revenue, 
		                Int32 max_employee, Double VAT, Double income_duty, Int16 type)
		{
			Id = id;
			Taxation_group = taxation_group;
			Max_revenue = max_revenue;
			Max_employee = max_employee;
			this.VAT = VAT;
			Income_duty = income_duty;
            Type = type;
		}
	}
}
