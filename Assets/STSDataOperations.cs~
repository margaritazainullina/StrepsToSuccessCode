using Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

namespace Assets
{
		public static class STSDataOperations
		{
				private static MySqlConnection instance;
		
				public static Boolean TryCreateInstance ()
				{
						Boolean success = true;
						String source = "Server=localhost;" +
								"Database=sts;" +
								"User ID=root;" +
								"Pooling=false;" +
								"Password=";
						try {
								if (instance == null || instance.State.ToString () != "Open") {
										instance = new MySqlConnection (source);
										instance.Open ();
								}
						} catch (Exception ex) {
								Debug.Log (ex.Message);
								success = false;
						} finally {
								instance.Close ();
						}
			
						return success;
				}
		
				public static void SaveGameToDB (Boolean autosave)
				{
						//Deleting DB records
						try {
								EnterpriseDAO.DeleteEnterprises (instance);
						} catch (Exception ex) {
								Debug.Log ("Deleting failed: " + ex.Message);
						} finally {
								if (instance != null && instance.State.ToString () == "Open") {
										instance.Close ();
								}
						}
			
						try {
								Int64 maxProjId = 0;
								Int64 maxEmpId = 0;
								EnterpriseDAO.InsertEnterprise (instance);
								AssetDAO.InsertAssets (instance);
								Enterprise_docsDAO.InsertEnterprise_docs (instance);
				
								if (Character.Instance.Enterprise.Projects != null && Character.Instance.Enterprise.Projects.Count > 0) {
										ProjectDAO.InsertProjects (instance);
										//To ensure that FK id equals inserted one
										maxProjId = ProjectDAO.GetLastProjectId (instance);
										maxProjId -= Character.Instance.Enterprise.Projects.Max (value => value.Id);
										Character.Instance.Enterprise.Projects.ForEach (value => value.Product.Project_id += maxProjId);
										ProductDAO.InsertProducts (instance);
										Character.Instance.Enterprise.Projects.ForEach (value => value.Project_stage.Project_id += maxProjId);
										Project_stageDAO.InsertProject_stages (instance);
								}
				
								ServiceDAO.InsertServices (instance);
								Enterprise_equipmentDAO.InsertEnterprise_equipment (instance);
				
								if (Character.Instance.Enterprise.Employees != null && Character.Instance.Enterprise.Employees.Count > 0) {
										EmployeeDAO.InsertEmployees (instance);
										maxEmpId = EmployeeDAO.GetLastEmployeeId (instance);
										maxEmpId -= Character.Instance.Enterprise.Employees.Max (value => value.Id);
										Character.Instance.Enterprise.Employees.ForEach (value => value.Salary_payments.ForEach (payment => payment.Employee_Id += maxEmpId));
										Salary_paymentDAO.InsertSalary_payments (instance);
								}
				
								List<Team_member> teamMembers = new List<Team_member> ();
								foreach (Project proj in Character.Instance.Enterprise.Projects) {
										foreach (Employee emp in proj.Employees) {
												teamMembers.Add (new Team_member (emp.Id + maxEmpId, proj.Id + maxProjId));
										}
								}
				
								Team_memberDAO.InsertTeam_members (instance, teamMembers);
						} catch (Exception ex) {
								Debug.Log ("Inserting failed: " + ex.Message);
						} finally {
								if (instance != null && instance.State.ToString () == "Open") {
										instance.Close ();
								}
						}
			
						try {
								FileStream fs;
								if (!Directory.Exists (Directory.GetCurrentDirectory () + "\\savegame"))
										Directory.CreateDirectory (Directory.GetCurrentDirectory () + "\\savegame");
				
								//Moving savefiles to get space for savefile to be saved
								for (Int32 i = 4; i > 0; i++) {
										if (autosave) {
												if (File.Exists (Directory.GetCurrentDirectory () + "\\savegame\\autosave" + i + ".sts")) {
														File.Move (Directory.GetCurrentDirectory () + "\\savegame\\autosave" + i + ".sts", Directory.GetCurrentDirectory () + "\\savegame\\autosave" + (i + 1) + ".sts");
												}
										} else {
												if (File.Exists (Directory.GetCurrentDirectory () + "\\savegame\\savegame" + i + ".sts")) {
														File.Move (Directory.GetCurrentDirectory () + "\\savegame\\savegame" + i + ".sts", Directory.GetCurrentDirectory () + "\\savegame\\savegame" + (i + 1) + ".sts");
												}
										}
								}
				
								if (autosave) {
										fs = new FileStream (Directory.GetCurrentDirectory () + "\\savegame\\autosave" + 1 + ".sts", FileMode.Create);
								} else {
										fs = new FileStream (Directory.GetCurrentDirectory () + "\\savegame\\savegame" + 1 + ".sts", FileMode.Create);
								}
								SaveDataToFile (fs);
						} catch (Exception ex) {
								Debug.Log ("Saving to file failed: " + ex.Message);
						}
				}
		
