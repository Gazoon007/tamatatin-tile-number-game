using System;
using Core.Helper;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tile
{
	public class TileGenerator : Singleton<TileGenerator>
	{
		[SerializeField] private int minRandomValue;
		[SerializeField] private int maxRandomValue;
		[SerializeField] private float numberAppearancePossibility;

		[Header("Set Board Dimension")] [SerializeField]
		private int columnSize;

		[SerializeField] private int rowSize;
		[SerializeField] private int marginPerTile;

		public static Action<Vector2> OnGeneratorInitialized;

		private Vector2 _tileSpriteSize;
		private Tile _tile;

		private void Start()
		{
			_tileSpriteSize = TileManager.Instance.TileSpriteSize;
			_tile = TileManager.Instance.Tile;

			OnGeneratorInitialized?.Invoke(
				new Vector2(
					(columnSize - 1f) / 2 * _tileSpriteSize.x,
					(rowSize - 1f) / 2 * _tileSpriteSize.y
				)
			);
			GenerateTiles();
		}

		private void GenerateTiles()
		{
			for (var row = 0; row < rowSize; row++)
			for (var column = 0; column < columnSize; column++)
			{
				var instantiatedTile = Instantiate(_tile, transform);
				var position = transform.position;

				GenerateRandomNumber(instantiatedTile);

				instantiatedTile.transform.position = new Vector2(
					position.x + _tileSpriteSize.x * column,
					position.y + _tileSpriteSize.y * row
				);

				TileManager.Instance.Tiles[((Vector2)instantiatedTile.transform.position).Round()] = instantiatedTile;
			}
		}

		private void GenerateRandomNumber(Tile instantiatedTile)
		{
			var result = Random.Range(0f, 1f);
			if (result <= numberAppearancePossibility)
				instantiatedTile.SetValue(Random.Range(minRandomValue, maxRandomValue));
		}
	}
}