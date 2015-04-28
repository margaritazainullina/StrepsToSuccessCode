using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class Enterprise_docsDAO
{

    public static List<Enterprise_docs> LoadEnterprise_docs(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM enterprise_docs WHERE enterprise_id=" + Character.Instance.Enterprise.Id + ";";
        MySqlDataReader data = command.ExecuteReader();

        List<Enterprise_docs> enterprise_docs = new List<Enterprise_docs>();
        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            Boolean availability = Convert.ToBoolean(data["availability"]);
            Boolean is_active = Convert.ToBoolean(data["is_active"]);
            Int64 document_id = Convert.ToInt64(data["document_id"]);
            DateTime expiration_date = Convert.ToDateTime(data["expiration_date"]);
            Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

            Enterprise_docs enterprise_doc = new Enterprise_docs(document_id, availability, is_active, expiration_date,
                                                                 enterprise_id);
            Debug.Log("Get enterprise_doc " + document_id);
            enterprise_docs.Add(enterprise_doc);
        }
        connection.Close();
        return enterprise_docs;
    }

    public static void InsertEnterprise_docs(MySqlConnection connection)
    {

        foreach (Enterprise_docs enterprise_doc in Character.Instance.Enterprise.Enterprise_docs)
        {
            connection.Open();
            String Query = "INSERT INTO enterprise_docs(document_id,availability,is_active,expiration_date,enterprise_id) values(" + enterprise_doc.Document_id + "," + enterprise_doc.Availability +
                "," + enterprise_doc.Is_active + ",'" + Helper.ToMySQLDateTimeFormat(enterprise_doc.Expiration_date) + "'," + enterprise_doc.Enterprise_id + ");";
            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert enterprise_doc " + enterprise_doc.Document_id);
            connection.Close();
        }

    }

    public static void UpdateEnterprise_docs(MySqlConnection connection)
    {

        foreach (Enterprise_docs enterprise_doc in Character.Instance.Enterprise.Enterprise_docs)
        {
            connection.Open();
            String Query = "UPDATE `enterprise_docs` SET availability=" + enterprise_doc.Availability + ", is_active=" +
                enterprise_doc.Is_active + ", expiration_date='" + Helper.ToMySQLDateTimeFormat(enterprise_doc.Expiration_date) +
                    " where enterprise_id=" + enterprise_doc.Enterprise_id + " AND document_id=" + enterprise_doc.Document_id + ";";
            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update character " + enterprise_doc.Document_id);
            connection.Close();
        }

    }

    public static void DeleteEnterprise_docs(MySqlConnection connection, List<Enterprise_docs> enterprise_docs)
    {

        foreach (Enterprise_docs enterprise_doc in enterprise_docs)
        {
            connection.Open();
            String Query = "DELETE FROM `enterprise_docs` WHERE enterprise_id=" + enterprise_doc.Enterprise_id + "AND document_id" + enterprise_doc.Document_id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete enterprise_doc ");
            connection.Close();
        }

    }

}