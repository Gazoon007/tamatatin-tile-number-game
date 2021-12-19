using System.Collections;
using Core;
using TMPro;
using UnityEngine;

namespace UI
{
	public class UIHandler : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI gameTurnText;
		[SerializeField] private RectTransform panel;
		[SerializeField] private RectTransform selectionButtonModes;
		[SerializeField] private PlayerController playerController;

		private void OnEnable()
		{
			playerController.OnClickToTile += UpdateTurnUI;
			GameManager.Instance.OnFinishedGame += ShowFinishedPanel;
		}

		private void OnDisable()
		{
			playerController.OnClickToTile -= UpdateTurnUI;
			GameManager.Instance.OnFinishedGame -= ShowFinishedPanel;
		}

		private void ShowFinishedPanel()
		{
			panel.gameObject.SetActive(true);
			selectionButtonModes.gameObject.SetActive(false);
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
	}
}
