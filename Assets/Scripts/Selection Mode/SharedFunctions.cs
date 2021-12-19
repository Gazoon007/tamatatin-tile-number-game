using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Helper;
using Tile;
using UnityEngine;
using System.Linq;

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
		
		public static async Task SetupTheProperDimension(HighlightTile highlightTile, IEnumerable<string> listOfSides, Func<string, Vector2, Vector2, Task> delegatedIterationTile)
		{
			var tileSpriteSize = TileManager.Instance.TileSpriteSize;
			var tilePosition = highlightTile.transform.position;
			ConsoleProDebug.Watch("Center Point", highlightTile.transform.position.ToString());
			
			var tasks = listOfSides
				.Select(side => delegatedIterationTile(side, tilePosition, tileSpriteSize))
				.ToList();

			await Task.WhenAll(tasks);
		}
	}
}