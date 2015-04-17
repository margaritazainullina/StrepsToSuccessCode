using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class CompetitorDAO
{
    public static List<Competitor> LoadCompetitors(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM competitor WHERE enterprise_id=" + Character.Instance.Enterprise.Id + ";";
        MySqlDataReader data = command.ExecuteReader();

        List<Competitor> compatitors = new List<Competitor>();
        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            String title = (String)data["title"];
            Double success_rate = Convert.ToDouble(data["success_rate"]);
            Int64 id = Convert.ToInt64(data["id"]);
            Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

            Competitor compatitor = new Competitor(id, title, success_rate, enterprise_id);
            Debug.Log("Get competitor " + title);
            compatitors.Add(compatitor);
        }
        connection.Close();
        return compatitors;
    }

    public static void InsertCompetitors(MySqlConnection connection)
    {

        foreach (Competitor compatitor in Character.Instance.Enterprise.Competitors)
        {
            connection.Open();
            String Query = "INSERT INTO competitor(title,success_rate,enterprise_id) values('" + compatitor.Title + "'," +
                compatitor.Success_rate + "," + compatitor.Enterprise_id + ");";
            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert competitor " + compatitor.Title);
            connection.Close();
        }

    }

    public static void UpdateCompetitors(MySqlConnection connection)
    {

        foreach (Competitor compatitor in Character.Instance.Enterprise.Competitors)
        {
            connection.Open();
            String Query = "UPDATE `competitor` SET title='" + compatitor.Title + "', success_rate=" + compatitor.Success_rate +
                ", enterprise_id=" + compatitor.Enterprise_id + " where id=" + compatitor.Id + ";";
            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update competitor " + compatitor.Title);
            connection.Close();
        }

    }

    public static void DeleteCompetitors(MySqlConnection connection, List<Competitor> compatitors)
    {

        foreach (Competitor compatitor in compatitors)
        {
            connection.Open();
            String Query = "DELETE FROM `competitor` WHERE id=" + compatitor.Id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete competitor " + compatitor.Title);
            connection.Close();
        }

    }
}