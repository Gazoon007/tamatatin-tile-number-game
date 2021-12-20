using System;

namespace Core.Helper
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