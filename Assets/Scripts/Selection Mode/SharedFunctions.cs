using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Helper;
using Tile;
using UnityEngine;
using System.Linq;

namespace Selection_Mode
{
	/// <summary>
	/// The purpose of this class is shared a functions across the Selection_Mode namespace so we can eliminate
	/// repeated code.
	/// </summary>
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

		public static async Task SetupTheProperDimension(HighlightTile highlightTile, IEnumerable<string> listOfSides,
			Func<string, Vector2, Vector2, Task> delegatedIterationTile)
		{
			var tileSpriteSize = TileManager.Instance.TileSpriteSize;
			var tilePosition = highlightTile.transform.position;

			var tasks = listOfSides
				.Select(side => delegatedIterationTile(side, tilePosition, tileSpriteSize))
				.ToList();

			await Task.WhenAll(tasks);
		}
	}
}