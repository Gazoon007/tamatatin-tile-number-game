using System;
using System.Collections.Generic;
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
		
		public void ExecuteMethodPerTile(string delegatedObserverPropName, Action delegatedMethod)
		{
			foreach (var (_, value) in _tiles)
			{
				var prop = value.GetType().GetProperty(delegatedObserverPropName);
				var observer = prop?.GetValue(value);
				prop?.SetValue(value, (Action) observer + delegatedMethod);
			}
		}
	}
}