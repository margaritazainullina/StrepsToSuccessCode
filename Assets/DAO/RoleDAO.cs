using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class RoleDAO
{
    //returns list with all characters from db
    public static List<Role> GetRoles(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM `role`";
        MySqlDataReader data = command.ExecuteReader();

        List<Role> roles = new List<Role>();

        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            String title = (String)data["title"];
            Int64 id = Convert.ToInt64(data["id"]);
            Decimal min_salary = Convert.ToDecimal(data["min_salary"]);
            Decimal max_salary = Convert.ToDecimal(data["max_salary"]);

            Role role = new Role(id, title, min_salary, max_salary);
            Debug.Log("Get role " + title);
            roles.Add(role);
        }
        connection.Close();
        return roles;
    }

    public static void InsertRoles(MySqlConnection connection, List<Role> roles)
    {

        foreach (Role role in roles)
        {
            connection.Open();
            String Query = "INSERT INTO role(title,min_salary,max_salary) values('" + role.Title + "'," +
                role.Min_salary + "," + role.Max_salary + ");";

            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert role " + role.Title);
            connection.Close();
        }

    }
    public static void UpdateRoles(MySqlConnection connection, List<Role> roles)
    {

        foreach (Role role in roles)
        {
            connection.Open();
            String Query = "UPDATE `role` SET title='" + role.Title + "', min_salary=" + role.Min_salary +
                ", max_salary=" + role.Max_salary + " where id=" + role.Id + ";";

            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update role " + role.Title);
            connection.Close();
        }

    }

    public static void DeleteRoles(MySqlConnection connection, List<Role> roles)
    {

        foreach (Role role in roles)
        {
            connection.Open();
            String Query = "DELETE FROM `role` WHERE id=" + role.Id + ";";

            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete role " + role.Title);
            connection.Close();
        }

    }
}