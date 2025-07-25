using Godot;
using System;

public partial class Wood : Node2D
{
	private AnimationPlayer _woodAnimations;
	
	private ItemData _woodData;
	
	[Export]
	public String DataPath;
	
	public override void _Ready()
	{
		_woodAnimations = GetNode<AnimationPlayer>("WoodAnimations");
		_woodData = GD.Load<ItemData>(DataPath);
	}
}
