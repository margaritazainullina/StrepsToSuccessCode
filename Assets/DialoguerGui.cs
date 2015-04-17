using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class DialoguerGui : MonoBehaviour {
	private bool _showing;
	private string[] _choices;
	private string _text;

	// Use this for initialization
	void Start () {
		//Process.Start("D:/School simulation/Assets/game.exe");
		Dialoguer.events.onStarted += onStarted;
		Dialoguer.events.onEnded += onEnded;
		Dialoguer.events.onTextPhase += onTextPhase;
	}
	void OnGUI()
	{
		if (!_showing) 
			return;
				GUI.Box (new Rect (10, 10, 350, 150), _text);

			if (_choices==null){
						if (GUI.Button (new Rect(50, 220, 200, 30), "продолжить")) {
								Dialoguer.ContinueDialogue ();		
						}
				} else {
			for (int i=0;i<_choices.Length;i++)
			{
			if (GUI.Button(new Rect(50,220+(40*i),200,30),_choices[i])){
				Dialoguer.ContinueDialogue(i);
			}
			}
		}
		}
	// Update is called once per frame
	void Update () {
	
	}
	private void onStarted()
	{
		_showing = true;
	}
	private void onEnded()
	{
		_showing = false;
	}
	private void onTextPhase(DialoguerTextData data){

		_text = data.text;
		_choices = data.choices;
	}

}
