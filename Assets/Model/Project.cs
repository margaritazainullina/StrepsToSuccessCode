using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;

namespace Model
{
		[Serializable]
		public class Project
		{
				public Int64 Id { get; set; }
				public String Title { get; set; }
				public DateTime Planned_begin_date { get; set; }
				public DateTime Planned_end_date { get; set; }
				public DateTime Real_begin_date { get; set; }
				public DateTime Real_end_date { get; set; }
				public Int32 State { get; set; }
				public Decimal Stated_budget { get; set; }
				public Decimal Expenditures { get; set; }
				public Int64 Enterprise_id { get; set; }

				public virtual List<Employee> Employees { get; set; }

				public virtual Product Product { get; set; }

				public virtual Project_stage Project_stage { get; set; }

				public Project (Int64 id, String title, DateTime planned_begin_date, DateTime planned_end_date,
                        DateTime real_begin_date, DateTime real_end_date, Int32 state,
                        Decimal stated_budget, Int64 enterprise_id, Decimal expenditures)
				{
						Title = title;
						Planned_begin_date = planned_begin_date;
						Planned_end_date = planned_end_date;
						Id = id;
						Real_begin_date = real_begin_date;
						Real_end_date = real_end_date;
						State = state;
						Stated_budget = stated_budget;
						Expenditures = expenditures;
						Enterprise_id = enterprise_id;
				}

				public Project (String title, DateTime planned_begin_date, DateTime planned_end_date,
                       Decimal stated_budget)
            : this(0, title, planned_begin_date, planned_end_date, new DateTime(), new DateTime(), 0,
                   stated_budget, 0, 0)
				{
				}

				public void Start ()
				{
						this.State = 1;
						this.Real_begin_date = DateTime.Now;
						//Enterprise.Projects.Add (this); ?????????????????????????????????????????/
				}

				public void AppoInt32Employee (Employee employee)
				{
						this.Employees.Add (employee);
				}

				public void RemoveEmployee (Employee employee)
				{
						this.Employees.Remove (employee);
				}

				public void Cancel (MySqlConnection connection)
				{
						this.Employees.Clear ();
						this.State = 3;
				}
				///////TROUBLE HERE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
				public void MakeProgress (DateTime salary_payment_date)
				{
						Double conception_hours = 0;
						Double programming_hours = 0;
						Double testing_hours = 0;
						Double design_hours = 0;

						foreach (Employee e in this.Employees) {

								foreach (Salary_payment sp in e.Salary_payments) {
										Int32 hours_worked = (Int32)sp.Hours_worked;

										switch (e.Role.Title) {
										case "Analyst":
												{
														conception_hours += (Int32)hours_worked * e.Qualification;
														break;
												}
										case "Programmer":
												{
														programming_hours += (Int32)hours_worked * e.Qualification;
														break;
												}
										case "Tester":
												{
														testing_hours += (Int32)hours_worked * e.Qualification;
														break;
												}
										case "Designer":
												{
														design_hours += (Int32)hours_worked * e.Qualification;
														break;
												}
										}

								}
						}
				}

				public void Complete (String product_title)
				{
						//Inserting new product
						Decimal prime_cost = this.Stated_budget - this.Expenditures;
						Double quality = 1 + Convert.ToDouble (prime_cost) / Convert.ToDouble (this.Stated_budget) + 1 +
								(this.Real_end_date - this.Planned_end_date).TotalDays / (this.Planned_end_date - this.Planned_begin_date).TotalDays;

						this.Product = new Product (this.Id, product_title, prime_cost, quality, prime_cost);

						//Updating prject state to finished
						this.State = 2;
						this.Real_end_date = DateTime.Now;

						//Updating employee qualification
						if (quality > 1) {
								Dictionary<Int64, Double> employeeData = new Dictionary<Int64, Double> ();
								foreach (Employee e in this.Employees) {
										Int32 hours_worked = 0;
										foreach (Salary_payment sp in e.Salary_payments) {
												hours_worked += (Int32)sp.Hours_worked;
										}
										e.Qualification += (hours_worked / (Real_end_date - Real_begin_date).TotalDays) * Product.Quality;
								}
						}

						//changing enterprise budget
						Character.Instance.Enterprise.Balance += Product.Price;
						Character.Instance.Enterprise.Assets.Add (new Asset (0, Product.Price, DateTime.Now, Character.Instance.Enterprise.Id));

						//delete team members
						this.Employees.Clear ();
				}
		}
}