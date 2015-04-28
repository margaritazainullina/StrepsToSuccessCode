
using UnityEngine;
using UnityEditor;
using System.Collections;
using plyCommon;
using plyCommonEditor;
using DiaQ;
using DiaQEditor;

public class DiaQSampleRewardGiverInfo : plyDataProviderInfo
{
	/// <summary> Unique name to identify the provider by </summary>
	public override string ProviderName()
	{
		return "DiaQ Sample Reward Giver";
	}

	/// <summary> Context is important to identify where this provider can be used.
	/// The default is "data", meaning it can provider data and is able to set 
	/// its data. That is, it implements DataProvider_GetValue and DataProvider_SetValue.
	/// Where a system, using DataProviders, are more specialised it will indicate what
	/// context it expects if it do not function purely on get/set of data. </summary>
	public override string ProviderContext()
	{
		// in the case of DiaQ the provider must use the context "DiaQReward"
		return "DiaQReward";
	}

	/// <summary> Return a nice name that identifies the data. Shown in the button
	/// used to open the data provider editor window for setup. </summary>
	public override string PrettyName(plyDataObject data, string emptyText)
	{
		if (data.nfo[0] == "0") return "XP";

		// in this case we use nfo[1] as a cache of the name of the picked item
		if (data.nfo[0] == "1") return "Item: " + data.nfo[1];
		return emptyText;
	}

	/// <summary> Init the target type with this when the provider is selected </summary>
	public override plyDataObject.TargetObjectType DefaultTargetType()
	{
		// will find object in scene by its name
		return plyDataObject.TargetObjectType.Name;
	}

	/// <summary> Init the target type data this when the provider is selected </summary>
	public override string DefaultTargetTypeData()
	{
		// name of the object in the scene
		return "DiaQSampleRewardGiver";
	}

	/// <summary> Init the component name with this when the provider is selected.
	/// It is the component that implements plyDataProviderInterface </summary>
	public override string DefaultComponent()
	{
		// name of the component
		return "DiaQSampleRewardGiver";
	}

	/// <summary> Init the the nfo[] field with this when the provider is selected </summary>
	public override string[] InitNfo()
	{
		// init nfo[] with one string to carry the needed info
		// nfo[0] = 0:XP, 1:Item
		// nfo[1] = name of picked item

		// In this basic sample I only care about the name of the picked item but you would
		// probably want to save its id. Have a look at the plyRPG (included plugin package)
		// script, plyRPGDiaQRewardInfo.cs to see a more advanced example of how this class
		// could work.

		return new string[] { "0", "" };
	}

	/// <summary> Return true if user may choose different settings for type and type data in editor </summary>
	public override bool CanChangeType()
	{
		return false;
	}

	// ============================================================================================================

	private static string[] Options = { "XP", "Item", };
	private static string[] ItemNames = { "Sword", "Potion", "Scrap Metal" };
	private int selectedOption = 0;
	private int selectedItem = -1;

	/// <summary> Called when the data provider is selected </summary>
	public override void NfoFieldFocus(plyDataObject data, EditorWindow ed)
	{
		selectedOption = 0;
		int.TryParse(data.nfo[0], out selectedOption);

		selectedItem = -1;
		if (selectedOption == 1)
		{
			for (int i = 0; i < ItemNames.Length; i++)
			{
				if (ItemNames[i].Equals(data.nfo[1]))
				{
					selectedItem = i;
					break;
				}
			}

			if (selectedItem < 0)
			{
				// no item selected. set a default.
				selectedItem = 0;
				data.nfo[1] = ItemNames[0];
				GUI.changed = true; // so changed gets saved
			}
		}
	}

	/// <summary> Called when the nfo[] edit fields should be rendered </summary>
	public override void NfoField(plyDataObject data, EditorWindow ed)
	{
		// Let designer pick a reward type
		EditorGUI.BeginChangeCheck();
		selectedOption = EditorGUILayout.Popup("Reward Type", selectedOption, Options);
		if (EditorGUI.EndChangeCheck())
		{
			data.nfo[0] = selectedOption.ToString();
			data.nfo[1] = "";

			if (selectedOption == 1)
			{
				// set a default item to be selected
				selectedItem = 0;
				data.nfo[1] = ItemNames[0];
			}
		}

		// Let designer pick an item as reward
		if (selectedOption == 1)
		{
			EditorGUI.BeginChangeCheck();
			selectedItem = EditorGUILayout.Popup(" ", selectedItem, ItemNames);
			if (EditorGUI.EndChangeCheck()) data.nfo[1] = ItemNames[selectedItem];
		}
	}

	// ============================================================================================================
}
