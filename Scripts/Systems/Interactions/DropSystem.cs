using Godot;
using System;

public partial class DropSystem : Node
{
	public static void SpawnLoot(Node context, Vector2 position, String prefabPath)
	{
		PackedScene item = GD.Load<PackedScene>(prefabPath);
		Node2D itemInstance = item.Instantiate<Node2D>();
		itemInstance.Position = position;
		context.GetParent().aCallDeferred("add_child", itemInstance);
	}
}
