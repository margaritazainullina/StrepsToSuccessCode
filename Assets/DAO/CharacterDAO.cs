﻿using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class CharacterDAO
{
		//returns list with all characters from db
		public static void LoadCharacterByName (MySqlConnection connection, String name)
		{
				connection.Open ();
				//retrieve from db
				MySqlCommand command = connection.CreateCommand ();
				command.CommandText = "SELECT * FROM `character` WHERE title='" + name + "';";
				MySqlDataReader data = command.ExecuteReader ();

				//read data from dataReader and form list of Character instances
				while (data.Read()) {
						Character.Instance.Title = (String)data ["title"];
						Character.Instance.Gender = (String)data ["gender"];
						Character.Instance.Id = Convert.ToInt64 (data ["id"]);
						Character.Instance.Level = Convert.ToInt32 (data ["level"]);
						Character.Instance.GameDay = (String)data ["GameDay"];
						Character.Instance.GameScene = (String)data ["GameScene"];

						Character.Instance.GameTime = Convert.ToDateTime (data ["GameTime"]);
						Character.Instance.Stage_Id = Convert.ToInt64 (data ["Stage_Id"]);
						Character.Instance.LocationX = Convert.ToDouble (data ["LocationX"]);
						Character.Instance.LocationY = Convert.ToDouble (data ["LocationY"]);
						Character.Instance.LocationZ = Convert.ToDouble (data ["LocationZ"]);

						Debug.Log ("Get character " + Character.Instance.Title);
				}
				connection.Close ();
		}

		public static void InsertCharacter (MySqlConnection connection)
		{
				connection.Open ();
				String Query = "INSERT INTO `character`(title,gender,level,id_enterprise) values('" + Character.Instance.Title
						+ "','" + Character.Instance.Gender + "'," + Character.Instance.Level + ");";
				Query = Helper.ReplaceInsertQueryVoidWithNulls (Query);
				MySqlCommand command = new MySqlCommand (Query, connection);

				command.ExecuteReader ();
				Debug.Log ("Insert character " + Character.Instance.Title);
				connection.Close ();

		}

		public static void UpdateCharacter (MySqlConnection connection)
		{
				connection.Open ();
				String Query = "UPDATE `character` SET title='" + Character.Instance.Title + "', gender='" + Character.Instance.Gender
						+ "', level=" + Character.Instance.Level + ", GameDay='" + Character.Instance.GameDay + "', GameScene='" + Character.Instance.GameScene 
						+ "', LocationX=" + Character.Instance.LocationX + ", LocationY=" + Character.Instance.LocationY + ", LocationZ=" + Character.Instance.LocationZ 
						+ ", Stage_Id=" + Character.Instance.Stage_Id + " where id=" + Character.Instance.Id + ";";

				Query = Helper.ReplaceUpdateQueryVoidWithNulls (Query);
				MySqlCommand command = new MySqlCommand (Query, connection);

				command.ExecuteReader ();
				Debug.Log ("Update character " + Character.Instance.Title);
				connection.Close ();
		}

		public static void DeleteCharacters (MySqlConnection connection, List<Character> characters)
		{
				foreach (Character c in characters) {
						connection.Open ();
						String Query = "DELETE FROM `character` WHERE id=" + c.Id + ";";
						MySqlCommand command = new MySqlCommand (Query, connection);

						command.ExecuteReader ();
						Debug.Log ("Delete character " + c.Title);
						connection.Close ();
				}
		}
}