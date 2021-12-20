using System.Collections;
using Core;
using Tile;
using TMPro;
using UnityEngine;

namespace UI
{
	/// <summary>
	/// This is handling UI stuff like updating the text of turn, show panel when finished.
	/// </summary>
	public class UIHandler : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI gameTurnText;
		[SerializeField] private TextMeshProUGUI finalGameTurnText;
		[SerializeField] private RectTransform panel;
		[SerializeField] private RectTransform selectionModeButtons;
		[SerializeField] private RectTransform boardSelectionButtons;
		[SerializeField] private PlayerController playerController;

		private void OnEnable()
		{
			playerController.OnClickToTile += UpdateTurnUI;
			GameManager.Instance.OnFinishedGame += ShowFinishedPanel;
		}

		private void OnDisable()
		{
			if(!gameObject.scene.isLoaded) return;
			playerController.OnClickToTile -= UpdateTurnUI;
			GameManager.Instance.OnFinishedGame -= ShowFinishedPanel;
		}

		private void ShowFinishedPanel()
		{
			panel.gameObject.SetActive(true);
			finalGameTurnText.text = $"Turn Used: {GameManager.Instance.Turn.ToString()}";
			selectionModeButtons.gameObject.SetActive(false);
			boardSelectionButtons.gameObject.SetActive(false);
		}

		private IEnumerator DelayingUpdateTurnUI()
		{
			yield return new WaitForSeconds(0.1f);
			gameTurnText.text = $"TURN {GameManager.Instance.Turn.ToString()}";
		}
	
		private void UpdateTurnUI()
		{
			StartCoroutine(DelayingUpdateTurnUI());
		}

		public void TurnOffPanel()
		{
			TileGenerator.Instance.CurrentBoard = Boards.Rectangle;
			panel.gameObject.SetActive(false);
			selectionModeButtons.gameObject.SetActive(true);
			boardSelectionButtons.gameObject.SetActive(true);
		}
	}
}
