using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;

public class Enterprise_equipmentDAO
{

    public static List<Enterprise_equipment> LoadEnterprise_equipment(MySqlConnection connection)
    {
        connection.Open();
        //retrieve from db
        MySqlCommand command = connection.CreateCommand();
        command.CommandText = "SELECT * FROM enterprise_equipment WHERE enterprise_id=" + Character.Instance.Enterprise.Id + ";";
        MySqlDataReader data = command.ExecuteReader();

        List<Enterprise_equipment> enterprise_equipment = new List<Enterprise_equipment>();

        //read data from dataReader and form list of Character instances
        while (data.Read())
        {
            if (data.IsDBNull(0))
            {
                break;
            }
            DateTime purchase_date = Convert.ToDateTime(data["purchase_date"]);
            Int32? quantity = Helper.GetValueOrNull<Int32>(Convert.ToString(data["quantity"]));
            Int32? lease_term = Helper.GetValueOrNull<Int32>(Convert.ToString(data["lease_term"]));
            Boolean? isRunning = Helper.GetValueOrNull<Boolean>(Convert.ToString(data["isRunning"]));
            Int64 enterprise_id = Convert.ToInt64(data["enterprise_id"]);
            Int64 equipment_id = Convert.ToInt64(data["equipment_id"]);

            Enterprise_equipment enterprise_equip = new Enterprise_equipment(purchase_date, quantity, lease_term, isRunning,
                                                                             enterprise_id, equipment_id);

            Debug.Log("Get enterprise_equip Enterprise_id=" + enterprise_id + " and Equipment_id=" + equipment_id);
            enterprise_equipment.Add(enterprise_equip);
        }
        connection.Close();
        return enterprise_equipment;
    }

    public static void InsertEnterprise_equipment(MySqlConnection connection)
    {

        foreach (Enterprise_equipment enterprise_equip in Character.Instance.Enterprise.Enterprise_equipment)
        {
            connection.Open();
            String Query = "INSERT INTO `enterprise_equipment` values(" + enterprise_equip.Enterprise_id + "," + enterprise_equip.Equipment_id + ",'" +
                Helper.ToMySQLDateTimeFormat(enterprise_equip.Purchase_date) + "'," + enterprise_equip.Quantity + "," + enterprise_equip.Lease_term + "," + enterprise_equip.IsRunning + ");";

            Query = Helper.ReplaceInsertQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Insert enterprise_equip Enterprise_id=" + enterprise_equip.Enterprise_id + " and Equipment_id=" + enterprise_equip.Equipment_id);
            connection.Close();
        }

    }

    public static void UpdateEnterprise_equipment(MySqlConnection connection)
    {

        foreach (Enterprise_equipment enterprise_equip in Character.Instance.Enterprise.Enterprise_equipment)
        {
            connection.Open();
            String Query = "UPDATE `enterprise_equipment` SET purchase_date='" + Helper.ToMySQLDateTimeFormat(enterprise_equip.Purchase_date) + "', quantity=" + enterprise_equip.Quantity +
                ", lease_term=" + enterprise_equip.Lease_term + ", isRunning=" + enterprise_equip.IsRunning + " where enterprise_id=" + enterprise_equip.Enterprise_id + " AND equipment_id=" + enterprise_equip.Equipment_id + ";";
            Query = Helper.ReplaceUpdateQueryVoidWithNulls(Query);
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Update enterprise_equip Enterprise_id=" + enterprise_equip.Enterprise_id + " and Equipment_id=" + enterprise_equip.Equipment_id);
            connection.Close();
        }

    }

    public static void DeleteEnterprise_equipment(MySqlConnection connection, List<Enterprise_equipment> enterprise_equipment)
    {

        foreach (Enterprise_equipment enterprise_equip in enterprise_equipment)
        {
            connection.Open();
            String Query = "DELETE FROM `enterprise_equipment` WHERE enterprise_id=" + enterprise_equip.Enterprise_id + " AND equipment_id=" + enterprise_equip.Equipment_id + ";";
            MySqlCommand command = new MySqlCommand(Query, connection);

            command.ExecuteReader();
            Debug.Log("Delete asset enterprise_equip Enterprise_id=" + enterprise_equip.Enterprise_id + " and Equipment_id=" + enterprise_equip.Equipment_id);
            connection.Close();
        }

    }

}