using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class EnterpriseDAO
{
		//returns list with all characters from db
		public static Enterprise LoadEnterprise (MySqlConnection connection)
		{
				connection.Open ();
				//retrieve from db
				MySqlCommand command = connection.CreateCommand ();
				command.CommandText = "SELECT * FROM `enterprise` WHERE id=" + Character.Instance.Id;
				MySqlDataReader data = command.ExecuteReader ();

				List<Enterprise> enterprises = new List<Enterprise> ();

				Enterprise enterprise = null;

				//read data from dataReader and form list of Character instances
				while (data.Read()) {
						if (data.IsDBNull (0)) {
								break;
						}
						String title = Convert.ToString (data ["title"]);
						Decimal balance = Convert.ToDecimal (data ["balance"]);
						Double stationary = Convert.ToDouble (data ["stationary"]);
						Int64 id = Convert.ToInt64 (data ["id"]);
						Int16? type = Helper.GetValueOrNull<Int16> (Convert.ToString (data ["type"]));
						Int64? taxation_id = Helper.GetValueOrNull<Int64> (Convert.ToString (data ["taxation_id"]));

						enterprise = new Enterprise (id, title, balance, stationary, type, taxation_id);
						Debug.Log ("Get enterprise " + title);
						enterprises.Add (enterprise);
				}

				connection.Close ();
				return enterprise;
		}

		public static void InsertEnterprise (MySqlConnection connection)
		{
				connection.Open ();
				String Query = "INSERT INTO enterprise(id,title,balance,stationary,type,taxation_id) values(" + Character.Instance.Enterprise.Id + ",'" + Character.Instance.Enterprise.Title + "'," + Character.Instance.Enterprise.Balance + "," +
						Character.Instance.Enterprise.Stationary + "," + Character.Instance.Enterprise.Type + "," + Character.Instance.Enterprise.Taxation_id + ");";

				Query = Helper.ReplaceInsertQueryVoidWithNulls (Query);
				MySqlCommand command = new MySqlCommand (Query, connection);

				command.ExecuteReader ();
				Debug.Log ("Insert enterprise " + Character.Instance.Enterprise.Title);
				connection.Close ();
		}

		public static void UpdateEnterprise (MySqlConnection connection)
		{
				connection.Open ();
				String Query = "UPDATE `enterprise` SET title='" + Character.Instance.Enterprise.Title + "', balance=" + Character.Instance.Enterprise.Balance + ", stationary=" + Character.Instance.Enterprise.Stationary +
						", type=" + Character.Instance.Enterprise.Type + ", taxation_id=" + Character.Instance.Enterprise.Taxation_id + " where id=" + Character.Instance.Enterprise.Id + ";";

				Query = Helper.ReplaceUpdateQueryVoidWithNulls (Query);
				MySqlCommand command = new MySqlCommand (Query, connection);

				command.ExecuteReader ();
				Debug.Log ("Update enterprise " + Character.Instance.Enterprise.Title);
				connection.Close ();
		}

		public static void DeleteEnterprises (MySqlConnection connection)
		{
				connection.Open ();
				String Query = "DELETE FROM `enterprise` WHERE id=" + Character.Instance.Enterprise.Id + ";";
				MySqlCommand command = new MySqlCommand (Query, connection);

				command.ExecuteReader ();
				Debug.Log ("Delete enterprise " + Character.Instance.Enterprise.Title);
				connection.Close ();
		}
}