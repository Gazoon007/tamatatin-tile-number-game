using Tile;
using UnityEngine;

namespace Selection_Mode
{
	public abstract class SelectionModeData : ScriptableObject, ISelection
	{
		public abstract void MapSelectedTile(HighlightTile highlightTile);
		public abstract void DecreaseValue(Tile.Tile tile);
	}
}