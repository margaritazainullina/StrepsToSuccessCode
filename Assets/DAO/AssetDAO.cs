using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
using MySql.Data;

public class AssetDAO
{
    public static List<Asset> LoadAssets(MySqlConnection connection)
    {
        List<Asset> assets = new List<Asset>();
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM asset WHERE enterprise_id=" + Character.Instance.Enterprise.Id + ";";
        MySqlDataReader data = command.ExecuteReader();

        Asset asset = null;
        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            Int32 value = Convert.ToInt32(data["value"]);
            Int64 id = Convert.ToInt64(data["id"]);
            DateTime asset_date = Convert.ToDateTime(data["asset_date"]);
            Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);

            asset = new Asset(id, value, asset_date, enterprise_id);

            Debug.Log("Get asset id=" + id + " and value=" + value);
            assets.Add(asset);
        }

        connection.Close();

        return assets;
    }


    public static void InsertAssets(MySqlConnection connection)
    {

        foreach (Asset asset in Character.Instance.Enterprise.Assets)
        {
            connection.Open();

            String Query = "INSERT INTO asset(value,asset_date,enterprise_id) values(" + asset.Value + ",'" + Helper.ToMySQLDateTimeFormat(asset.Asset_date)
                    + "'," + asset.Enterprise_id + ");";

            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);

            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();

            Debug.Log("Insert asset id=" + asset.Id + " and value=" + asset.Value);
            connection.Close();
        }

    }


    public static void UpdateAssets(MySqlConnection connection)
    {

        foreach (Asset asset in Character.Instance.Enterprise.Assets)
        {
            connection.Open();
            String Query = "UPDATE `asset` SET value='" + asset.Value + "', asset_date='" + Helper.ToMySQLDateTimeFormat(asset.Asset_date) + "', enterprise_id=" +
                    asset.Enterprise_id + " where id=" + asset.Id + ";";

            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);

            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update asset id=" + asset.Id + " and value=" + asset.Value);
            connection.Close();
        }

    }

    public static void DeleteAssets(MySqlConnection connection, List<Asset> assets)
    {

        foreach (Asset asset in assets)
        {
            connection.Open();
            String Query = "DELETE FROM `asset` WHERE id=" + asset.Id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete asset id=" + asset.Id + " and value=" + asset.Value);
            connection.Close();
        }

    }
}