				public static bool LoadGameFromDB ()
				{
						/*****************LOADING THE GAME***********************/
						if (!TryCreateInstance ())
								return false;
			
						Character.Instance = null;
						List<Product> product;
						List<Project_stage> projectStages;
			
						try {
								CharacterDAO.LoadCharacterByName (instance, "Rita");
				
								Character.Instance.Enterprise = EnterpriseDAO.LoadEnterprise (instance);
				
								Character.Instance.Enterprise.Employees = EmployeeDAO.LoadEmployees (instance);
								Character.Instance.Enterprise.Competitors = CompetitorDAO.LoadCompetitors (instance);
								Character.Instance.Enterprise.Enterprise_docs = Enterprise_docsDAO.LoadEnterprise_docs (instance);
								Character.Instance.Enterprise.Assets = AssetDAO.LoadAssets (instance);
								Character.Instance.Enterprise.Enterprise_equipment = Enterprise_equipmentDAO.LoadEnterprise_equipment (instance);
								Character.Instance.Enterprise.Projects = ProjectDAO.LoadProjects (instance);
								Character.Instance.Enterprise.Services = ServiceDAO.LoadServices (instance);
				
				
								Character.Instance.Enterprise.AllTaxations = TaxationDAO.GetTaxations (instance);
								Character.Instance.Enterprise.AllRoles = RoleDAO.GetRoles (instance);
								Character.Instance.Enterprise.AllEquipment = EquipmentDAO.GetEquipment (instance);
								Character.Instance.Enterprise.AllDocuments = DocumentDAO.GetDocuments (instance);
								Character.Instance.Enterprise.AllCompanies = CompanyDAO.GetCompanies (instance);
				
								List<Team_member> teamMembers = Team_memberDAO.LoadTeam_members (instance);
				
								foreach (Project proj in Character.Instance.Enterprise.Projects) {
										proj.Employees = new List<Employee> ();
										foreach (Employee emp in Character.Instance.Enterprise.Employees) {
												if (teamMembers.Where (value => value.Project_id == proj.Id).Any (value => value.Employee_id == emp.Id)) {
														proj.Employees.Add (emp);
												}
										}
								}
				
								foreach (Employee item in Character.Instance.Enterprise.Employees) {
										item.Salary_payments = Salary_paymentDAO.LoadSalary_paymentsByEmployee (instance, item.Id);
								}
				
								product = ProductDAO.LoadProducts (instance);
								projectStages = Project_stageDAO.LoadProject_stages (instance);
						} catch (Exception ex) {
								Debug.Log ("Loading failed: " + ex.Message);
								return false;
						} finally {
								if (instance != null && instance.State.ToString () == "Open") {
										instance.Close ();
								}
						}
			
						Character.Instance.Enterprise.Services
				.ForEach (item => item.Company = Character.Instance.Enterprise.AllCompanies.FirstOrDefault (value => value.Id == item.Company_id));
			
						Character.Instance.Enterprise.Enterprise_equipment
				.ForEach (item => item.Equipment = Character.Instance.Enterprise.AllEquipment.FirstOrDefault (value => value.Id == item.Equipment_id));
			
						Character.Instance.Enterprise.Enterprise_docs
				.ForEach (item => item.Document = Character.Instance.Enterprise.AllDocuments.FirstOrDefault (value => value.Id == item.Document_id));
			
						Character.Instance.Enterprise.Employees
				.ForEach (item => item.Role = Character.Instance.Enterprise.AllRoles.FirstOrDefault (value => value.Id == item.Role_id));
			
						Character.Instance.Enterprise.Taxation
				= Character.Instance.Enterprise.AllTaxations.FirstOrDefault (value => value.Id == Character.Instance.Enterprise.Taxation_id);
			
						Character.Instance.Enterprise.Projects
				.ForEach (item => item.Product = product.FirstOrDefault (value => value.Project_id == item.Id));
			
						Character.Instance.Enterprise.Projects
				.ForEach (item => item.Project_stage = projectStages.FirstOrDefault (value => value.Project_id == item.Id));
			
						return true;
				}
		
				public static Boolean SaveDataToFile (FileStream fs)
				{
						IFormatter bform = new BinaryFormatter ();
						try {
								bform.Serialize (fs, Character.Instance);
								Debug.Log ("Successful serialization");
						} catch (Exception ex) {
								Debug.Log ("Serializing error: " + ex.Message);
								return false;
						}
						return true;
				}
		
				public static Boolean LoadDataFromFile (FileStream fs)
				{
						IFormatter bform = new BinaryFormatter ();
						Character.Instance = null;
						try {
								Character.Instance = (Character)bform.Deserialize (fs);
								Debug.Log ("Successful deserialization");
						} catch (Exception ex) {
								return false;
								Debug.Log ("Deserializing error: " + ex.Message);
						}
						return true;
				}
		
				public static void StartNewGame (String characterName, String characterGender, Int32 level, MySqlConnection connection)
				{
						//Create character here
						Character.Instance.Title = characterName;
						Character.Instance.Gender = characterGender;
						Character.Instance.Level = level;
			
						CharacterDAO.InsertCharacter (connection);
				}
		
		}
}