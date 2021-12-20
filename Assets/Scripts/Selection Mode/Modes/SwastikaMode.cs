using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Tile;
using UnityEngine;

namespace Selection_Mode.Modes
{
	[CreateAssetMenu(menuName = "SelectionMode/Swastika Mode")]
	public class SwastikaMode : SelectionModeData
	{
		public override async void MapSelectedTile(HighlightTile highlightTile)
		{
			var listOfSides = new[] { "innerSideUpward", "innerSideRightward", "outerSideUpward", "outerSideRightward" };
			
			await SharedFunctions.SetupTheProperDimension(highlightTile, listOfSides, IterateTileByDimension);
		}

		public override void DecreaseValue(TileUnit tileUnit)
		{
			tileUnit.SetValue(tileUnit.Number - attackValue);
		}

		private Task IterateTileByDimension(string dimensionType, Vector2 centerPoint, Vector2 tileSpriteSize)
		{
			for (var dimension = -rangeTile; dimension <= rangeTile; dimension++)
			{
				Vector2 tileAtPosition;
				if (dimensionType == "innerSideRightward")
					tileAtPosition = new Vector2(centerPoint.x + dimension * tileSpriteSize.x, centerPoint.y);
				else if (dimensionType == "innerSideUpward")
					tileAtPosition = new Vector2(centerPoint.x, centerPoint.y + dimension * tileSpriteSize.y);
				else if (dimensionType == "outerSideRightward")
				{
					if (dimension < 0)
						tileAtPosition = new Vector2(centerPoint.x + dimension * tileSpriteSize.x, centerPoint.y + rangeTile * tileSpriteSize.y);
					else if (dimension == 0)
						continue;
					else
						tileAtPosition = new Vector2(centerPoint.x + dimension * tileSpriteSize.x, centerPoint.y - rangeTile * tileSpriteSize.y);
				}
				else if (dimensionType == "outerSideUpward")
				{
					if (dimension < 0)
						tileAtPosition = new Vector2(centerPoint.x - rangeTile * tileSpriteSize.x, centerPoint.y + dimension * tileSpriteSize.y);
					else if (dimension == 0)
						continue;
					else
						tileAtPosition = new Vector2(centerPoint.x + rangeTile * tileSpriteSize.x, centerPoint.y + dimension * tileSpriteSize.y);
				}
				else
					tileAtPosition = default(Vector2);

				SharedFunctions.MapHighlightTileAtPosition(tileAtPosition);
			}
			
			return Task.CompletedTask;
		}
	}
	
	/// <summary>
	/// Odin Attribute Processor will update the attribute of selected class dynamically, so we can have certain validation
	/// for each mode that derived from the abstract class.
	/// </summary>
	[UsedImplicitly]
	public class SwastikaModeAttributeProcessor : OdinAttributeProcessor<SwastikaMode>
	{
		public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
		{
			if (member.Name == "rangeTile")
			{
				attributes.Add(new InfoBoxAttribute("You can only set the value from 2", InfoMessageType.Warning));
				attributes.Add(new MinValueAttribute(2));
			}

			if (member.Name == "attackValue")
			{
				attributes.Add(new InfoBoxAttribute("You can only set the value from 1", InfoMessageType.Warning));
				attributes.Add(new MinValueAttribute(1));
			}
		}
	}
}