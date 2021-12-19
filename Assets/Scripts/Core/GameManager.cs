using System;
using Core.Helper;
using Selection_Mode;
using Tile;
using UnityEngine;

namespace Core
{
	public class GameManager : Singleton<GameManager>
	{
		[SerializeField] private SelectionModeData[] selectionModes;

		private PlayerController _playerController;
		private int _turn = 1;
		private int _tilesZeroValueQty;
		private SelectionModeData _selectionMode;

		public Action OnFinishedGame;
		public int Turn => _turn;
		public SelectionModeData SelectionMode => _selectionMode;
		public SelectionModeData[] SelectionModes => selectionModes;

		private void OnEnable()
		{
			_playerController = GetComponent<PlayerController>();

			TileGenerator.Instance.OnGeneratorFinished += GetReferenceOfAllTiles;
			if (_playerController != null)
				_playerController.OnClickToTile += UpdateTurn;
		}

		private void OnDisable()
		{
			TileGenerator.Instance.OnGeneratorFinished -= GetReferenceOfAllTiles;
			_playerController.OnClickToTile -= UpdateTurn;
		}

		// Need refactor, violate SRP
		private void GetReferenceOfAllTiles()
		{
			// Need to be refactored, violate SRP
			// foreach (var tile in FindObjectsOfType<TileUnit>())
			// {
			// 	tile.OnReachedZeroValue += CheckGameState;
			// }

			// Solution to follow SRP
			TileManager.Instance.ExecuteMethodPerTile("OnReachedZeroValueProp", CheckGameState);
		}

		private void CheckGameState()
		{
			_tilesZeroValueQty += 1;
			if (_tilesZeroValueQty == TileManager.Instance.TotalTiles)
			{
				FinishGame();
			}
		}

		private void FinishGame()
		{
			OnFinishedGame?.Invoke();
			ResetTurn();
		}

		private void UpdateTurn()
		{
			_turn += 1;
		}

		private void ResetTurn()
		{
			_turn = 1;
		}

		public void ChangeSelection(Selections selection, Vector2 attackAndRangeValue)
		{
			_selectionMode = null;
			foreach (var selectionMode in selectionModes)
			{
				if (selection == selectionMode.selection && 
				    (int)attackAndRangeValue.x == selectionMode.attackValue &&
				    (int)attackAndRangeValue.y == selectionMode.rangeTile)
					_selectionMode = selectionMode;
			}
		}
	}
}