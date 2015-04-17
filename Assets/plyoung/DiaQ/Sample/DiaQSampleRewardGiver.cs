
using UnityEngine;
using System.Collections;
using plyCommon;

public class DiaQSampleRewardGiver : MonoBehaviour, plyDataProviderInterface
{

	public void DataProvider_Callback(string[] nfo)
	{
		// this is what gets called when DiaQuest.RunRewardGivers()
		// is called and this provider is chosen for handling
		// a reward entry in the quest. Note that RunRewardGivers()
		// will append the reward value to the nfo array so the 
		// last entry in this array will be the value as entered
		// in the quest editor. The other nfo entries are as you
		// set them up in the Info (editor) class. DiaQSampleRewardGiverInfo

		// I know that I defined the following about nfo
		// nfo[0] = 0:XP, 1:Item
		// nfo[1] = name of picked item
		//
		// and that this was appended by RunRewardGivers()
		// nfo[2] = the value

		if (nfo[0] == "0") 
		{
			// give player XP
			Debug.Log("Player received: " + nfo[2] + " XP");
		}
		else if (nfo[0] == "1")
		{	
			// give player a number of copies of the item
			Debug.Log("Player received: " + nfo[2] + "x " + nfo[1]);
		}
	}

	public object DataProvider_GetValue(string[] nfo)
	{
		// not used in this context but needed by plyDataProviderInterface
		return null;
	}

	public void DataProvider_SetValue(string[] nfo, object value)
	{
		// not used in this context but needed by plyDataProviderInterface
	}

}
