using System;
using Core;
using Core.Helper;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tile
{
	public class TileGenerator : Core.Helper.Singleton<TileGenerator>
	{
		[Header("Set Random Properties")]
		[SerializeField] private int minRandomValue;
		[SerializeField] private int maxRandomValue;
		[SerializeField] private float numberAppearancePossibility;

		[Header("Set Board Dimension")]
		[DetailedInfoBox("Beware!!! read the details before you want to change this settings",
			"If you choose round board later, " +
			"for now the value that are supported are SAME VALUE between columnSize and rowSize and the value must be odd, " +
			"otherwise it will be break" +
			"\n\n" +
			"then, if you choose hourglass later, " +
			"for now the value that are supported are rowSize must not be than the columnSize value, rowSize can be " +
			"greater by 1 value than columnSize but the rowSize must be an even number" +
			"\n\n" +
			"Rectangle is supported for all kinds of value."
			, InfoMessageType.Warning)]
		[SerializeField] private int columnSize;
		[SerializeField] private int rowSize;

		public Action<Vector2> OnGeneratorInitialized;
		public Action OnGeneratorFinished;
		public Vector2 ColumnAndRow => new(columnSize, rowSize);
		public Boards CurrentBoard
		{
			get => _currentBoard;
			set => SetBoard(value);
		}

		private Vector2 _tileSpriteSize;
		private TileUnit _tileUnit;
		private Boards _currentBoard = Boards.Rectangle;

		private void Start()
		{
			InitializeGenerator();
			GenerateTiles();
		}

		private void InitializeGenerator()
		{
			_tileSpriteSize = TileManager.Instance.TileSpriteSize;
			_tileUnit = TileManager.Instance.TileUnit;

			OnGeneratorInitialized?.Invoke(
				new Vector2(
					(columnSize - 1f) / 2 * _tileSpriteSize.x,
					(rowSize - 1f) / 2 * _tileSpriteSize.y
				)
			);
		}

		/// <summary>
		/// Use for Set re-set everything when user choose to play with other board.
		/// </summary>
		/// <param name="value"></param>
		private void SetBoard(Boards value)
		{
			_currentBoard = value;
			GameManager.Instance.ResetGame();
			TileManager.Instance.Tiles.Clear();

			// This is not a cool practice, we should use ObjectPool by Unity instead, but since we change board not as
			// often as shooting a bullet with a gun or jet, i think it won't cost so much.
			foreach (Transform child in gameObject.transform) Destroy(child.gameObject);

			InitializeGenerator();
			GenerateTiles();
		}

		// This region actually serve a VERY bad practice, because we violate OCP, and also had some repeated code on
		// certain part, we should handle it like we handle SelectionMode namespace that use strategy pattern, because
		// I have no time left, I will left the message here so you can understand that i'm indeed concern about this
		// bad practice. TODO: Implement strategy pattern for accomodate this board changer logic.

	#region Board Changer Logic

		private void GenerateTiles()
		{
			switch (_currentBoard)
			{
				case Boards.Rectangle:
					RectangleBoard();
					break;
				case Boards.Hourglass:
					HourglassBoard();
					break;
				case Boards.Round:
					RoundBoard();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			OnGeneratorFinished?.Invoke();
		}

		private void RectangleBoard()
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
		}

		private void HourglassBoard()
		{
			var slimmer = 0;
			for (var row = 0; row < rowSize; row++)
			{
				for (var column = slimmer; column < columnSize - slimmer; column++)
				{
					var instantiatedTile = Instantiate(_tileUnit, transform);
					var position = transform.position;

					GenerateRandomNumber(instantiatedTile);

					instantiatedTile.transform.position = new Vector2(
						position.x + _tileSpriteSize.x * column,
						position.y + _tileSpriteSize.y * row
					);

					TileManager.Instance.Tiles[((Vector2)instantiatedTile.transform.position).Round()] =
						instantiatedTile;
				}

				if (row != 0 && row > rowSize / 2 - 1)
					slimmer--;
				else if (rowSize % 2 != 0) slimmer++;
				else if (row != rowSize / 2 - 1) slimmer++;
			}
		}

		private void RoundBoard()
		{
			int xCoord, yCoord, point;

			for (var row = 0; row <= rowSize - 1; row++)
			{
				for (var col = 0; col <= columnSize - 1; col++)
				{
					xCoord = (columnSize - 1) / 2 - row;
					yCoord = (rowSize - 1) / 2 - col;

					point = xCoord * xCoord + yCoord * yCoord;
					if (point <= (columnSize - 1) / 2 * (rowSize - 1) / 2 + 1)
					{
						var instantiatedTile = Instantiate(_tileUnit, transform);
						var position = transform.position;

						GenerateRandomNumber(instantiatedTile);

						instantiatedTile.transform.position = new Vector2(
							position.x + _tileSpriteSize.x * (xCoord + (columnSize - 1) / 2),
							position.y + _tileSpriteSize.y * (yCoord + (rowSize - 1) / 2)
						);
						TileManager.Instance.Tiles[((Vector2)instantiatedTile.transform.position).Round()] =
							instantiatedTile;
					}
				}
			}
		}

		private void GenerateRandomNumber(TileUnit instantiatedTileUnit)
		{
			var result = Random.Range(0f, 1f);
			if (result <= numberAppearancePossibility)
				instantiatedTileUnit.SetValue(Random.Range(minRandomValue, maxRandomValue));
			else
				instantiatedTileUnit.SetValue(0);
		}

	#endregion
	}
}