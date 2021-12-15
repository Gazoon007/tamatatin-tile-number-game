using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI textMP;

	private int number;

	private void Start()
	{
		textMP = GetComponent<TextMeshProUGUI>();
	}
	
	private void DecreaseValue(int value)
	{
		SetValue(number - value);
	}
	
	public void SetValue(int value)
	{
		number = value;
		textMP.text = value == 0 ? "" : value.ToString();
	}
}