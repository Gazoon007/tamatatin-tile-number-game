using Core.Helper;
using Tile;
using UnityEngine;

namespace Selection_Mode
{
	[CreateAssetMenu(menuName = "SelectionMode/Plus Mode")]
	public class SelectionModePlusData : SelectionModeData
	{
		public int attackValue = 1;
		public int rangeTile = 2;

		public override void MapSelectedTile(HighlightTile highlightTile)
		{
			var tileSpriteSize = TileManager.Instance.TileSpriteSize;
			var tilePosition = highlightTile.transform.position;
			ConsoleProDebug.Watch("Center Point", highlightTile.transform.position.ToString());
			IterateTileByDimension("column", tilePosition, tileSpriteSize);
			IterateTileByDimension("row", tilePosition, tileSpriteSize);
		}

		public override void DecreaseValue(Tile.Tile tile)
		{
			tile.SetValue(tile.Number - attackValue);
		}

		private void IterateTileByDimension(string dimensionType, Vector2 centerPoint, Vector2 tileSpriteSize)
		{
			for (var dimension = -rangeTile; dimension <= rangeTile; dimension++)
			{
				var tileAtPosition = dimensionType switch
				{
					"column" => new Vector2(centerPoint.x + dimension * tileSpriteSize.x, centerPoint.y),
					"row" => new Vector2(centerPoint.x, centerPoint.y + dimension * tileSpriteSize.y),
					_ => default
				};
				var selectedTile =
					TileManager.Instance.GetTileAtPosition(tileAtPosition.Round());

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
}