using System.Threading.Tasks;
using Tile;
using UnityEngine;

namespace Selection_Mode.Modes
{
	[CreateAssetMenu(menuName = "SelectionMode/Diamond Mode")]
	public class DiamondMode : SelectionModeData
	{
		public override async void MapSelectedTile(HighlightTile highlightTile)
		{
			var listOfSides = new[] { "leftToTop", "topToRight", "rightToBottom", "bottomToLeft" };
			
			await SharedFunctions.SetupTheProperDimension(highlightTile, listOfSides, IterateTileByDimension);
		}

		public override void DecreaseValue(TileUnit tileUnit)
		{
			tileUnit.SetValue(tileUnit.Number - attackValue);
		}

		private Task IterateTileByDimension(string dimensionType, Vector2 centerPoint, Vector2 tileSpriteSize)
		{
			for (var dimension = -rangeTile; dimension <= rangeTile - (rangeTile + 1); dimension++)
			{
				var tileAtPosition = dimensionType switch
				{
					"leftToTop" => new Vector2(centerPoint.x + dimension * tileSpriteSize.x,
						centerPoint.y + (rangeTile * tileSpriteSize.y + dimension * tileSpriteSize.y)),
					"topToRight" => new Vector2(
						centerPoint.x + (rangeTile * tileSpriteSize.x + dimension * tileSpriteSize.x),
						centerPoint.y - dimension * tileSpriteSize.y),
					"rightToBottom" => new Vector2(centerPoint.x - dimension * tileSpriteSize.x,
						centerPoint.y - dimension * tileSpriteSize.y - rangeTile * tileSpriteSize.y),
					"bottomToLeft" => new Vector2(
						centerPoint.x - (dimension * tileSpriteSize.x + rangeTile * tileSpriteSize.x),
						centerPoint.y + dimension * tileSpriteSize.y),
					_ => default
				};

				SharedFunctions.MapHighlightTileAtPosition(tileAtPosition);
			}

			return Task.CompletedTask;
		}
	}
}