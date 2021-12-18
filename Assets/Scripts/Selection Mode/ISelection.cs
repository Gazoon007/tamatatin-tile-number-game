using Tile;

namespace Selection_Mode
{
	public interface ISelection
	{
		void MapSelectedTile(HighlightTile highlightTile);
		void DecreaseValue(TileUnit tileUnit);
	}
}