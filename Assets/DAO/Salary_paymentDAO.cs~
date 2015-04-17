using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
public class Salary_paymentDAO
{

		public static List<Salary_payment> LoadSalary_paymentsByEmployee (MySqlConnection connection, Int64 employee)
		{
				connection.Open ();
				//retrieve from db
				MySqlCommand command = connection.CreateCommand ();
				command.CommandText = "SELECT * FROM salary_payment " +
						"WHERE salary_payment.employee_id=" + employee + ";";
				MySqlDataReader data = command.ExecuteReader ();

				List<Salary_payment> salary_payments = new List<Salary_payment> ();

				//read data from dataReader and form list of Character instances
				while (data.Read()) {
						if (data.IsDBNull (0)) {
								break;
						}
						Int64 id = Convert.ToInt64 (data ["id"]);
						Int64 employee_Id = Convert.ToInt64 (data ["employee_Id"]);
						DateTime date = Convert.ToDateTime (data ["date"]);
						Int32? hours_worked = Helper.GetValueOrNull<Int32> (Convert.ToString (data ["hours_worked"]));
						Decimal? salary = Helper.GetValueOrNull<Decimal> (Convert.ToString (data ["salary"]));

						Salary_payment salary_payment = new Salary_payment (id, employee_Id, date, hours_worked, salary);
						Debug.Log ("Get employee_Id of salary payment=" + employee_Id);
						salary_payments.Add (salary_payment);
				}
				connection.Close ();
				return salary_payments;
		}

		public static void InsertSalary_payments (MySqlConnection connection)
		{

				foreach (Employee employee in Character.Instance.Enterprise.Employees) {

						if (employee.Salary_payments == null)
								continue;
						foreach (Salary_payment salary_payment in employee.Salary_payments) {
								connection.Open ();
								String Query = "INSERT INTO salary_payment(`date`,hours_worked, salary,employee_id) values('" + Helper.ToMySQLDateTimeFormat (salary_payment.Date) + "'," + salary_payment.Hours_worked + "," +
										salary_payment.Salary + "," + salary_payment.Employee_Id + ");";


								Query = Helper.ReplaceInsertQueryVoidWithNulls (Query);

								MySqlCommand command = new MySqlCommand (Query, connection);

								command.ExecuteReader ();
								Debug.Log ("Insert salary_payment id=" + salary_payment.Id);
								connection.Close ();
						}
				}

		}
		public static void UpdateSalary_payments (MySqlConnection connection)
		{

				foreach (Employee employee in Character.Instance.Enterprise.Employees) {

						if (employee.Salary_payments == null)
								continue;
						foreach (Salary_payment salary_payment in employee.Salary_payments) {
								connection.Open ();
								String Query = "UPDATE `salary_payment` SET date='" + Helper.ToMySQLDateTimeFormat (salary_payment.Date) + "', hours_worked=" + salary_payment.Hours_worked +
										", salary=" + salary_payment.Salary + ", employee_id" + salary_payment.Employee_Id + " where id=" + salary_payment.Id + ";";


								Query = Helper.ReplaceUpdateQueryVoidWithNulls (Query);

								MySqlCommand command = new MySqlCommand (Query, connection);

								command.ExecuteReader ();
								Debug.Log ("Update salary_payment id=" + salary_payment.Id);
								connection.Close ();
						}
				}

		}

		public static void DeleteSalary_payments (MySqlConnection connection, List<Salary_payment> salary_payments)
		{

				foreach (Salary_payment salary_payment in salary_payments) {
						connection.Open ();
						String Query = "DELETE FROM `salary_payment` WHERE id=" + salary_payment.Id + ";";
						MySqlCommand command = new MySqlCommand (Query, connection);

						command.ExecuteReader ();
						Debug.Log ("Delete salary_payment id=" + salary_payment.Id);
						connection.Close ();
				}

		}

}