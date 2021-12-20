using System.Threading.Tasks;
using Tile;
using UnityEngine;

namespace Selection_Mode.Modes
{
	[CreateAssetMenu(menuName = "SelectionMode/Diagonal Mode")]
	public class DiagonalMode : SelectionModeData
	{
		public override async void MapSelectedTile(HighlightTile highlightTile)
		{
			var listOfSides = new[] { "slash", "backslash" };

			await SharedFunctions.SetupTheProperDimension(highlightTile, listOfSides, IterateTileByDimension);
		}

		public override void DecreaseValue(TileUnit tileUnit)
		{
			tileUnit.SetValue(tileUnit.Number - attackValue);
		}

		private Task IterateTileByDimension(string dimensionType, Vector2 centerPoint, Vector2 tileSpriteSize)
		{
			for (var dimension = -rangeTile; dimension <= rangeTile; dimension++)
			{
				var tileAtPosition = dimensionType switch
				{
					"slash" => new Vector2(centerPoint.x + dimension * tileSpriteSize.x,
						centerPoint.y + dimension * tileSpriteSize.y),
					"backslash" => new Vector2(centerPoint.x + -dimension * tileSpriteSize.x,
						centerPoint.y + dimension * tileSpriteSize.y),
					_ => default
				};

				SharedFunctions.MapHighlightTileAtPosition(tileAtPosition);
			}

			return Task.CompletedTask;
		}
	}
}