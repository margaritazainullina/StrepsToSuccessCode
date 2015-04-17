using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using Model;

public class TabScript : MonoBehaviour 
{
	GameObject tab;
	GameObject buttonSlideIn;
	GameObject buttonSlideOut;
	bool isTabShowed = false;

	private Sprite tab1 = null;
	private Sprite tab2 = null;
	private Sprite tab3 = null;
	private Sprite tab4 = null;
	private Sprite tab5 = null;
	private Sprite tab6 = null;

	GameTimer gt;

	//singletone Enterprise
	//TODO: replace, load from storage
//	Enterprise enterprise = Character.Instance.Enterprise;	
	Enterprise enterprise = new Enterprise(0, "ООО \"Сельдерей\"", 0, 0, 0, 3);

	
	// Use this for initialization
	void Start ()
	{
		//NotificationCenter instance for handling events
		//link event to it handler

		//for class Enterprise
		NotificationCenter.getI.addCallback("CreateEnterpriseWithPrivateAssets", this.CreateEnterpriseWithPrivateAssetsDisplay);
		NotificationCenter.getI.addCallback("CreateEnterpriseWithInvestment", this.CreateEnterpriseWithInvestmentDisplay);
		NotificationCenter.getI.addCallback("RecieveInvestment", this.RecieveInvestmentDisplay);
		NotificationCenter.getI.addCallback("CreateEnterpriseWithCredit", this.CreateEnterpriseWithCreditDisplay);
		NotificationCenter.getI.addCallback("RecieveCredit", this.RecieveCreditDisplay);
		NotificationCenter.getI.addCallback("CreateEnterpriseWithPrivateAssetsAndInvestment", this.CreateEnterpriseWithPrivateAssetsAndInvestmentDisplay);
		NotificationCenter.getI.addCallback("CreateEnterpriseWithPrivateAssetsAndCredit", this.CreateEnterpriseWithPrivateAssetsAndCreditDisplay);
		NotificationCenter.getI.addCallback("SetTaxationType", this.SetTaxationTypeDisplay);
		NotificationCenter.getI.addCallback("CompleteDocuments", this.CompleteDocumentsDisplay);
		NotificationCenter.getI.addCallback("PaySalary", this.PaySalaryDisplay);
		NotificationCenter.getI.addCallback("PayUST", this.PayUSTDisplay);
		NotificationCenter.getI.addCallback("LoanDisbursement", this.LoanDisbursementDisplay);
		NotificationCenter.getI.addCallback("LoanDisbursement", this.SharePayoutDisplay);


		//start timer
		//todo: remove it to init script
		GameTimer gt = new GameTimer (1);

		tab = GameObject.Find("Image");
		buttonSlideIn = GameObject.Find("ButtonSlideIn");
		buttonSlideOut = GameObject.Find("ButtonSlideOut");

		tab1 = Resources.Load<Sprite>("_01");
		tab2 = Resources.Load<Sprite>("_03");
		tab3 = Resources.Load<Sprite>("_05");
		tab4 = Resources.Load<Sprite>("_15");
		tab5 = Resources.Load<Sprite>("_11");
		tab6 = Resources.Load<Sprite>("_13");	

		//TODO: remove,just for test
		//enterprise = new Enterprise (0, "", 100, 100, 1, 1);
		enterprise.CreateEnterpriseWithPrivateAssets(10000);
	}

