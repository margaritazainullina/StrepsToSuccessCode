using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;

using AssemblyCSharp;
public class TaxationDAO
{

    //returns list with all characters from db
    public static List<Taxation> GetTaxations(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM `taxation`";
        MySqlDataReader data = command.ExecuteReader();

        List<Taxation> taxations = new List<Taxation>();

        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            Int64 id = Convert.ToInt64(data["id"]);
            Int16 taxation_group = Convert.ToInt16(data["taxation_group"]);
            Decimal max_revenue = Convert.ToDecimal(data["max_revenue"]);
            Int32 max_employee = Convert.ToInt32(data["max_employee"]);

            Double VAT = Convert.ToDouble(data["VAT"]);
            Double income_duty = Convert.ToDouble(data["income_duty"]);
            Int16 type = Convert.ToInt16(data["type"]);

            Taxation taxation = new Taxation(id, taxation_group, max_revenue, max_employee,
                                             VAT, income_duty, type);
            Debug.Log("Get taxation " + id);
            taxations.Add(taxation);
        }
        connection.Close();
        return taxations;
    }

    public static void InsertTaxations(MySqlConnection connection)
    {
        Taxation taxation = Character.Instance.Enterprise.Taxation;

        connection.Open();
        String Query = "INSERT INTO taxation(taxation_group,max_revenue,max_employee,VAT,income_duty,type) values(" + taxation.Taxation_group + "," +
            taxation.Max_revenue + "," + taxation.Max_employee + "," + taxation.VAT + "," + taxation.Income_duty + "," + taxation.Type + ");";

        Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);

        MySqlCommand command = new MySqlCommand(Query, connection);

        command.ExecuteReader();
        Debug.Log("Insert role " + taxation.Id);
        connection.Close();

    }
    public static void UpdateTaxation(MySqlConnection connection)
    {
        Taxation taxation = Character.Instance.Enterprise.Taxation;

        connection.Open();
        String Query = "UPDATE `taxation` SET taxation_group=" + taxation.Taxation_group + ", max_revenue=" + taxation.Max_revenue +
            ", max_employee=" + taxation.Max_employee + ", VAT=" + taxation.VAT + ", income_duty=" + taxation.Income_duty + ", type=" + taxation.Type + " where id=" + taxation.Id + ";";

        Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);


        MySqlCommand command = new MySqlCommand(Query, connection);

        command.ExecuteReader();
        Debug.Log("Update role " + taxation.Id);
        connection.Close();
    }

    public static void DeleteTaxations(MySqlConnection connection, List<Taxation> taxations)
    {
        foreach (Taxation taxation in taxations)
        {
            connection.Open();
            String Query = "DELETE FROM `taxation` WHERE id=" + taxation.Id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete role " + taxation.Id);
            connection.Close();
        }
    }
}