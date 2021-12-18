using System;
using Core.Helper;
using Tile;
using TMPro;
using UnityEngine;

namespace Core
{
	public class GameManager : Singleton<GameManager>
	{
		private PlayerController _playerController;
		private int _turn = 1;
		private int _tilesZeroValueQty;
		
		public Action OnFinishedGame;
		public int Turn => _turn;

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
			foreach (var tile in FindObjectsOfType<TileUnit>())
			{
				tile.OnReachedZeroValue += CheckGameState;
			}
			
			// Solution to follow SRP, but still hasn't worked properly
			// TileManager.Instance.ExecuteMethodPerTile("OnReachedZeroValue", CheckGameState);
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
	}
}