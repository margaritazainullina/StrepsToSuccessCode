using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class ProjectDAO
{
		public static List<Project> LoadProjects (MySqlConnection connection)
		{
				connection.Open ();
				//retrieve from db
				MySqlCommand command = connection.CreateCommand ();
				command.CommandText = "SELECT * FROM project WHERE enterprise_id=" + Character.Instance.Enterprise.Id + ";";
				MySqlDataReader data = command.ExecuteReader ();

				List<Project> projects = new List<Project> ();
				//read data from dataReader and form list of Character instances
				while (data.Read()) {
						if (data.IsDBNull (0)) {
								break;
						}
						Int64 id = Convert.ToInt64 (data ["id"]);
						String title = Convert.ToString (data ["title"]);
						DateTime planned_begin_date = Convert.ToDateTime (data ["planned_begin_date"]);
						DateTime planned_end_date = Convert.ToDateTime (data ["planned_end_date"]);
						DateTime real_begin_date = Convert.ToDateTime (data ["real_begin_date"]);
						DateTime real_end_date = Convert.ToDateTime (data ["real_end_date"]);
						Int32 state = Convert.ToInt32 (data ["state"]);
						Decimal stated_budget = Convert.ToDecimal (data ["stated_budget"]);
						Int64 enterprise_id = Convert.ToInt64 (data ["enterprise_id"]);
						Decimal expenditures = Convert.ToDecimal (data ["expenditures"]);

						Project project = new Project (id, title, planned_begin_date, planned_end_date, real_begin_date, real_end_date,
                              state, stated_budget, enterprise_id, expenditures);
						Debug.Log ("Get character " + id);
						projects.Add (project);
				}
				connection.Close ();
				return projects;
		}

		public static Int64 GetLastProjectId (MySqlConnection connection)
		{
				connection.Open ();
				//retrieve from db
				MySqlCommand command = connection.CreateCommand ();
				command.CommandText = "SELECT Max(Id) as id FROM project;";
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

		public static void InsertProjects (MySqlConnection connection)
		{

				foreach (Project project in Character.Instance.Enterprise.Projects) {
						connection.Open ();
						String Query = "INSERT INTO project(title,planned_begin_date,planned_end_date,real_begin_date,real_end_date,state,stated_budget,expenditures,enterprise_id) values('" + project.Title + "','" + Helper.ToMySQLDateTimeFormat (project.Planned_begin_date) + "','" +
								Helper.ToMySQLDateTimeFormat (project.Planned_end_date) + "','" + Helper.ToMySQLDateTimeFormat (project.Real_begin_date) + "','" + Helper.ToMySQLDateTimeFormat (project.Real_end_date) + "'," +
								project.State + "," + project.Stated_budget + "," + project.Expenditures + "," + project.Enterprise_id + ");";
						Query = Helper.ReplaceInsertQueryVoidWithNulls (Query);
						MySqlCommand command = new MySqlCommand (Query, connection);

						command.ExecuteReader ();
						Debug.Log ("Insert project " + project.Id);
						connection.Close ();
				}

		}

		public static void UpdateProjects (MySqlConnection connection)
		{

				foreach (Project project in Character.Instance.Enterprise.Projects) {
						connection.Open ();
						String Query = "UPDATE `project` SET planned_begin_date='" + Helper.ToMySQLDateTimeFormat (project.Planned_begin_date) + "', title='" + project.Title + "', planned_begin_date='" + Helper.ToMySQLDateTimeFormat (project.Planned_end_date) +
								"', real_begin_date='" + Helper.ToMySQLDateTimeFormat (project.Real_begin_date) + "', real_end_date='" + Helper.ToMySQLDateTimeFormat (project.Real_end_date) +
								"', state=" + project.State + ", stated_budget=" + project.Stated_budget + ", expenditures=" + project.Expenditures + ", enterprise_id=" + project.Enterprise_id + " where id=" + project.Id + ";";
						Query = Helper.ReplaceUpdateQueryVoidWithNulls (Query);
						MySqlCommand command = new MySqlCommand (Query, connection);

						command.ExecuteReader ();
						Debug.Log ("Update project " + project.Id);
						connection.Close ();
				}

		}

		public static void DeleteCharacters (MySqlConnection connection, List<Project> projects)
		{

				foreach (Project project in projects) {
						connection.Open ();
						String Query = "DELETE FROM `project` WHERE id=" + project.Id + ";";
						MySqlCommand command = new MySqlCommand (Query, connection);

						command.ExecuteReader ();
						Debug.Log ("Delete project " + project.Id);
						connection.Close ();
				}

		}
}