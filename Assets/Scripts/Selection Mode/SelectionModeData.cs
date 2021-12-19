using Sirenix.OdinInspector;
using Tile;
using UnityEngine;

namespace Selection_Mode
{
	public abstract class SelectionModeData : ScriptableObject, ISelection
	{
		public Selections selection;
		public int rangeTile;
		public int attackValue;
		
		public abstract void MapSelectedTile(HighlightTile highlightTile);
		public abstract void DecreaseValue(TileUnit tileUnit);
	}
}