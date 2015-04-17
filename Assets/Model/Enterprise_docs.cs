using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Enterprise_docs
    {
        public Int64 Document_id { get; set; }
        public Boolean Availability { get; set; }
        public Boolean Is_active { get; set; }
        public DateTime Expiration_date { get; set; }

        public Int64 Enterprise_id { get; set; }

        public Document Document { get; set; }

        public Enterprise_docs(Int64 document_id, Boolean availability, Boolean is_active, DateTime expiration_date,
                                Int64 enterprise_id)
        {
            Document_id = document_id;
            Availability = availability;
            Is_active = is_active;
            Expiration_date = expiration_date;
            Enterprise_id = enterprise_id;
        }
    }
}