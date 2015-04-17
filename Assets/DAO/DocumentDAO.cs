using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class DocumentDAO
{
    //returns list with all characters from db
    public static List<Document> GetDocuments(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM `document`";
        MySqlDataReader data = command.ExecuteReader();

        List<Document> documents = new List<Document>();

        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            String title = (String)data["title"];
            String type = (String)data["type"];
            Int64 id = Convert.ToInt64(data["id"]);
            Int32 path = Convert.ToInt32(data["path"]);

            Document document = new Document(id, title, type, path);
            Debug.Log("Get character " + title);
            documents.Add(document);
        }
        connection.Close();
        return documents;
    }

    public static void InsertDocuments(MySqlConnection connection)
    {

        foreach (Enterprise_docs enterprise_docs in Character.Instance.Enterprise.Enterprise_docs)
        {
            connection.Open();
            String Query = "INSERT INTO document(title,type,path) values('" + enterprise_docs.Document.Title + "','" +
                    enterprise_docs.Document.Type + "'," + enterprise_docs.Document.Path + ");";

            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert document " + enterprise_docs.Document.Title);
            connection.Close();
        }

    }

    public static void UpdateDocuments(MySqlConnection connection)
    {

        foreach (Enterprise_docs enterprise_docs in Character.Instance.Enterprise.Enterprise_docs)
        {
            connection.Open();
            String Query = "UPDATE `document` SET title='" + enterprise_docs.Document.Title + "', type='" + enterprise_docs.Document.Type +
                    "', path=" + enterprise_docs.Document.Path + " where id=" + enterprise_docs.Document.Id + ";";

            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update document " + enterprise_docs.Document.Title);
            connection.Close();
        }

    }

    public static void DeleteDocuments(MySqlConnection connection, List<Document> documents)
    {

        foreach (Document document in documents)
        {
            connection.Open();
            String Query = "DELETE FROM `document` WHERE id=" + document.Id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete document " + document.Title);
            connection.Close();
        }

    }
}