using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Asset
    {
        public Int64 Id { get; set; }
        public Decimal Value { get; set; }
        public DateTime Asset_date { get; set; }


        public Int64 Enterprise_id { get; set; }

        public Asset(Int64 id, Decimal value, DateTime asset_date, Int64 enterprise_id)
        {
            Id = id;
            Value = value;
            Asset_date = asset_date;
            Enterprise_id = enterprise_id;
        }
    }
}