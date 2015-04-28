
using UnityEngine;
using System.Collections;
using plyCommon;
using DiaQ;

public class DiaQSample : MonoBehaviour 
{
	// =================================================================================
	// This script is a simple example of using the DiaQ API.

	private plyGraph conversation = null;
	private DiaQuest quest = null;

	// =================================================================================

	protected void Start() 
	{
		// find the quest and cache a reference to it so I do not have to look it up 
		// each time I want to work with it. Will simply search by its name but there 
		// are methods too.
		quest = DiaQEngine.Instance.questManager.GetQuestByName("Sample Quest");

		// will also cache the graph. A graph is the conversation flow of the
		// quest giver's dialogue
		conversation = DiaQEngine.Instance.graphManager.GetGraphByName("Sample Graph");
	}

	protected void OnGUI()
	{
		GUILayout.BeginHorizontal();
		{
			GUILayout.Space(20);
			GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(200));
			{
				GUILayout.Space(5);
				LoadSave();
				GUILayout.Space(5);
			}
			GUILayout.EndVertical();
			GUILayout.Space(20);
			GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(200));
			{
				GUILayout.Space(5);
				QuestGiver();
				GUILayout.Space(5);
			}
			GUILayout.EndVertical();
			GUILayout.Space(20);
			GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(200));
			{
				GUILayout.Space(5);
				QuestStatus();
				GUILayout.Space(5);
			}
			GUILayout.EndVertical();
			GUILayout.Space(20);
			GUILayout.BeginVertical(GUI.skin.box, GUILayout.Width(200));
			{
				GUILayout.Space(5);
				MetaData();
				GUILayout.Space(5);
			}
			GUILayout.EndVertical();
		}
		GUILayout.EndHorizontal();
	}

	// =================================================================================
	// This shows how to Save and Load the DiaQ data

	private void LoadSave()
	{
		GUILayout.Label("Data is saved to/ restored from PlayerPrefs. To test, interact with the quest giver, then save. Restart the scene and press restore to see the state of the quest updated.");
		GUILayout.Space(5);
		if (GUILayout.Button("Save State"))
		{
			string data = DiaQEngine.Instance.GetSaveData();
			PlayerPrefs.SetString("DIAQ", data);
			PlayerPrefs.Save();
		}

		if (GUILayout.Button("Restore State"))
		{
			string data = PlayerPrefs.GetString("DIAQ", "");
			DiaQEngine.Instance.RestoreFromSaveData(data);
		}
	}

	// =================================================================================
	// This represents a conversation with a quest giver using one of the DiaQ graphs 
	// to run through the conversation

	private void QuestGiver()
	{
		// check if the quest giver's graph is active. if so then the player is talking to the quest giver
		// if the active graph is null then it means there is no active conversation/ dialogue (graph flow)
		if (DiaQEngine.Instance.graphManager.ActiveGraph() == null)
		{
			// show button to talk to the quest giver
			if (GUILayout.Button("Talk to Quest Giver"))
			{
				// start the dialogue graph so that it can flow from node to node
				// the player is now talking to the quest giver
				DiaQEngine.Instance.graphManager.BeginGraph(conversation);
			}

			// check if player has the quest and show the buttons to perform quest
			QuestTask();
		}

		else
		{
			// Currently in a conversation with the quest giver since the graph is active.
			// Check if there is a node waiting for a response from the player. In the 
			// example graph this can only come from a dialogue node so I make this 
			// assumption and will treat any waiting node as a Dialogue Node.

			DiaQNode_Dlg dlg = DiaQEngine.Instance.graphManager.NodeWaitingForData() as DiaQNode_Dlg;
			if (dlg != null)
			{
				// Show the Dialogue Text
				string s = dlg.dialogueText;
				
				// Check if there is a quest linked to this dialogue node. In that case I want 
				// to take the quest text and append it to the text that will be shown to the player
				DiaQuest q = dlg.LinkedQuest();
				if (q != null)
				{
					s += "\n\n" + q.text;
				}

				// now show it
				GUILayout.Label(s);
				GUILayout.Space(10);

				// Show the possible responses the player can choose from
				for (int i = 0; i < dlg.responses.Length; i++)
				{
					if (GUILayout.Button(dlg.responses[i]))
					{
						// send the waiting node the info it is waiting for
						// In this case the dialogue node simply wants an integer
						// value that tells it which response was chosen
						DiaQEngine.Instance.graphManager.SendDataToNode(i);
					}
				}
			}

		}
	}

	// =================================================================================
	// Shows the buttons the player must click to perform the quest tasks

	private void QuestTask()
	{
		GUILayout.Space(50);
		if (true == quest.accepted && false == quest.completed)
		{
			// player has to click Button A twice and Button B once
			if (GUILayout.Button("Button A"))
			{
				// tell the quest that the player clicked Button by updating the 
				// condition with the key "A" as entered in the quest editor
				quest.ConditionPerformed("A");
			}

			if (GUILayout.Button("Button B"))
			{
				// tell the quest that the player pressed button B
				quest.ConditionPerformed("B");
			}
		}
	}

	// =================================================================================
	// This shows the status of the quests(s)

	private void QuestStatus()
	{
		GUILayout.Label("QUEST INFO");
		GUILayout.Space(10);
		GUILayout.Label("Accepted: " + (quest.accepted ? "Yes" : "No"));
		GUILayout.Label("Completed: " + (quest.completed ? "Yes" : "No"));
		GUILayout.Label("Rewarded: " + (quest.rewarded ? "Yes" : "No"));
		GUILayout.Space(10);
		GUILayout.Label("Task progress");
		GUILayout.Label("  => Click button A: " + quest.conditions[0].performedTimes);
		GUILayout.Label("  => Click button B: " + quest.conditions[1].performedTimes);
	}

	// =================================================================================
	// This is called from the Graph to give the player his rewards. The ideal would be 
	// for you to create a specialised Reward Node that can be used. Have look at the
	// DiaQ docs to learn how new nodes can be created.

	public void GiveRewards(DiaQuest quest)
	{
		quest.rewarded = true;

		// access the reward keys to see what rewards to give the player
		// ideally you would create a Data Provider which can be selected
		// from in the quest editor. I use both methods in this sample to 
		// demonstrate. First I will call RunRewardGivers() to try and run
		// through them. IN the sample quest I did set up one that works
		// in this manner - see DiaQSampleRewardGiver. There is also a
		// reward that works off of the keyString and that I handle here.

		quest.RunRewardGivers();

		for (int i = 0; i < quest.rewards.Count; i++)
		{
			if (quest.rewards[i].keyString == "Gold")
			{	
				// this is the one I can manually handle. You can handle all 
				// your rewards in this manner but it is bug prone as the 
				// designer might enter the key incorrectly. The best would
				// be to try create a reward giver/ handler as demonstrated
				// in the DiaQSampleRewardGiver script. Also see the script
				// editor/DiaQSampleRewardGiverInfo as it is a supporting
				// script on the editor side of the sample reward giver

				Debug.Log("Player received: " + quest.rewards[i].value + " Gold");

			}

			// else ... check for other keys that can be handled
		}

	}

	// =================================================================================
	// Sample of how to access and change metaData

	private bool metaInited = false;
	private plyMetaData diaqVar;
	private plyMetaData condVar;

	private void MetaData()
	{
		// to make things simpler I will first grab and keep 
		// references to the various variables
		if (false == metaInited)
		{
			metaInited = true;

			// The DiaQ "Global" metaData (variables) is stored in the graph manager
			// The metaData must be defined (done via the DiaQ editors) 
			// else this returns null
			diaqVar = DiaQEngine.Instance.graphManager.GetMetaData("var1");

			// Let's also get a reference to metaData defined for the first
			// condition of the Sample Quest. Remember we already have a
			// reference to the sample quest. Done in Start()
			condVar = quest.conditions[0].GetMetaData("testVar");
		}

		GUILayout.Label("Example of accessing the metaData (variables) of DiaQ");

		// I know this is inited as string value and can therefore access
		// it directly like that
		GUILayout.Label("DiaQ var");
		diaqVar.stringValue = GUILayout.TextField(diaqVar.stringValue);

		// the condition variable was defined as an int, so again
		// I will simply access it directly.
		GUILayout.Label("Condition var");
		string s = GUILayout.TextField(condVar.intValue.ToString());
		if (!s.Equals(condVar.intValue.ToString())) int.TryParse(s, out condVar.intValue);

		// finally, you can add metaData at runtime
		// lets add them to the 2nd reward of sample quest
		if (GUILayout.Button("Add Reward metaData"))
		{
			// to create a name I'll simply check how many metaData entries are 
			// already present and use that as part of the name.
			string metaDataName = "var " + quest.rewards[0].runtimeMetaData.Count;

			// this will now add the new meta data since the name does not yet 
			// exist in the runtime metadata. Since I am passing a string here
			// as 'val' param this metaData will initialise as a string type.
			quest.rewards[0].SetMetaDataValue(metaDataName, "");
		}

		// show a way to edit the entries in the reward metaData. Note that I
		// use '.runtimeMetaData' and not '.metaData' as the latter is only
		// valid in the editor side and not at runtime. Note that 
		// runtimeMetaData is a Dictionary.
		GUILayout.Label("Reward metaData");
		foreach (plyMetaData md in quest.rewards[0].runtimeMetaData.Values)
		{
			GUILayout.BeginHorizontal();
			{
				GUILayout.Label(md.name);
				md.stringValue = GUILayout.TextField(md.stringValue);
			}
			GUILayout.EndHorizontal();
		}

	}

	// =================================================================================
}
