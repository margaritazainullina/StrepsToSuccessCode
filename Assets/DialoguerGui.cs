﻿using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine.UI;




public class DialoguerGui : MonoBehaviour
{
		private bool _showing = true;
		private string[] _choices;
		private string _text;
		private float currentDialogue = -1;

		private int slideId = 0;
		public GameObject tabletImage;
		public GameObject minimapCamera; 
		private Button[] tabsButtons = new Button[6];
		private Sprite[] sprites = new Sprite[6];

		// Use this for initialization
		protected virtual void Start ()
		{
				Dialoguer.events.onStarted += onStarted;
				Dialoguer.events.onEnded += onEnded;
				Dialoguer.events.onTextPhase += onTextPhase;
				currentDialogue = Dialoguer.GetGlobalFloat (1);
				if (currentDialogue == 0) {
						for (int i=0; i < tabsButtons.Length; i++) {
								tabsButtons [i] = tabletImage.GetComponentInChildren<Canvas> ().GetComponentsInChildren<Button> () [i];
						}
						sprites [0] = Resources.Load<Sprite> ("_01");
						sprites [1] = Resources.Load<Sprite> ("_03");
						sprites [2] = Resources.Load<Sprite> ("_05");
						sprites [3] = Resources.Load<Sprite> ("_15");
						sprites [4] = Resources.Load<Sprite> ("_11");
						sprites [5] = Resources.Load<Sprite> ("_13");	
				}
				_showing = true;
		}
	
		void OnGUI ()
		{
				if (!_showing)
						return;
		
				GUI.Box (new Rect (100, Screen.height - 150, Screen.width - 180, 60), _text);
		
				if (_choices == null) {
						if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height - 50, 150, 30), "Далее")) {
								Dialoguer.ContinueDialogue ();		

								UnityEngine.Debug.Log ("User clicked next dialogue slide");
						}
				} else {
						for (int i = 0; i < _choices.Length; i++) {
								if (GUI.Button (new Rect (50, 220 + (40 * i), 200, 30), _choices [i])) {
										Dialoguer.ContinueDialogue (i);
										UnityEngine.Debug.Log ("User has selected choice" + i);
								}
						}
				}
		}
		// Update is called once per frame
		void Update ()
		{
		
		}
	
		protected void onStarted ()
		{
				_showing = true;
				UnityEngine.Debug.Log ("Started dialog " + Dialoguer.GetGlobalFloat (1));
		}
	
		protected void onEnded ()
		{
				_showing = false;	
				UnityEngine.Debug.Log ("Ended dialog " + Dialoguer.GetGlobalFloat (1));
				currentDialogue = Dialoguer.GetGlobalFloat (1);
		}
	
		protected virtual void onTextPhase (DialoguerTextData data)
		{		
				if (slideId > 1 && currentDialogue == Dialoguer.GetGlobalFloat (1)) {
						//_showing = false;
				}
				_text = data.text;
				_choices = data.choices;
				UnityEngine.Debug.Log ("Text phase changed");
				if (currentDialogue == 0) {
						actionOnSlide ();
				}
				slideId++;
		}

		private void actionOnSlide ()
		{
				switch (slideId) {
				case 3:
						{
								UnityEngine.Debug.Log ("Show $ panel");
								break;
						}
				case 4:
						{
								minimapCamera.camera.orthographicSize = 10;
								break;
						}
				case 5:
						{
								minimapCamera.camera.orthographicSize = 22;
								iTween.MoveFrom (tabletImage, iTween.Hash ("x", -230f, "easetype", iTween.EaseType.easeInExpo));
								iTween.MoveTo (tabletImage, iTween.Hash ("x", 220f, "easetype", iTween.EaseType.easeInExpo));
								break;
						}
				case 7:
						{
								Image i = tabletImage.GetComponentsInChildren<Image> () [0];
								i.sprite = sprites [1];
								break;
						}
				case 8:
						{
								Image i = tabletImage.GetComponentsInChildren<Image> () [0];
								i.sprite = sprites [2];
								break;
						}
				case 9:
						{
								Image i = tabletImage.GetComponentsInChildren<Image> () [0];
								i.sprite = sprites [3];
								break;
						}
				case 10:
						{
								Image i = tabletImage.GetComponentsInChildren<Image> () [0];
								i.sprite = sprites [4];
								break;
						}
				case 11:
						{
								Image i = tabletImage.GetComponentsInChildren<Image> () [0];
								i.sprite = sprites [5];
								break;
						}
				case 12:
						{
								iTween.MoveFrom (tabletImage, iTween.Hash ("x", 240f, "easetype", iTween.EaseType.easeInExpo));
								iTween.MoveTo (tabletImage, iTween.Hash ("x", -220f, "easetype", iTween.EaseType.easeInExpo));
								break;
						}

				}
		}
	
}
