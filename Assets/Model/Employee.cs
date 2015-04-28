using System;
using System.Collections.Generic;

namespace Model
{
    [Serializable]
    public class Employee
    {
        public Int64 Id { get; set; }
        public String Title { get; set; }
        public Double Qualification { get; set; }
        public Decimal Salary { get; set; }
        public Int64 Role_id { get; set; }
        public Int64 Enterprise_id { get; set; }

        public List<Salary_payment> Salary_payments { get; set; }

        public virtual Role Role { get; set; }

        public Employee(Int64 id, String title, Double qualification, Decimal salary,
                         Int64 role_id, Int64 enterprise_id)
        {
            Id = id;
            Title = title;
            Qualification = qualification;
            Salary = salary;
            Role_id = role_id;
            Enterprise_id = enterprise_id;
        }

    }
}