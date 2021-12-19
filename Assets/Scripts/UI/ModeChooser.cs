using System;
using System.Collections.Generic;
using Core;
using Core.Helper.OdinCommunityTools;
using Selection_Mode;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace UI
{
	public class ModeChooser : MonoBehaviour
	{
		[EnumToggleButtons] [Title("Selection Mode")] [ShowInInspector] [SerializeField] [OnValueChanged("OnChangedSelectionByOdin")]
		private Selections selection;

		[ListItemSelector("SetAttackAndRangeValueByOdin")] [SerializeField] [Title("Attack And Range")]
		private List<Vector2> attackAndRangeValueList = new();

		[SerializeField, ReadOnly] private Vector2 currentAttackAndRangeValue;

		[SerializeField] private TextMeshProUGUI damagePointText;
		
#if UNITY_EDITOR
		private void SetAttackAndRangeValueByOdin(int index)
		{
			currentAttackAndRangeValue = index >= 0 ? attackAndRangeValueList[index] : new Vector2();
			damagePointText.text = currentAttackAndRangeValue.x.ToString();
			EditorUtility.SetDirty(this);
		}	
#endif
		
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