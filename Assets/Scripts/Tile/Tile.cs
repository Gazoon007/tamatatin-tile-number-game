using Core;
using Selection_Mode;
using TMPro;
using UnityEngine;

namespace Tile
{
	public class Tile : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI textMP;
		[SerializeField] private SelectionModeData selectionMode;

		private int _number;
		private HighlightTile _highlightTile;
		private PlayerController _playerController;

		public int Number => _number;

		private void OnEnable()
		{
			_playerController = FindObjectOfType<PlayerController>();
			_playerController.OnClickToTile += Hit;
		}

		private void OnDisable()
		{
			_playerController.OnClickToTile -= Hit;
		}

		private void Start()
		{
			_highlightTile = GetComponent<HighlightTile>();
		}

		private void Hit()
		{
			if (_highlightTile.IsTileHighlighted)
				selectionMode.DecreaseValue(this);
		}

		public void SetValue(int value)
		{
			if (value <= 0) value = 0;
			_number = value;
			textMP.text = value == 0 ? "" : value.ToString();
		}
	}
}