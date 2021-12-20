using Tile;
using UnityEngine;

namespace Selection_Mode.Modes
{
	[CreateAssetMenu(menuName = "SelectionMode/Normal Mode")]
	public class NormalMode : SelectionModeData
	{
		public override void MapSelectedTile(HighlightTile highlightTile)
		{
			SharedFunctions.MapHighlightTileAtPosition(highlightTile.transform.position);
		}

		public override void DecreaseValue(TileUnit tileUnit)
		{
			tileUnit.SetValue(tileUnit.Number - attackValue);
		}
	}
}