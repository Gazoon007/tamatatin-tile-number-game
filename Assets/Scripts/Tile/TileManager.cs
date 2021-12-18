using System;
using System.Collections.Generic;
using System.Reflection;
using Core.Helper;
using UnityEngine;

namespace Tile
{
	public class TileManager : Singleton<TileManager>
	{
		[Header("Set Tile Properties")] [SerializeField]
		private TileUnit tileUnit;

		private Dictionary<Vector2, TileUnit> _tiles = new();
		private Dictionary<Vector2, HighlightTile> _highlightedTiles = new();
		private Vector2 _tileSpriteSize;

		public Vector2 TileSpriteSize => _tileSpriteSize;
		public TileUnit TileUnit => tileUnit;
		public int TotalTiles => _tiles.Count;
		public Dictionary<Vector2, TileUnit> Tiles
		{
			get => _tiles;
			set => _tiles = value;
		}

		public Dictionary<Vector2, HighlightTile> HighlightedTiles
		{
			get => _highlightedTiles;
			set => _highlightedTiles = value;
		}

		private void Awake()
		{
			_tileSpriteSize = tileUnit.GetComponent<SpriteRenderer>().size;
		}

		public TileUnit GetTileAtPosition(Vector2 position)
		{
			return _tiles.TryGetValue(position, out var tileAtPosition) ? tileAtPosition : null;
		}
		
		
		// TODO: Fix this, how to get delgatedObserverName and dynamically listen to delegatedMethod 
		// public void ExecuteMethodPerTile(string delegatedObserverName, Action delegatedMethod)
		// {
		// 	foreach (var entry in _tiles)
		// 	{
		// 		var prop = entry.Value.GetType().GetProperty(delegatedObserverName);
		// 		var observer = prop?.GetValue(entry.Value);
		// 		prop?.SetValue(entry.Value, (Action) observer + delegatedMethod);
		// 	}
		// }
	}
}