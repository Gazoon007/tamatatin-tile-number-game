using Selection_Mode;
using UnityEngine;

namespace Tile
{
	public class HighlightTile : MonoBehaviour
	{
		[SerializeField] private SelectionModeData selectionMode;

		private SpriteRenderer _highlightedTileSprite;
		public bool IsTileHighlighted { get; set; }

		private void Start()
		{
			_highlightedTileSprite = GetComponentsInChildren<SpriteRenderer>()[1];
			_highlightedTileSprite.enabled = false;
		}

		private void OnMouseEnter()
		{
			selectionMode.MapSelectedTile(this);
		}

		private void OnMouseExit()
		{
			foreach (var entry in TileManager.Instance.HighlightedTiles)
			{
				entry.Value.IsTileHighlighted = false;
				entry.Value.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
			}

			TileManager.Instance.HighlightedTiles.Clear();
		}
	}
}