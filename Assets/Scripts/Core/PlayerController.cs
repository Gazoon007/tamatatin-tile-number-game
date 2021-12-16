using System;
using UnityEngine;

namespace Core
{
	public class PlayerController : MonoBehaviour
	{
		public Action OnClickToTile;

		private void Update()
		{
			if (Input.GetMouseButtonDown(0)) OnClickToTile?.Invoke();
		}
	}
}