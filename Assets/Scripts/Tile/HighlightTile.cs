using Core;
using UnityEngine;

namespace Tile
{
	public class HighlightTile : MonoBehaviour
	{
		private SpriteRenderer _highlightedTileSprite;
		public bool IsTileHighlighted { get; set; }

		private void Start()
		{
			_highlightedTileSprite = GetComponentsInChildren<SpriteRenderer>()[1];
			_highlightedTileSprite.enabled = false;
		}

		private void OnMouseEnter()
		{
			GameManager.Instance.SelectionMode?.MapSelectedTile(this);
		}

		private void OnMouseExit()
		{
			// TODO: Need refactor, violate DRY
			foreach (var entry in TileManager.Instance.HighlightedTiles)
			{
				entry.Value.IsTileHighlighted = false;
				entry.Value.GetComponentsInChildren<SpriteRenderer>()[1].enabled = false;
			}

			TileManager.Instance.HighlightedTiles.Clear();
		}
	}
}