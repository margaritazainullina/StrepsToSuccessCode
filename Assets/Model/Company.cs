using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Company
    {
        public Int64 Id { get; set; }
        public String Title { get; set; }
        public Double Share { get; set; }
        public Int32 Period { get; set; }
        public Decimal Investment { get; set; }


        public virtual ICollection<Service> Services { get; set; }

        public Company(Int64 id, String title, Double share, Int32 period,
                        Decimal investment)
        {
            Id = id;
            Title = title;
            Share = share;
            Period = period;
            Investment = investment;
        }
    }
}