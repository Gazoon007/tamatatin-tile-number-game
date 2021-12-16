using Tile;

namespace Selection_Mode
{
	public interface ISelection
	{
		void MapSelectedTile(HighlightTile highlightTile);
		void DecreaseValue(Tile.Tile tile);
	}
}