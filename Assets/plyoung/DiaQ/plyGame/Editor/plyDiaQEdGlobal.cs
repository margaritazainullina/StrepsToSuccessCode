﻿// -= DiaQ =-
// www.plyoung.com
// Copyright (c) Leslie Young
// ====================================================================================================================

using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using plyCommonEditor;
using plyBloxKitEditor;
using plyGameEditor;

namespace DiaQEditor
{
	[InitializeOnLoad]
	public class plyDiaQEdGlobal
	{

		static plyDiaQEdGlobal()
		{
			EditorApplication.update += RunOnce;
		}

		private static void RunOnce()
		{
			EditorApplication.update -= RunOnce;
			
			/*EdGlobal.RegisterPlugin(
						new plyGamePluginInfo()
						{
							name = "DiaQ by PL Young",
							versionFile = "plyoung/DiaQ/Documentation/version.txt",
						}
					);

			plyBloxGUI.blockIcons.Add("diaq", plyEdGUI.LoadEditorTexture(plyEdUtil.PackagesRelativePathStart() + "DiaQ/plyGame/edRes/Icons/diaq.png"));
			*/

			RegisterDefaultToolButtons();
			EdGlobal.RegisterAutoCreate(DiaQEdGlobal.Prefab);
		}

		private static void RegisterDefaultToolButtons()
		{
			EdToolbar.AddToolbarButtons(new System.Collections.Generic.List<EdToolbar.ToolbarButton>()
			{
				new EdToolbar.ToolbarButton() { order = 200, callback = OpenDiaQEditor, gui = new GUIContent(plyEdGUI.LoadEditorTexture(plyEdUtil.PackagesRelativePathStart() + "DiaQ/plyGame/edRes/Toolbar/diaq" + (EditorGUIUtility.isProSkin ? "" : "_l") + ".png"), "DiaQ") },
			});
		}

		public static void OpenDiaQEditor()
		{
			DiaQEd.ShowDiaQEditorWindow();
		}

		// ============================================================================================================
	}
}