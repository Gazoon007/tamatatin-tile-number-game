using System;

namespace Core.Helper.OdinCommunityTools
{
	public class ListItemSelectorAttribute : Attribute
	{
		public string SetSelectedMethod;

		public ListItemSelectorAttribute(string setSelectedMethod)
		{
			this.SetSelectedMethod = setSelectedMethod;
		}
	}
}