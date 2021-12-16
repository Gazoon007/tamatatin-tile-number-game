using System.Collections.Generic;
using Core.Helper;
using UnityEngine;

namespace Tile
{
	public class TileManager : Singleton<TileManager>
	{
		[Header("Set Tile Properties")] [SerializeField]
		private Tile tile;

		private Dictionary<Vector2, Tile> _tiles = new();
		private Dictionary<Vector2, HighlightTile> _highlightedTiles = new();
		private Vector2 _tileSpriteSize;

		public Vector2 TileSpriteSize => _tileSpriteSize;
		public Tile Tile => tile;

		public Dictionary<Vector2, Tile> Tiles
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
			_tileSpriteSize = tile.GetComponent<SpriteRenderer>().size;
		}

		public Tile GetTileAtPosition(Vector2 position)
		{
			return _tiles.TryGetValue(position, out var tileAtPosition) ? tileAtPosition : null;
		}
	}
}