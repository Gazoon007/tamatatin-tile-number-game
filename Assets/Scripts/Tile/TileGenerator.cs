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

		public Action<Vector2> OnGeneratorInitialized;
		public Action OnGeneratorFinished;

		private Vector2 _tileSpriteSize;
		private TileUnit _tileUnit;

		private void Start()
		{
			_tileSpriteSize = TileManager.Instance.TileSpriteSize;
			_tileUnit = TileManager.Instance.TileUnit;

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
				var instantiatedTile = Instantiate(_tileUnit, transform);
				var position = transform.position;

				GenerateRandomNumber(instantiatedTile);

				instantiatedTile.transform.position = new Vector2(
					position.x + _tileSpriteSize.x * column,
					position.y + _tileSpriteSize.y * row
				);

				TileManager.Instance.Tiles[((Vector2)instantiatedTile.transform.position).Round()] = instantiatedTile;
			}
			OnGeneratorFinished?.Invoke();
		}

		private void GenerateRandomNumber(TileUnit instantiatedTileUnit)
		{
			var result = Random.Range(0f, 1f);
			if (result <= numberAppearancePossibility)
				instantiatedTileUnit.SetValue(Random.Range(minRandomValue, maxRandomValue));
			else
				instantiatedTileUnit.SetValue(0);
		}
	}
}