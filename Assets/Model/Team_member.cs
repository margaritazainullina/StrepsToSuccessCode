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

namespace Model
{
		[Serializable]
		public class Team_member
		{
				public virtual Int64 Employee_id { get; set; }
				public virtual Int64 Project_id { get; set; }
		
		
				public Team_member (Int64 employee_id, Int64 project_id)
				{
						Employee_id = employee_id;
						Project_id = project_id;			
				}
		}
}
