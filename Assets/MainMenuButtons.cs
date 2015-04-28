using UnityEngine;
using System.Collections;
using Assets;
using System.Collections.Generic;
using System.Linq;
using Model;
using System;
using System.IO;

public class MainMenuButtons : MonoBehaviour
{
	
		// Use this for initialization
		void Start ()
		{
		
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}
	
		public void NewGame ()
		{
				//WHERE SHOULD WE ASK FOR BAMEE AND GENDER - IN WHICH SCENE
				//STSDataOperations.StartNewGame();
				Application.LoadLevel ("mock_city");			
		}
	
		public void ExitGame ()
		{
				Application.Quit ();
		}
}
