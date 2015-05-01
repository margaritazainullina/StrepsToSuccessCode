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

	Text tab1Text;
	Text tab2Text;
	Text tab3Text;
	Text tab4Text;
	Text tab5Text;
	Text tab6Text;
	Text timeText;


	
	public delegate void InvokeDelegate(string input);

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
		NotificationCenter.getI.addCallback("OnTimedEvent", this.OnTimedEventDisplay);


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

		//get text elements for each tab
		tab1Text = tab.GetComponentsInChildren<Text>()[0];
		tab2Text = tab.GetComponentsInChildren<Text>()[1];
		tab3Text = tab.GetComponentsInChildren<Text>()[2];
		tab4Text = tab.GetComponentsInChildren<Text>()[3];
		tab5Text = tab.GetComponentsInChildren<Text>()[4];
		tab6Text = tab.GetComponentsInChildren<Text>()[5];
		//text with time
		timeText = tab.GetComponentsInChildren<Text>()[6];


		//TODO: remove,just for test
		//enterprise = new Enterprise (0, "", 100, 100, 1, 1);
		enterprise.CreateEnterpriseWithPrivateAssets(10000);
	}

	//methods for handling events
	//name of method - name of event method in observed class+Display
	private void CreateEnterpriseWithPrivateAssetsDisplay(object sender)
	{
		//TODO: remove logging with writing to ui for all!
		Decimal amount = (Decimal)sender;
		Debug.Log ("Создано предприятие "+enterprise.Title+"!");
		Debug.Log ("На баланс предприятия внесено: "+amount+" грн.");

		tab1Text.text+="Создано предприятие "+enterprise.Title+"!\n";
		tab1Text.text+="На баланс предприятия внесено: "+amount+" грн.\n";
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
		if(t.Taxation_group!=0)
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
	public void OnTimedEventDisplay(object sender){
	//	String time = "111";
	//	object[] obj = new object[1];
	//	obj [0] = time;
	//	timeText.Invoke("UpdateTimeGui", 1);
	}

	
/*	public void UpdateTimeGui(string input)
	{
		timeText.text = input;
	}*/

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
		tab1Text.enabled = true;
		tab2Text.enabled = false;
		tab3Text.enabled = false;
		tab4Text.enabled = false;
		tab5Text.enabled = false;
		tab6Text.enabled = false;
	}
	public void TabTasks(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab2;
		tab1Text.enabled = false;
		tab2Text.enabled = true;
		tab3Text.enabled = false;
		tab4Text.enabled = false;
		tab5Text.enabled = false;
		tab6Text.enabled = false;
		tab2Text.text="Tab2 is loaded!";
	}
	public void TabEnterprise(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab3;
		tab1Text.enabled = false;
		tab2Text.enabled = false;
		tab3Text.enabled = true;
		tab4Text.enabled = false;
		tab5Text.enabled = false;
		tab6Text.enabled = false;
		tab3Text.text="Tab3 is loaded!";
	}
	public void TabPersonnel(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab4;
		tab1Text.enabled = false;
		tab2Text.enabled = false;
		tab3Text.enabled = false;
		tab4Text.enabled = true;
		tab5Text.enabled = false;
		tab6Text.enabled = false;
		tab4Text.text="Tab4 is loaded!";
	}
	public void TabProject(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab5;
		tab1Text.enabled = false;
		tab2Text.enabled = false;
		tab3Text.enabled = false;
		tab4Text.enabled = false;
		tab5Text.enabled = true;
		tab6Text.enabled = false;
		tab5Text.text="Tab5 is loaded!";
	}
	public void TabStatistics(){
		Image i = tab.GetComponentsInChildren<Image>()[0];
		i.sprite = tab6;
		tab1Text.enabled = false;
		tab2Text.enabled = false;
		tab3Text.enabled = false;
		tab4Text.enabled = false;
		tab5Text.enabled = false;
		tab6Text.enabled = true;
		tab6Text.text="Tab6 is loaded!";
	}

}