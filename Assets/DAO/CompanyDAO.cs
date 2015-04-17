using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class CompanyDAO
{
    //returns list with all characters from db
    public static List<Company> GetCompanies(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM `company`";
        MySqlDataReader data = command.ExecuteReader();

        List<Company> companies = new List<Company>();


        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            String title = Convert.ToString(data["title"]);
            Double share = Convert.ToDouble(data["share"]);
            Int64 id = Convert.ToInt64(data["id"]);
            Int32 period = Convert.ToInt32(data["period"]);
            Decimal investment = Convert.ToDecimal(data["investment"]);

            Company company = new Company(id, title, share, period, investment);
            Debug.Log("Get company " + title);
            companies.Add(company);
        }
        connection.Close();
        return companies;
    }

    public static void InsertCompanies(MySqlConnection connection)
    {
        Company company;

        foreach (Service service in Character.Instance.Enterprise.Services)
        {
            company = service.Company;
            connection.Open();

            String Query = "INSERT INTO company(title,share,period,investment) values('" +
                company.Title + "'," + company.Share + "," + company.Period + "," + company.Investment + ");";

            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert company " + company.Title);
            connection.Close();
        }

    }

    public static void UpdateCompanies(MySqlConnection connection)
    {
        Company company;

        foreach (Service service in Character.Instance.Enterprise.Services)
        {
            company = service.Company;
            connection.Open();

            String Query = "UPDATE `company` SET title='" + company.Title + "', share=" +
                    company.Share + ", period=" + company.Period + ", investment=" + company.Investment + " where id=" + company.Id + ";";

            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update company " + company.Title);
            connection.Close();
        }

    }

    public static void DeleteCompanies(MySqlConnection connection, List<Company> companies)
    {

        foreach (Company company in companies)
        {
            connection.Open();
            String Query = "DELETE FROM `company` WHERE id=" + company.Id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete company " + company.Title);
            connection.Close();
        }

    }
}