	//methods for handling events
	//name of method - name of event method in observed class+Display
	private void CreateEnterpriseWithPrivateAssetsDisplay(object sender)
	{
		//TODO: remove logging with writing to ui
		Decimal amount = (Decimal)sender;
		Debug.Log ("Создано предприятие "+enterprise.Title+"!");
		Debug.Log ("На баланс предприятия внесено: "+amount+" грн.");
	}
	private void CreateEnterpriseWithInvestmentDisplay(object sender)
	{
		Decimal amount = (Decimal)sender;
		Debug.Log ("Создано предприятие "+enterprise.Title+"!");
		Debug.Log ("На баланс предприятия внесено: "+amount+" грн.");
	}
	private void RecieveInvestmentDisplay(object sender)
	{
		Company investor = (Company)sender;
		Debug.Log ("Получена инвестиция от "+investor.Title+" в размере "+investor.Investment+" грн.");
	}
	private void CreateEnterpriseWithCreditDisplay(object sender)
	{
		Decimal amount = (Decimal)sender;
		Debug.Log ("Создано предприятие "+enterprise.Title+"!");
		Debug.Log ("На баланс предприятия внесено: "+amount+" грн.");
	}
	private void RecieveCreditDisplay(object sender)
	{
		Company investor = (Company)sender;
		Debug.Log ("Получен кредит от "+investor.Title+" в размере "+investor.Investment+" грн. на срок "+investor.Period+" мес." );
	}
	private void CreateEnterpriseWithPrivateAssetsAndInvestmentDisplay(object sender)
	{
		Decimal amount = (Decimal)sender;
		Debug.Log ("Создано предприятие "+enterprise.Title+"!");
		Debug.Log ("На баланс предприятия внесено: "+amount+" грн.");
	}
	private void CreateEnterpriseWithPrivateAssetsAndCreditDisplay(object sender)
	{
		Decimal amount = (Decimal)sender;
		Debug.Log ("Создано предприятие "+enterprise.Title+"!");
		Debug.Log ("На баланс предприятия внесено: "+amount+" грн.");
	}
	private void SetTaxationTypeDisplay(object sender)
	{
		Taxation t = (Taxation)sender;
		if(t!=0)
		Debug.Log ("На предприятии выбрана "+enterprise.Title+" группа налогообложения");
		else Debug.Log ("Вы не можете выбрать этот тип налогообложения для вашего предприятия! ");
	}
	private void CompleteDocumentsDisplay(object sender)
	{
		Document t = (Document)sender;
			Debug.Log ("Оформлен документ "+t.Title);
	}
	private void PaySalaryDisplay(object sender)
	{
		Decimal amount = (Decimal)sender;
		Debug.Log ("Выплачена зарплата работникам предприятия в размере"+amount+" грн.");
	}
	private void PayUSTDisplay(object sender)
	{
		Decimal amount = (Decimal)sender;
		Debug.Log ("Выплачен ЕСВ работникам предприятия в размере"+amount+" грн.");
	}
	private void LoanDisbursementDisplay(object sender)
	{
		Decimal amount = (Decimal)sender;
		Debug.Log ("Выплачено по кредитам: "+amount+" грн.");
	}
	private void SharePayoutDisplay(object sender)
	{
		Decimal amount = (Decimal)sender;
		Debug.Log ("Выплачено инвесторам: "+amount+" грн.");
	}


	// Update is called once per frame
	void Update ()
	{
		
	}

	public void Slide ()
	{
		if (isTabShowed) {
			iTween.MoveFrom (tab, iTween.Hash ("x", 240f, "easetype", iTween.EaseType.easeInExpo));
			iTween.MoveTo(tab, iTween.Hash("x", -220f, "easetype", iTween.EaseType.easeInExpo));
			isTabShowed = false;


			Image[] i = buttonSlideIn.GetComponentsInChildren<Image>();	
			Color c = i[0].color;
			c.a=1;
			i[0].color=c;
			
			i = buttonSlideOut.GetComponentsInChildren<Image>();	
			c = i[0].color;
			c.a=0;
			i[0].color=c;


		} 
		else {
			iTween.MoveFrom (tab, iTween.Hash ("x", -230f, "easetype", iTween.EaseType.easeInExpo));
			iTween.MoveTo(tab, iTween.Hash("x", 220f, "easetype", iTween.EaseType.easeInExpo));
			//iTween.FadeTo( button , iTween.Hash( "alpha" , 0.0f , "time" , .3 , "easeType", "easeInSine") );

			isTabShowed = true;	

			Image[] i = buttonSlideIn.GetComponentsInChildren<Image>();	
			Color c = i[0].color;
			c.a=0;
			i[0].color=c;

			i = buttonSlideOut.GetComponentsInChildren<Image>();	
			c = i[0].color;
			c.a=0.85f;
			i[0].color=c;

			iTween.MoveFrom (buttonSlideOut, iTween.Hash ("x", 30, "easetype", iTween.EaseType.easeInExpo));
			iTween.MoveTo(buttonSlideOut, iTween.Hash("x", 475f, "easetype", iTween.EaseType.easeInExpo));

		}
	}

	public void TabMessages(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab1;
	}
	public void TabTasks(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab2;
	}
	public void TabEnterprise(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab3;
	}
	public void TabPersonnel(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab4;
	}
	public void TabProject(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab5;
	}
	public void TabStatistics(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab6;
	}
}