using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class ServiceDAO
{

    public static List<Service> LoadServices(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM service WHERE enterprise_id=" + Character.Instance.Enterprise.Id + ";";
        MySqlDataReader data = command.ExecuteReader();

        List<Service> services = new List<Service>();

        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            String title = (String)data["title"];
            Int64 company_id = Convert.ToInt64(data["company_id"]);
            Decimal price = Convert.ToDecimal(data["price"]);
            Int32 period = Convert.ToInt32(data["period"]);
            Int32 periodsPaid = Convert.ToInt32(data["periods_paid"]);
            Decimal effectiveness = Convert.ToDecimal(data["effectiveness"]);
            Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

            Service service = new Service(company_id, title, price, period, periodsPaid, effectiveness, enterprise_id);
            // effectiveness);
            Debug.Log("Get service " + title);
            services.Add(service);
        }
        connection.Close();
        return services;
    }

    public static void InsertServices(MySqlConnection connection)
    {

        foreach (Service service in Character.Instance.Enterprise.Services)
        {
            connection.Open();
            String Query = "INSERT INTO service(company_id,title,price,period,effectiveness,periods_paid,enterprise_id) values(" + service.Company_id + ",'" + service.Title + "'," +
                service.Price + "," + service.Period + "," + service.PeriodsPaid + "," + service.Effectiveness + "," + service.Enterprise_id + ");";


            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);

            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert service " + service.Title);
            connection.Close();
        }

    }
    public static void UpdateServices(MySqlConnection connection)
    {

        foreach (Service service in Character.Instance.Enterprise.Services)
        {
            connection.Open();
            String Query = "UPDATE `service` SET title='" + service.Title + "', price=" + service.Price +
                ", period=" + service.Period + ", periods_paid=" + service.PeriodsPaid + ", effectiveness=" + service.Effectiveness + ", enterprise_id=" +
                    service.Enterprise_id + " where company_id=" + service.Company_id + ";";


            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);

            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update service " + service.Title);
            connection.Close();
        }

    }

    public static void DeleteServices(MySqlConnection connection, List<Service> services)
    {

        foreach (Service service in services)
        {
            connection.Open();
            String Query = "DELETE FROM `character` WHERE company_id=" + service.Company_id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete service " + service.Title);
            connection.Close();
        }

    }

}