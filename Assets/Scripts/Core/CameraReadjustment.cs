using Tile;
using UnityEngine;

namespace Core
{
	public class CameraReadjustment : MonoBehaviour
	{
		private Camera camera;

		private void OnEnable()
		{
			TileGenerator.OnGeneratorInitialized += ReadjustCamera;
		}

		private void OnDisable()
		{
			TileGenerator.OnGeneratorInitialized -= ReadjustCamera;
		}

		private void ReadjustCamera(Vector2 newPosition)
		{
			var mainCam = GetComponent<Camera>();
			mainCam.transform.position = new Vector3(newPosition.x, newPosition.y, mainCam.transform.position.z);
		}
	}
}