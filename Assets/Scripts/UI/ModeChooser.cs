using System;
using System.Collections.Generic;
using Core;
using Core.Helper.OdinCommunityTools;
using JetBrains.Annotations;
using Selection_Mode;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace UI
{
	/// <summary>
	/// Responsible for choosing a mode based on what we click from the inspector.
	/// I provide the Odin functionality, so the designer can easily choose what's the best for certain mode.
	///
	/// For example if we have clicked mode "plus", then the list of scriptable objects that have been configure
	/// by it's attack value or its range will be reflected on the list, and if we just click the list
	/// the currentAttackAndRangeValue will be updated, so it's very dynamic.
	/// </summary>
	public class ModeChooser : MonoBehaviour
	{
		[EnumToggleButtons] [Title("Selection Mode")] [SerializeField] [OnValueChanged("OnChangedSelectionByOdin")]
		[InfoBox("Click the button below, then the list of attack and range for the selected mode will be appeared")]
		private Selections selection;
		
		[ListItemSelector("SetAttackAndRangeValueByOdin")] [SerializeField] [Title("Attack And Range")]
		[InfoBox("Choose the attack and range pair value by click the row of the list, X denotes Attack Value, " +
		         "while Y denotes the Range Tiles")]
		private List<Vector2> attackAndRangeValueList = new();

		[SerializeField, ReadOnly] private Vector2 currentAttackAndRangeValue;
		[SerializeField] private TextMeshProUGUI damagePointText;

#if UNITY_EDITOR
		/// <summary>
		/// Need to be set dirty because under the hood the script from odin community tools has relation with editor
		/// GUI and the selection won't be saved if not set it to dirty
		/// </summary>
		/// <param name="index"></param>
		[UsedImplicitly]
		private void SetAttackAndRangeValueByOdin(int index)
		{
			currentAttackAndRangeValue = index >= 0 ? attackAndRangeValueList[index] : new Vector2();
			damagePointText.text = currentAttackAndRangeValue.x.ToString();
			EditorUtility.SetDirty(this);
		}
#endif

		[UsedImplicitly]
		private void OnChangedSelectionByOdin()
		{
			attackAndRangeValueList.Clear();
			foreach (var selectionMode in GameManager.Instance.SelectionModes)
			{
				if (selection == selectionMode.selection)
					attackAndRangeValueList.Add(new Vector2(selectionMode.attackValue, selectionMode.rangeTile));
			}
		}

		private void Start()
		{
			damagePointText.text = currentAttackAndRangeValue.x.ToString();
		}

		public void OnSelectedMode()
		{
			GameManager.Instance.ChangeSelection(selection, currentAttackAndRangeValue);
		}
	}
}