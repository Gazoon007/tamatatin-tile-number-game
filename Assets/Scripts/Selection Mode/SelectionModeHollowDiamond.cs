using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Helper;
using Sirenix.OdinInspector;
using Tile;
using UnityEngine;

namespace Selection_Mode
{
	[CreateAssetMenu(menuName = "SelectionMode/Hollow Diamond Mode")]
	public class SelectionModeHollowDiamond : SelectionModeData
	{
		[MinValue(2)] [PropertyTooltip("Minimum Value is 2")]
		public int rangeTile = 2;

		public int attackValue = 1;

		public override async void MapSelectedTile(HighlightTile highlightTile)
		{
			var tileSpriteSize = TileManager.Instance.TileSpriteSize;
			var tilePosition = highlightTile.transform.position;
			ConsoleProDebug.Watch("Center Point", highlightTile.transform.position.ToString());

			var listOfSides = new[] { "leftToTop", "topToRight", "rightToBottom", "bottomToLeft" };
			var tasks = listOfSides
				.Select(side => IterateTileByDimension(side, tilePosition, tileSpriteSize))
				.ToList();

			await Task.WhenAll(tasks);
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