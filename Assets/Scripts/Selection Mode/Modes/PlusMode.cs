using System.Threading.Tasks;
using Tile;
using UnityEngine;

namespace Selection_Mode.Modes
{
	[CreateAssetMenu(menuName = "SelectionMode/Plus Mode")]
	public class PlusMode : SelectionModeData
	{
		public override async void MapSelectedTile(HighlightTile highlightTile)
		{
			var listOfSides = new[] { "column", "row" };
			
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
					"column" => new Vector2(centerPoint.x + dimension * tileSpriteSize.x, centerPoint.y),
					"row" => new Vector2(centerPoint.x, centerPoint.y + dimension * tileSpriteSize.y),
					_ => default
				};
				
				SharedFunctions.MapHighlightTileAtPosition(tileAtPosition);
			}
			
			return Task.CompletedTask;
		}
	}
	
}