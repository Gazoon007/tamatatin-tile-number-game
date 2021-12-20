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
	[CreateAssetMenu(menuName = "SelectionMode/Diagonal Mode")]
	public class DiagonalMode : SelectionModeData
	{
		public override async void MapSelectedTile(HighlightTile highlightTile)
		{
			var listOfSides = new[] { "slash", "backslash" };

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
				var tileAtPosition = dimensionType switch
				{
					"slash" => new Vector2(centerPoint.x + dimension * tileSpriteSize.x,
						centerPoint.y + dimension * tileSpriteSize.y),
					"backslash" => new Vector2(centerPoint.x + -dimension * tileSpriteSize.x,
						centerPoint.y + dimension * tileSpriteSize.y),
					_ => default
				};

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
	public class DiagonalModeAttributeProcessor : OdinAttributeProcessor<DiagonalMode>
	{
		public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member, List<Attribute> attributes)
		{
			if (member.Name == "rangeTile")
			{
				attributes.Add(new InfoBoxAttribute("You can only set the value from 1", InfoMessageType.Warning));
				attributes.Add(new MinValueAttribute(1));
			}

			if (member.Name == "attackValue")
			{
				attributes.Add(new InfoBoxAttribute("You can only set the value from 1", InfoMessageType.Warning));
				attributes.Add(new MinValueAttribute(1));
			}
		}
	}
}