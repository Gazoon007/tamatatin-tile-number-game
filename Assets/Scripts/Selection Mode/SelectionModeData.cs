using System.ComponentModel;
using Sirenix.OdinInspector;
using Tile;
using UnityEngine;

namespace Selection_Mode
{
	/// <summary>
	/// Here implement strategy pattern, that integrate with Unity's ScriptableObject. So the designer have the advantage
	/// to edit for each mode based on its range or attackValue.
	///
	/// For example, the plus mode can have a different range or attack value, we can have as many as designer want for
	/// each mode, just create scriptable object instance in inspector, and add it to the lists in game manager.
	/// </summary>
	public abstract class SelectionModeData : ScriptableObject, ISelection
	{
		public Selections selection;
		[InfoBox("Range of tile from center point of your current cursor location")]
		public int rangeTile;
		[InfoBox("Attack value for each click by your cursor that affect the tile's value regression ")]
		public int attackValue;
		
		public abstract void MapSelectedTile(HighlightTile highlightTile);
		public abstract void DecreaseValue(TileUnit tileUnit);
	}
}