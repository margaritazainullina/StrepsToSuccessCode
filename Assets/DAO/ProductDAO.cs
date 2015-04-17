using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class ProductDAO
{
    public static List<Product> LoadProducts(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT product.* FROM product, project WHERE " +
            "product.project_id=project.id AND project.enterprise_id =" + Character.Instance.Enterprise.Id + ";";
        MySqlDataReader data = command.ExecuteReader();

        List<Product> products = new List<Product>();
        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            String title = (String)data["title"];
            Int64 project_id = Convert.ToInt64(data["project_id"]);
            Decimal price = Convert.ToDecimal(data["price"]);
            Double quality = Convert.ToDouble(data["quality"]);
            Decimal prime_cost = Convert.ToDecimal(data["prime_cost"]);

            Product product = new Product(project_id, title, price, quality, prime_cost);
            Debug.Log("Get product " + title);
            products.Add(product);
        }
        connection.Close();
        return products;
    }

    public static void InsertProducts(MySqlConnection connection)
    {

        foreach (Project project in Character.Instance.Enterprise.Projects)
        {
            Product product = project.Product;
            connection.Open();
            String Query = "INSERT INTO product(project_id, title,price,quality,prime_cost) values(" + product.Project_id + ",'" + product.Title + "'," +
                product.Price + "," + product.Quality + "," + product.Prime_cost + ");";

            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert product " + product.Title);
            connection.Close();
        }

    }

    public static void UpdateProducts(MySqlConnection connection)
    {

        foreach (Project project in Character.Instance.Enterprise.Projects)
        {
            Product product = project.Product;
            connection.Open();

            String Query = "UPDATE `product` SET title='" + product.Title + "', price=" + product.Price +
                    ", quality=" + product.Quality + ", prime_cost=" + product.Prime_cost + " where project_id=" + product.Project_id + ";";

            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update product " + product.Title);
            connection.Close();
        }

    }

    public static void DeleteProducts(MySqlConnection connection, List<Product> products)
    {

        foreach (Product product in products)
        {
            connection.Open();
            String Query = "DELETE FROM `product` WHERE project_id=" + product.Project_id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete product " + product.Title);
            connection.Close();
        }

    }

}