using System.Collections;
using Core;
using Tile;
using TMPro;
using UnityEngine;

namespace UI
{
	public class BoardChooser : MonoBehaviour
	{
		[SerializeField] private Boards board;
		[SerializeField] private RectTransform errorPanel;
		[SerializeField] private TextMeshProUGUI errorMessage;

		private PlayerController _playerController;

		private void OnEnable()
		{
			_playerController = FindObjectOfType<PlayerController>();
			_playerController.OnClickToTile += ResetErrorContent;
		}

		private void OnDisable()
		{
			_playerController.OnClickToTile -= ResetErrorContent;
		}

		public void OnSelectedBoard()
		{
			var tileGeneratorInstance = TileGenerator.Instance;
			var column = (int) tileGeneratorInstance.ColumnAndRow.x;
			var row = (int) tileGeneratorInstance.ColumnAndRow.y;

			if (board == Boards.Hourglass && column < row - 1)
			{
				errorMessage.text =
					"Can't choose this board, the value that are supported are rowSize must not be greater than the columnSize value. " +
					"\n" +
					"rowSize can be greater by 1 value than columnSize but the rowSize must be an even number";
				errorPanel.gameObject.SetActive(true);
				StartCoroutine(DelayingPanelToDisappear());
			}
			else if (board == Boards.Round && (column != row || column % 2 == 0))
			{
				errorMessage.text =
					"Can't choose this board, the value that are supported are SAME VALUE between columnSize and rowSize and the value must be odd";
				errorPanel.gameObject.SetActive(true);
				StartCoroutine(DelayingPanelToDisappear());
			}
			else
			{
				TileGenerator.Instance.CurrentBoard = board;
			}
		}

		private void ResetErrorContent()
		{
			errorMessage.text = "";
			errorPanel.gameObject.SetActive(false);
		}

		IEnumerator DelayingPanelToDisappear()
		{
			yield return new WaitForSeconds(10f);
			ResetErrorContent();
		}
	}
}