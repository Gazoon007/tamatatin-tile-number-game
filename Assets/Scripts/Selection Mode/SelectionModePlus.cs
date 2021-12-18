using Core.Helper;
using Sirenix.OdinInspector;
using Tile;
using UnityEngine;

namespace Selection_Mode
{
	[CreateAssetMenu(menuName = "SelectionMode/Plus Mode")]
	public class SelectionModePlus : SelectionModeData
	{
		[MinValue(1)] public int rangeTile = 2;
		public int attackValue = 1;

		public override void MapSelectedTile(HighlightTile highlightTile)
		{
			var tileSpriteSize = TileManager.Instance.TileSpriteSize;
			var tilePosition = highlightTile.transform.position;
			ConsoleProDebug.Watch("Center Point", highlightTile.transform.position.ToString());
			IterateTileByDimension("column", tilePosition, tileSpriteSize);
			IterateTileByDimension("row", tilePosition, tileSpriteSize);
		}

		public override void DecreaseValue(TileUnit tileUnit)
		{
			tileUnit.SetValue(tileUnit.Number - attackValue);
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
				
				SharedFunctions.MapHighlightTileAtPosition(tileAtPosition);
			}
		}
	}
}