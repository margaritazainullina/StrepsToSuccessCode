using System;
namespace Model
{
    [Serializable]
    public class Enterprise_equipment
    {
        public DateTime Purchase_date { get; set; }
        public Int32? Quantity { get; set; }
        public Int32? Lease_term { get; set; }
        public Boolean? IsRunning { get; set; }

        public Int64 Enterprise_id { get; set; }
        public Int64 Equipment_id { get; set; }

        public virtual Equipment Equipment { get; set; }

        public Enterprise_equipment(DateTime purchase_date, Int32? quantity, Int32? lease_term,
                                     Boolean? isRunning, Int64 enterprise_id, Int64 equipment_id)
        {
            Purchase_date = purchase_date;
            Quantity = quantity;
            Lease_term = lease_term;
            IsRunning = isRunning;
            Enterprise_id = enterprise_id;
            Equipment_id = equipment_id;
        }
    }
}

