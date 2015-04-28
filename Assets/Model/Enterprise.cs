using UnityEngine;
using System.Collections;
using MySql.Data.MySqlClient;
using System;
using Model;
using System.Collections.Generic;
using AssemblyCSharp;
using System.Linq;
using Assets;

namespace Model
{

		[Serializable]
		public class Enterprise
		{
				public Int64 Id { get; set; }
				public String Title { get; set; }
				public Decimal Balance { get; set; }
				public Double Stationary { get; set; }
				public Int16? Type { get; set; }
				public Int64 Taxation_id { get; set; }

				public virtual Taxation Taxation { get; set; }

				public virtual List<Asset> Assets { get; set; }
				public virtual List<Service> Services { get; set; }
				public virtual List<Competitor> Competitors { get; set; }
				public virtual List<Employee> Employees { get; set; }
				public virtual List<Enterprise_docs> Enterprise_docs { get; set; }
				public virtual List<Enterprise_equipment> Enterprise_equipment { get; set; }
				public virtual List<Project> Projects { get; set; }

				//Standard collections to load all possible options and be standalone from DB
				public virtual List<Taxation> AllTaxations { get; set; }
				public virtual List<Role> AllRoles { get; set; }
				public virtual List<Equipment> AllEquipment { get; set; }
				public virtual List<Document> AllDocuments { get; set; }
				public virtual List<Company> AllCompanies { get; set; }

				public Enterprise (Int64 id, String title, Decimal balance, Double stationary,
                           Int16? type, Int64 taxation_id)
				{
						Id = id;
						Title = title;
						Balance = balance;
						Stationary = stationary;
						Type = type;
						Taxation_id = taxation_id;

						//initialise collections
						Assets = new List<Asset>();
						Services = new List<Service>();
						Competitors = new List<Competitor>();
						Employees = new List<Employee>();
						Enterprise_docs = new List<Enterprise_docs>();
						Enterprise_equipment = new List<Enterprise_equipment>();
						Projects = new List<Project>();
						/*AllRoles = RoleDAO.GetRoles(STSDataOperations.instance);
						AllTaxations = TaxationDAO.GetTaxations(STSDataOperations.instance);
						AllEquipment = EquipmentDAO.GetEquipment(STSDataOperations.instance);
						AllDocuments = DocumentDAO.GetDocuments(STSDataOperations.instance);
						AllCompanies = CompanyDAO.GetCompanies(STSDataOperations.instance);*/
				}


		public Enterprise(){
			//TabScript tb = new TabScript();	
		}

				//5 types of enterprise creation - 
				//private assets, investment, bank credit, private assets+investment, private assets+bank credit
				public void CreateEnterpriseWithPrivateAssets (Decimal personal)
				{
						this.Balance = personal;
						this.Assets.Add (new Asset (0, personal, DateTime.Now, this.Id));
						//push notification to handle it and display info in UI
						NotificationCenter.getI.postNotification("CreateEnterpriseWithPrivateAssets", personal);	
				}

				public void CreateEnterpriseWithInvestment (Decimal investment, Company investor)
				{	
						RecieveInvestment (investor);
						NotificationCenter.getI.postNotification("CreateEnterpriseWithInvestment", investment);
				}

				public void RecieveInvestment (Company investor)
				{
						this.Balance += investor.Investment;
						Assets.Add (new Asset (0, investor.Investment, DateTime.Now, this.Id));
						NotificationCenter.getI.postNotification("RecieveInvestment", investor);
				}

				public void CreateEnterpriseWithCredit (Decimal credit, Company bank)
				{
						RecieveCredit (bank);
						NotificationCenter.getI.postNotification("CreateEnterpriseWithCredit", credit);
				}

				public void RecieveCredit (Company bank)
				{
						this.Balance += bank.Investment;
						Assets.Add (new Asset (0, bank.Investment, DateTime.Now, this.Id));
						NotificationCenter.getI.postNotification ("RecieveCredit", bank);
				}

				public void CreateEnterpriseWithPrivateAssetsAndInvestment (Decimal personal, Decimal investment, Company investor)
				{
						CreateEnterpriseWithInvestment (investment, investor);
						CreateEnterpriseWithPrivateAssets (personal);
						NotificationCenter.getI.postNotification ("CreateEnterpriseWithPrivateAssetsAndInvestment", personal + investment);
				}

				public void CreateEnterpriseWithPrivateAssetsAndCredit (Decimal personal, Decimal credit, Company bank)
				{
						CreateEnterpriseWithCredit (credit, bank);
						CreateEnterpriseWithPrivateAssets (personal);
						NotificationCenter.getI.postNotification ("CreateEnterpriseWithPrivateAssetsAndCredit", personal + credit);
				}

				public Decimal TotalIncomeForPeriod (Int32 days)
				{
						//calculation of annual income
						//Доход = выручка от продаж за выбранный период  - себестоимость продукции, услуг за выбранный период (в стоимостном выражении)
						//не путать с выручкой! 
						//доходы/расходы (себестоиость, выручка, аренда, амортизация, вообще все)- в Asset, выручка за реализацию продукции в Revenue
						Decimal revenue = 0;
						foreach (Asset asset in this.Assets) {
								if (asset.Asset_date <= DateTime.Today.AddDays (days)) {
										revenue += asset.Value;
								}
						}
						return revenue;
				}

