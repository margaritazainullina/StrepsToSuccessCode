using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class EmployeeDAO
{
		public static List<Employee> LoadEmployees (MySqlConnection connection)
		{
				connection.Open ();
				//retrieve from db
				MySqlCommand command = connection.CreateCommand ();
				command.CommandText = "SELECT * FROM employee WHERE enterprise_id=" + Character.Instance.Enterprise.Id + ";";
				MySqlDataReader data = command.ExecuteReader ();

				List<Employee> employees = new List<Employee> ();

				//read data from dataReader and form list of Character instances
				while (data.Read()) {
						if (data.IsDBNull (0)) {
								break;
						}
						String title = (String)data ["title"];
						Double qualification = Convert.ToDouble (data ["qualification"]);
						Int64 id = Convert.ToInt64 (data ["id"]);
						Decimal salary = Convert.ToDecimal (data ["salary"]);
						Int64 role_id = Convert.ToInt64 (data ["role_id"]);
						Int64 enterprise_id = Convert.ToInt64 (data ["enterprise_id"]);

						Employee employee = new Employee (id, title, qualification, salary, role_id, enterprise_id);
						Debug.Log ("Get employee " + title);
						employees.Add (employee);
				}
				connection.Close ();
				return employees;
		}

		public static Int64 GetLastEmployeeId (MySqlConnection connection)
		{
				connection.Open ();
				//retrieve from db
				MySqlCommand command = connection.CreateCommand ();
				command.CommandText = "SELECT Max(Id) as id FROM employee;";
				MySqlDataReader data = command.ExecuteReader ();

				Int64 maxId = 0;
				//read data from dataReader and form list of Character instances
				while (data.Read()) {
						if (data.IsDBNull (0)) {
								break;
						}
						maxId = Convert.ToInt64 (data ["id"]);
				}
				connection.Close ();
				return maxId;
		}

		public static void InsertEmployees (MySqlConnection connection)
		{
				foreach (Employee employee in Character.Instance.Enterprise.Employees) {
						connection.Open ();
						String Query = "INSERT INTO employee(title,salary,qualification,role_id,enterprise_id) values('" + employee.Title +
								"'," + employee.Qualification + "," + employee.Salary + "," + employee.Role_id +
								"," + employee.Enterprise_id + ");";
						Query = Helper.ReplaceInsertQueryVoidWithNulls (Query);
						MySqlCommand command = new MySqlCommand (Query, connection);

						command.ExecuteReader ();
						Debug.Log ("Insert employee " + employee.Title);
						connection.Close ();
				}

		}

		public static void UpdateEmployees (MySqlConnection connection)
		{

				foreach (Employee employee in Character.Instance.Enterprise.Employees) {
						connection.Open ();
						String Query = "UPDATE `employee` SET title='" + employee.Title + "', qualification=" +
								employee.Qualification + ", salary=" + employee.Salary + ", role_id=" + employee.Role_id +
								", enterprise_id=" + employee.Enterprise_id + " where id=" + employee.Id + ";";
						Query = Helper.ReplaceUpdateQueryVoidWithNulls (Query);
						MySqlCommand command = new MySqlCommand (Query, connection);

						command.ExecuteReader ();
						Debug.Log ("Update employee " + employee.Title);
						connection.Close ();
				}

		}

		public static void DeleteEmployees (MySqlConnection connection, List<Employee> employees)
		{

				foreach (Employee employee in employees) {
						connection.Open ();
						String Query = "DELETE FROM `employee` WHERE id=" + employee.Id + ";";
						MySqlCommand command = new MySqlCommand (Query, connection);

						command.ExecuteReader ();
						Debug.Log ("Delete employee " + employee.Title);
						connection.Close ();
				}

		}
}