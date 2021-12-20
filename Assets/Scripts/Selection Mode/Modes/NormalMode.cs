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
	
	/// <summary>
	/// Odin Attribute Processor will update the attribute of selected class dynamically, so we can have certain validation
	/// for each mode that derived from the abstract class.
	/// </summary>
	[UsedImplicitly]
	public class NormalModeAttributeProcessor : OdinAttributeProcessor<NormalMode>
	{
		public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
		{
			if (member.Name == "rangeTile")
			{
				attributes.Add(new InfoBoxAttribute("You can only set the value only 0", InfoMessageType.Warning));
				attributes.Add(new MinValueAttribute(0));
				attributes.Add(new MaxValueAttribute(0));
			}

			if (member.Name == "attackValue")
			{
				attributes.Add(new InfoBoxAttribute("You can only set the value from 1", InfoMessageType.Warning));
				attributes.Add(new MinValueAttribute(1));
			}
		}
	}
}