				//setting taxation type for the enterprise
				//if return false - this type can't be applied
				public Boolean SetTaxationType (Int32 type)
				{
						Decimal revenue = TotalIncomeForPeriod (365);
						Boolean b = true;

						switch (type) {
						case 1:
								{
										//type of production of the enterprise doesn't match ones stated in taxation type
										b = false;
										break;
								}
						case 2:
								{
										//if selected wrong taxation for a legal/private body
										if (this.Type == 1)
												b = false;
										//if enterprise exceeds number of its employees
										if (this.Employees.Count > 10)
												b = false;
										//if enterprise revenue exceeds sated in taxation type
										if (revenue > 1000000)
												b = false;

										break;
								}
						case 3:
								{
										//type of production of the enterprise doesn't match ones stated in taxation type
										return false;
								}
						case 4:
								{
										if (this.Type == 0)
												b = false;
										if (this.Employees.Count > 50)
												b = false;
										if (revenue > 5000000)
												b = false;
										break;
								}
						case 5:
								{
										//type of production of the enterprise doesn't match ones stated in taxation type
										b = false;
										break;
								}
						case 6:
								{
										if (this.Type == 1)
												b = false;
										if (this.Employees.Count > 50)
												b = false;
										if (revenue > 20000000)
												b = false;
										break;
								}
						}
						if (!b)
								NotificationCenter.getI.postNotification ("SetTaxationType", 0);
						this.Type = (Int16?)type;
						NotificationCenter.getI.postNotification ("SetTaxationType", Type);
						return true;
				}


				public String CompleteDocuments (MySqlConnection connection)
				{
						//if legal body - complete form #1, if private - form #10
						String form = String.Empty;
						String document = String.Empty;

						if (Type == 1) {
								form = System.IO.File.ReadAllText (@"Assets\Documents\registration_form_1.txt");
								String ss = @"\{\d+\}";
								char[] a = ss.ToCharArray ();
								String[] s = form.Split (a);
								document = s [0] + this.Title +
										s [1] + "\"ООО\"" +
										s [2] + "просп. Ленина, 9А, Харків, Україна" +
										s [3] + this.Balance +
										s [4] + DateTime.Now.ToString ("dd.MM.yyyy") +
										s [5] + "Иванов Иван Иванович" +
										s [6] + DateTime.Now.ToString ("dd.MM.yyyy");
						} else {
								form = System.IO.File.ReadAllText (@"Assets\Documents\registration_form_10.txt");
								String ss = @"\{\d+\}";
								char[] a = ss.ToCharArray ();
								String[] s = form.Split (a);
								document =
                    s [0] + "Иванов Иван Иванович" +
										s [1] + "11111111" +
										s [2] + "MT 111111" +
										s [3] + "Україна" +
										s [4] + "Иванов Иван Иванович" +
										s [5] + "просп. Ленина, 9А, Харків, Україна" +
										s [6] + "72.22.0" +
										s [7] + "Інші види діяльності у сфері розроблення програмного забезпечення" +
										s [8] + "Иванов Иван Иванович" +
										s [9] + DateTime.Now.ToString ("dd.MM.yyyy");
						}
						Debug.Log ("Enterprise.CompleteDocuments(): " + document);
						NotificationCenter.getI.postNotification ("CompleteDocuments", document);
						return document;
				}

				public void PaySalary (Employee employee, Int32 hours_worked, DateTime date)
				{
						Decimal salary = (Decimal)(hours_worked * employee.Qualification);

						Salary_payment salary_payment = new Salary_payment (0, employee.Id, DateTime.Now, hours_worked, 
			                                                    salary);
						employee.Salary_payments.Add (salary_payment);
						String Query;

						Project project = Character.Instance.Enterprise.Projects
                .OrderByDescending (value => value.Id)
                .FirstOrDefault (value => value.Employees.Contains (employee));

						project.Expenditures += salary;

						this.Balance -= project.Expenditures;
						PayUST (project.Expenditures);

						Assets.Add (new Asset (0, salary, DateTime.Now, this.Id));
						NotificationCenter.getI.postNotification ("PaySalary", project.Expenditures);
						PayUST (salary);
				}

				public void PayUST (Decimal amountOfSalaryPaidToday)
				{
						//Balance -= ∑(Salary_payment. Salary)*0,036-зарплата_предпринимателю*0,347(???)
						Decimal ust = 0;
						if (Type == 0) {
								//if personal body
								ust = amountOfSalaryPaidToday * (Decimal)0.036;

						} else {
								ust = amountOfSalaryPaidToday * (Decimal)0.376;
						}

						Balance -= ust;
						Assets.Add (new Asset (0, ust, DateTime.Now, Id));
						NotificationCenter.getI.postNotification("PayUST", ust);
				}

				public void LoanDisbursement ()
				{
						//budget -= (investment*share)/period
						foreach (Service s in this.Services) {
								//if a loan hasn't been disbursed yet
								if (s.Period < s.PeriodsPaid) {
										s.PeriodsPaid++;
										Decimal loan = (s.Company.Investment * (Decimal)s.Company.Share) / (Decimal)s.Period;

										Balance -= loan;
										NotificationCenter.getI.postNotification ("LoanDisbursement", loan);
										Assets.Add (new Asset (1, loan, DateTime.Now, this.Id));
								}
						}
				}

				public void SharePayout ()
				{
						//budget -= ∑ (revenue.Value за последний месяц)*share
						foreach (Service s in Services) {
								Decimal income = TotalIncomeForPeriod (DateTime.DaysInMonth (DateTime.Now.Year, DateTime.Now.Month));
								Decimal payout = income * (Decimal)s.Company.Share;

								Asset a1 = new Asset (1, payout, DateTime.Now, Id);
								Balance -= payout;
								Assets.Add (a1);
								NotificationCenter.getI.postNotification ("SharePayout", payout);
						}
				}

				//DO WE NEED IT?
				public Employee getEmployeeWithId (Int64 Id)
				{
						foreach (Employee e in this.Employees) {
								if (e.Id == Id)
										return e;
						}
						return null;
				}
		}
}