using System;
using Core;
using Selection_Mode;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Tile
{
	public class TileUnit : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI textMP;

		private HighlightTile _highlightTile;
		private PlayerController _playerController;
		private bool _hasReachedZero;

		public int Number { get; private set; }
		public Action OnReachedZeroValue;
		public Action OnReachedZeroValueProp
		{
			get => OnReachedZeroValue;
			set => OnReachedZeroValue = value;
		}

		void OnDrawGizmos() 
		{
			Handles.Label(transform.position + Vector3.up, transform.position.x + ", " + transform.position.y );
		}
		
		private void OnEnable()
		{
			_playerController = FindObjectOfType<PlayerController>();
			_playerController.OnClickToTile += Hit;
			TileGenerator.Instance.OnGeneratorFinished += CheckTileFirst;
		}

		private void OnDisable()
		{
			_playerController.OnClickToTile -= Hit;
			TileGenerator.Instance.OnGeneratorFinished -= CheckTileFirst;
		}

		private void Start()
		{
			_highlightTile = GetComponent<HighlightTile>();
		}

		private void CheckTileFirst()
		{
			if (Number == 0)
				OnReachedZeroValue?.Invoke();
		}

		private void Hit()
		{
			if (_highlightTile.IsTileHighlighted)
				GameManager.Instance.SelectionMode.DecreaseValue(this);
		}

		public void SetValue(int value)
		{
			if (_hasReachedZero) return;
			if (value <= 0)
			{
				value = 0;
				OnReachedZeroValue?.Invoke();
				_hasReachedZero = true;
			}
			Number = value;
			textMP.text = value == 0 ? "" : value.ToString();
		}
	}
}