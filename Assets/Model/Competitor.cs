using System;

namespace Model
{
    [Serializable]
    public class Competitor
    {
        public Int64 Id { get; set; }
        public String Title { get; set; }
        public Double Success_rate { get; set; }
        public Int64 Enterprise_id { get; set; }


        public Competitor(Int64 id, String title, Double success_rate, Int64 enterprise_id)
        {
            Id = id;
            Title = title;
            Success_rate = success_rate;
            Enterprise_id = enterprise_id;
        }
    }
}