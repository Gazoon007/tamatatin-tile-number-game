using System;
using Core;
using Selection_Mode;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Tile
{
	/// <summary>
	/// Responsible for manage the logic for the individual tile.
	/// </summary>
	public class TileUnit : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI tileValueText;

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

		/// <summary>
		/// Debugging purpose only, it's easy to directly know the coordinate of for each tile by looking at the scene
		/// </summary>
		// void OnDrawGizmos() 
		// {
		// 	Handles.Label(transform.position + Vector3.up, transform.position.x + ", " + transform.position.y );
		// }
		
		private void OnEnable()
		{
			_playerController = FindObjectOfType<PlayerController>();
			_playerController.OnClickToTile += Hit;
			TileGenerator.Instance.OnGeneratorFinished += CheckTileFirst;
		}

		private void OnDisable()
		{
			if(!gameObject.scene.isLoaded) return;
			_playerController.OnClickToTile -= Hit;
			TileGenerator.Instance.OnGeneratorFinished -= CheckTileFirst;
		}

		private void Start()
		{
			_highlightTile = GetComponent<HighlightTile>();
		}

		/// <summary>
		/// Checking first if tile is already 0 by the random tile value generator, so if there are, it will update the
		/// counter of zero values in game manager right away the game start. 
		/// </summary>
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
			tileValueText.text = value == 0 ? "" : value.ToString();
		}
	}
}