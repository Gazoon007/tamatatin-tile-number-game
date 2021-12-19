using System;
using System.Collections.Generic;
using System.Reflection;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
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
	
	[UsedImplicitly]
	public class NormalModeAttributeProcessor : OdinAttributeProcessor<NormalMode>
	{
		public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
		{
			if (member.Name == "rangeTile")
			{
				attributes.Add(new MinValueAttribute(0));
				attributes.Add(new MaxValueAttribute(0));
			}
			
			if (member.Name == "attackValue")
				attributes.Add(new MinValueAttribute(1));
		}
	}
}