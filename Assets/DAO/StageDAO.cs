using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class StageDAO
{
		//returns list with all characters from db
		public static List<Stage> GetStages (MySqlConnection connection)
		{
				connection.Open ();
				//retrieve from db
				MySqlCommand command = connection.CreateCommand ();
				command.CommandText = "SELECT * FROM `stage`";
				MySqlDataReader data = command.ExecuteReader ();
		
				List<Stage> stages = new List<Stage> ();
		
				//read data from dataReader and form list of Character instances
				while (data.Read()) {
						if (data.IsDBNull (0)) {
								break;
						}
						String title = (String)data ["title"];
						Int64 id = Convert.ToInt64 (data ["id"]);
						Int32 mission = Convert.ToInt32 (data ["mission"]);
						Double targetX = Convert.ToDouble (data ["targetX"]);
						Double targetY = Convert.ToDouble (data ["targetY"]);
						Double targetZ = Convert.ToDouble (data ["targetZ"]);
			
						Stage stage = new Stage (id, title, mission, targetX, targetY, targetZ);
						Debug.Log ("Get stage " + title);
						stages.Add (stage);
				}
				connection.Close ();
				return stages;
		}

}