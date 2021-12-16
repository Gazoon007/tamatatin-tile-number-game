﻿using UnityEngine;

namespace Core.Helper
{
	public class Singleton<T> : MonoBehaviour where T : Component
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindObjectOfType<T>();
					if (_instance == null)
					{
						var newGO = new GameObject();
						_instance = newGO.AddComponent<T>();
					}
				}

				return _instance;
			}
		}

		private void Awake()
		{
			_instance = this as T;
		}
	}
}