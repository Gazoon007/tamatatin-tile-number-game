using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core
{
	public class PlayerController : MonoBehaviour
	{
		public Action OnClickToTile;

		private void Update ()
		{
			if (Input.GetMouseButtonDown(0)) 
			{
				var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				var mousePos2D = new Vector2(mousePos.x, mousePos.y);
				var hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
				
				if (hit.collider == null) return;
				if (hit.collider.CompareTag("Tiles")) 
				{
					OnClickToTile?.Invoke();
				}
			}
		}
	}
}