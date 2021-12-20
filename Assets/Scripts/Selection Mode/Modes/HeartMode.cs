using System.Threading.Tasks;
using Tile;
using UnityEngine;

namespace Selection_Mode.Modes
{
	[CreateAssetMenu(menuName = "SelectionMode/Heart Mode")]
	public class HeartMode : SelectionModeData
	{
		public override async void MapSelectedTile(HighlightTile highlightTile)
		{
			var listOfSides = new[] { "bottomLeft", "bottomRight", "right", "left", "topLeft", "topRight" };
			
			await SharedFunctions.SetupTheProperDimension(highlightTile, listOfSides, IterateTileByDimension);
		}

		public override void DecreaseValue(TileUnit tileUnit)
		{
			tileUnit.SetValue(tileUnit.Number - attackValue);
		}

		private Task IterateTileByDimension(string dimensionType, Vector2 centerPoint, Vector2 tileSpriteSize)
		{
			for (var dimension = -rangeTile; dimension <= -1; dimension++)
			{
				Vector2 tileAtPosition;
				switch (dimensionType)
				{
					case "bottomRight" when dimension == -rangeTile:
						continue;
					case "bottomRight":
						tileAtPosition = new Vector2(centerPoint.x - dimension * tileSpriteSize.x,
							centerPoint.y - dimension * tileSpriteSize.y - rangeTile * tileSpriteSize.y);
						break;
					case "bottomLeft":
						tileAtPosition = new Vector2(
							centerPoint.x - (dimension * tileSpriteSize.x + rangeTile * tileSpriteSize.x),
							centerPoint.y + dimension * tileSpriteSize.y);
						break;
					case "right" when dimension == -rangeTile:
						tileAtPosition = new Vector2(
							centerPoint.x + (rangeTile * tileSpriteSize.x - 1 * tileSpriteSize.x),
							centerPoint.y + (-dimension - 1) * tileSpriteSize.y);
						break;
					case "right":
						tileAtPosition = new Vector2(
							centerPoint.x + rangeTile * tileSpriteSize.x,
							centerPoint.y + (-dimension - 1) * tileSpriteSize.y);
						break;
					case "left" when dimension == -rangeTile:
						tileAtPosition = new Vector2(
							centerPoint.x - (rangeTile * tileSpriteSize.x - 1 * tileSpriteSize.x ),
							centerPoint.y + (-dimension - 1) * tileSpriteSize.y);
						break;
					case "left":
						tileAtPosition = new Vector2(
							centerPoint.x - rangeTile * tileSpriteSize.x,
							centerPoint.y + (-dimension - 1) * tileSpriteSize.y);
						break;
					case "topLeft" when dimension == -rangeTile || dimension == -1:
						continue;
					case "topLeft":
						tileAtPosition = new Vector2(
							centerPoint.x + dimension * tileSpriteSize.x + 1 * tileSpriteSize.x,
							centerPoint.y + -dimension * tileSpriteSize.y);
						break;
					case "topRight" when dimension == -rangeTile:
						continue;
					case "topRight":
						tileAtPosition = new Vector2(
							centerPoint.x - dimension * tileSpriteSize.x - 1 * tileSpriteSize.x,
							centerPoint.y - dimension * tileSpriteSize.y);
						break;
					default:
						tileAtPosition = default;
						break;
				}

				SharedFunctions.MapHighlightTileAtPosition(tileAtPosition);
			}
			

			return Task.CompletedTask;
		}
	}
}