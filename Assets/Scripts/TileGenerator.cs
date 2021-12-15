using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileGenerator : MonoBehaviour
{
	[Header("Set Tile Properties")] [SerializeField]
	private Tile tile;

	[SerializeField] private int minRandomValue;
	[SerializeField] private int maxRandomValue;
	[SerializeField] private float numberAppearancePossibility;

	[Header("Set Board Dimension")] [SerializeField]
	private int columnSize;

	[SerializeField] private int rowSize;
	[SerializeField] private int marginPerTile;

	public static Action<Vector2> OnGeneratorInitialized;

	private Vector2 tileSpriteSize;
	private float[,] tiles;

	private void Start()
	{
		tileSpriteSize = tile.GetComponent<SpriteRenderer>().size;
		OnGeneratorInitialized?.Invoke(
			new Vector2(
				(columnSize - 1f) / 2 * tileSpriteSize.x, 
				(rowSize - 1f) / 2 * tileSpriteSize.y
			)
		);
		GenerateTiles();
	}

	private void GenerateTiles()
	{
		for (var row = 0; row < rowSize; row++)
		{
			for (var column = 0; column < columnSize; column++)
			{
				var instantiatedTile = Instantiate(tile.gameObject, transform);
				var position = transform.position;

				GenerateRandomNumber(instantiatedTile);

				instantiatedTile.transform.position = new Vector2(
					position.x + tileSpriteSize.x * column,
					position.y + tileSpriteSize.y * row
				);
			}
		}
	}

	private void GenerateRandomNumber(GameObject instantiatedTile)
	{
		var result = Random.Range(0f, 1f);
		if (result <= numberAppearancePossibility)
			instantiatedTile.GetComponent<Tile>().SetValue(Random.Range(minRandomValue, maxRandomValue));
	}
}