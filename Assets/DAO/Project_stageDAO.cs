using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class Project_stageDAO
{
    public static List<Project_stage> LoadProject_stages(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT project_stage.* FROM project_stage, project WHERE " +
            "project.enterprise_id =" + Character.Instance.Enterprise.Id + ";";
        MySqlDataReader data = command.ExecuteReader();

        List<Project_stage> project_stages = new List<Project_stage>();

        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            Int32? conception_hours = Helper.GetValueOrNull<Int32>(Convert.ToString(data["conception_hours"]));
            Int32? programming_hours = Helper.GetValueOrNull<Int32>(Convert.ToString(data["programming_hours"]));
            Int32? testing_hours = Helper.GetValueOrNull<Int32>(Convert.ToString(data["testing_hours"]));
            Int32? design_hours = Helper.GetValueOrNull<Int32>(Convert.ToString(data["design_hours"]));

            Double? conception_done = Helper.GetValueOrNull<Double>(Convert.ToString(data["conception_done"]));
            Double? programming_done = Helper.GetValueOrNull<Double>(Convert.ToString(data["programming_done"]));
            Double? testing_done = Helper.GetValueOrNull<Double>(Convert.ToString(data["testing_done"]));
            Double? design_done = Helper.GetValueOrNull<Double>(Convert.ToString(data["design_done"]));

            Int64 project_id = Convert.ToInt64(data["project_id"]);

            Project_stage project_stage = new Project_stage(project_id, conception_hours, programming_hours, testing_hours, design_hours,
                                              conception_done, programming_done, testing_done, design_done);
            Debug.Log("Get asset type=" + project_id);
            project_stages.Add(project_stage);
        }
        connection.Close();
        return project_stages;
    }

    public static void InsertProject_stages(MySqlConnection connection)
    {

        foreach (Project project in Character.Instance.Enterprise.Projects)
        {
            Project_stage project_stage = project.Project_stage;
            connection.Open();

            String Query = "INSERT INTO `project_stage` values(" + project_stage.Project_id + "," + project_stage.Conception_hours + "," + project_stage.Programming_hours + "," +
                project_stage.Testing_hours + "," + project_stage.Design_hours + "," + project_stage.Conception_done + "," + project_stage.Programming_done + ","
                    + project_stage.Testing_done + "," + project_stage.Design_done + ");";

            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert project_stage type=" + project_stage.Project_id + " and value=" + project_stage.Project_id);
            connection.Close();
        }

    }

    public static void UpdateProject_stages(MySqlConnection connection)
    {

        foreach (Project project in Character.Instance.Enterprise.Projects)
        {
            Project_stage project_stage = project.Project_stage;
            connection.Open();

            String Query = "UPDATE `project_stage` SET conception_hours=" + project_stage.Conception_hours + ", programming_hours=" + project_stage.Programming_hours + ", testing_hours=" +
                project_stage.Testing_hours + ", design_hours=" + project_stage.Design_hours + ", conception_done=" + project_stage.Conception_done + ", programming_done=" + project_stage.Programming_done + ", testing_done="
                    + project_stage.Testing_done + ", design_done=" + project_stage.Design_done + " where project_id=" + project_stage.Project_id + ";";
            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update project_stage type=" + project_stage.Project_id + " and value=" + project_stage.Project_id);
            connection.Close();
        }

    }

    public static void DeleteProject_stages(MySqlConnection connection, List<Project_stage> project_stages)
    {

        foreach (Project_stage project_stage in project_stages)
        {
            connection.Open();
            String Query = "DELETE FROM `project_stage` WHERE project_id=" + project_stage.Project_id + ";";

            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete project_stage type=" + project_stage.Project_id + " and value=" + project_stage.Project_id);
            connection.Close();
        }

    }

}