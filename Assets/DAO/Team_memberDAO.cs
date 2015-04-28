using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class Team_memberDAO
{
    public static List<Team_member> LoadTeam_members(MySqlConnection connection)
    {
        List<Team_member> team_members = new List<Team_member>();
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT Team_member.* FROM project, team_member WHERE " +
                "team_member.project_id=project.id AND project.enterprise_id =" + Character.Instance.Enterprise.Id + ";";
        MySqlDataReader data = command.ExecuteReader();

        Team_member team_member = null;

        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            Int64 employee_id = Convert.ToInt64(data["employee_id"]);
            Int64 project_id = Convert.ToInt64(data["project_id"]);

            team_member = new Team_member(employee_id, project_id);

            Debug.Log("Get Team_members employee_id=" + employee_id + " and project_id=" + project_id);
            team_members.Add(team_member);
        }
        connection.Close();
        return team_members;
    }

    //What if there's no element for id
    public static void InsertTeam_members(MySqlConnection connection, List<Team_member> team_members)
    {

        foreach (Team_member team_member in team_members)
        {
            connection.Open();
            String Query = "INSERT INTO Team_member(employee_id,project_id) values(" + team_member.Employee_id + "," + team_member.Project_id + ");";

            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);

            MySqlCommand command = new MySqlCommand(Query, connection);
            command.ExecuteReader();

            Debug.Log("Inserted Team_member employee_id=" + team_member.Employee_id + " and project_id=" + team_member.Project_id);
            connection.Close();
        }

    }

    public static void DeleteTeam_members(MySqlConnection connection, List<Team_member> team_members)
    {

        foreach (Team_member team_member in team_members)
        {
            connection.Open();
            String Query = "DELETE FROM `asset` WHERE employee_id=" + team_member.Employee_id + " AND project_id=" + team_member.Project_id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Deleted Team_member employee_id=" + team_member.Employee_id + " and project_id=" + team_member.Project_id);
            connection.Close();
        }

    }


}