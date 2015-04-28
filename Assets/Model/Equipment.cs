using System;
using System.Collections.Generic;

namespace Model
{
		[Serializable]
		public class Equipment
		{
				public Int64 Id { get; set; }
				public String Title { get; set; }
				public Decimal Price { get; set; }


				public virtual ICollection<Enterprise_equipment> Enterprise_equipments { get; set; }

				public Equipment (Int64 id, String title, Decimal price)
				{
						Id = id;
						Title = title;
						Price = price;
				}
		}
}