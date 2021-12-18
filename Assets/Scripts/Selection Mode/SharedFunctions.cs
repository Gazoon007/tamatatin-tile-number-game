using Core.Helper;
using Tile;
using UnityEngine;

namespace Selection_Mode
{
	public class SharedFunctions : MonoBehaviour
	{
		public static void MapHighlightTileAtPosition(Vector2 tileAtPosition)
		{
			var selectedTile = TileManager.Instance.GetTileAtPosition(tileAtPosition.Round());

			if (selectedTile != null)
			{
				var spriteRenderer = selectedTile.GetComponentsInChildren<SpriteRenderer>()[1];
				var highlightedTile = selectedTile.GetComponent<HighlightTile>();

				spriteRenderer.enabled = true;
				highlightedTile.IsTileHighlighted = true;

				TileManager.Instance.HighlightedTiles[tileAtPosition.Round()] = highlightedTile;
			}
		}
	}
}