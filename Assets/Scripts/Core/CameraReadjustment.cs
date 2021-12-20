using Tile;
using UnityEngine;

namespace Core
{
	public class CameraReadjustment : MonoBehaviour
	{

		[SerializeField] private float cameraZoomOutRatio = 1.4f;

		private void OnEnable()
		{
			TileGenerator.Instance.OnGeneratorInitialized += ReadjustCamera;
		}

		private void OnDisable()
		{
			if(!gameObject.scene.isLoaded) return;
			TileGenerator.Instance.OnGeneratorInitialized -= ReadjustCamera;
		}

		/// <summary>
		/// Readjust camera based on the board dimension, if the board is large then the camera will zoom out a bit.
		/// </summary>
		/// <param name="centerOfTheBoardPosition"></param>
		private void ReadjustCamera(Vector2 centerOfTheBoardPosition)
		{
			var mainCam = GetComponent<Camera>();
			mainCam.transform.position = new Vector3(centerOfTheBoardPosition.x, centerOfTheBoardPosition.y, mainCam.transform.position.z);
			
			var columnAndRow = TileGenerator.Instance.ColumnAndRow;
			var maxValue = Mathf.Max(columnAndRow.x, columnAndRow.y);
			var expanded = Mathf.FloorToInt(maxValue * cameraZoomOutRatio);
			
			mainCam.orthographicSize = expanded;
		}
	}
}