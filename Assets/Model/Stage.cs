using System;
using System.Collections.Generic;

namespace Model
{
		[Serializable]
		public class Stage
		{
				public Int64 Id { get; set; }
				public String Title { get; set; }
				public Int32 Mission { get; set; }

				public Double TargetX { get; set; }
				public Double TargetY { get; set; }
				public Double TargetZ { get; set; }

				public virtual ICollection<Character> Services { get; set; }
		
				public Stage (Int64 id, String title, Int32 mission, Double targetX, Double targetY, Double targetZ)
				{
						Id = id;
						Title = title;
						Mission = mission;
						TargetX = targetX;
						TargetY = targetY;
						TargetZ = targetZ;
				}
		}
}