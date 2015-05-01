using UnityEngine;
using System.Collections;
using System;
using System.Timers;

//observer
using Model;
using UnityEngine.UI;


public class GameTimer : MonoBehaviour {
	//real date and time for timer
	public static DateTime date;
	//game date and time
	public static DateTime gameDate;
	System.Timers.Timer Timer1;

	public static TabScript ts = new TabScript();


	//speed 0-pause
	//speed 1 - 1 game hour = 7 second
	//speed 2 - 1 game hour = 5 second
	//speed 3 - 1 game hour = 3 second
	private int speed;
	public int Speed
	{
		get {return speed;}
		set { 
			if(speed<0||speed>3) throw new ArgumentException();
			speed = value; 
			switch(speed){
			case 0: Timer1.Interval = 0; break;
			case 1: Timer1.Interval = 7000; break;
			case 2: Timer1.Interval = 5000; break;
			case 3: Timer1.Interval = 3000; break;
			}
			Debug.Log("Game speed: "+speed);
		}
	}

	//creates and starts timer with set speed
	public GameTimer(int speed) { 
		//TODO: fetch date and time from autosaving
		Timer1 = new  System.Timers.Timer();
		Timer1.Elapsed += new ElapsedEventHandler(OnTimedEvent);
		Speed = speed;
		Timer1.Enabled = true;
		Debug.Log("Timer started.");
		date = DateTime.Now;
		gameDate = DateTime.Now;


	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
  

    //TODO: Display time on gui element
	private static void OnTimedEvent(object source, ElapsedEventArgs e)
	{
		//increase gametime value by 1 hour
		gameDate=gameDate.AddHours (1);
		Debug.Log("Time: "+gameDate.ToShortDateString()+" "+gameDate.ToShortTimeString());
		//if(ts!=null)ts.showTime (gameDate.ToShortDateString () + " " + gameDate.ToShortTimeString ());
		NotificationCenter.getI.postNotification("OnTimedEvent", gameDate.ToShortDateString () + " " + gameDate.ToShortTimeString ());
	}


}
