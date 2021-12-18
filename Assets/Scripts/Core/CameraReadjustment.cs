using Tile;
using UnityEngine;

namespace Core
{
	public class CameraReadjustment : MonoBehaviour
	{
		private void OnEnable()
		{
			TileGenerator.Instance.OnGeneratorInitialized += ReadjustCamera;
		}

		private void OnDisable()
		{
			TileGenerator.Instance.OnGeneratorInitialized -= ReadjustCamera;
		}

		private void ReadjustCamera(Vector2 newPosition)
		{
			var mainCam = GetComponent<Camera>();
			mainCam.transform.position = new Vector3(newPosition.x, newPosition.y, mainCam.transform.position.z);
		}
	}
}