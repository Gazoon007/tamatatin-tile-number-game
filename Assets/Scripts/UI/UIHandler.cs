using System.Collections;
using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIHandler : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI textMp;
		[SerializeField] private RectTransform panel;
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
			panel.GetComponent<Image>().enabled = true;
		}

		private IEnumerator DelayingUpdateTurnUI()
		{
			yield return new WaitForSeconds(0.1f);
			textMp.text = $"TURN {GameManager.Instance.Turn.ToString()}";
		}
	
		private void UpdateTurnUI()
		{
			StartCoroutine(DelayingUpdateTurnUI());
		}
	}
}
