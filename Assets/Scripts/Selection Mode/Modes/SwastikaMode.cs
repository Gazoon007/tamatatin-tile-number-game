﻿using System.Threading.Tasks;
using Tile;
using UnityEngine;

namespace Selection_Mode.Modes
{
	[CreateAssetMenu(menuName = "SelectionMode/Swastika Mode")]
	public class SwastikaMode : SelectionModeData
	{
		public override async void MapSelectedTile(HighlightTile highlightTile)
		{
			var listOfSides = new[] { "innerSideUpward", "innerSideRightward", "outerSideUpward", "outerSideRightward" };
			
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
				Vector2 tileAtPosition;
				if (dimensionType == "innerSideRightward")
					tileAtPosition = new Vector2(centerPoint.x + dimension * tileSpriteSize.x, centerPoint.y);
				else if (dimensionType == "innerSideUpward")
					tileAtPosition = new Vector2(centerPoint.x, centerPoint.y + dimension * tileSpriteSize.y);
				else if (dimensionType == "outerSideRightward")
				{
					if (dimension < 0)
						tileAtPosition = new Vector2(centerPoint.x + dimension * tileSpriteSize.x, centerPoint.y + rangeTile * tileSpriteSize.y);
					else if (dimension == 0)
						continue;
					else
						tileAtPosition = new Vector2(centerPoint.x + dimension * tileSpriteSize.x, centerPoint.y - rangeTile * tileSpriteSize.y);
				}
				else if (dimensionType == "outerSideUpward")
				{
					if (dimension < 0)
						tileAtPosition = new Vector2(centerPoint.x - rangeTile * tileSpriteSize.x, centerPoint.y + dimension * tileSpriteSize.y);
					else if (dimension == 0)
						continue;
					else
						tileAtPosition = new Vector2(centerPoint.x + rangeTile * tileSpriteSize.x, centerPoint.y + dimension * tileSpriteSize.y);
				}
				else
					tileAtPosition = default(Vector2);

				SharedFunctions.MapHighlightTileAtPosition(tileAtPosition);
			}
			
			return Task.CompletedTask;
		}
	}
}