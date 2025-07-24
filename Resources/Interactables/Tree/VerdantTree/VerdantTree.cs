using Godot;
using System;
using System.Linq;

public partial class VerdantTree : StaticBody2D
{
	private AnimationPlayer _animationObject;
	
	public override void _Ready()
	{
		_animationObject = GetNode<AnimationPlayer>("AnimationObject");
		String[] listAnimations = _animationObject.GetAnimationList()
			.Where(name => name != "Dead")
			.ToArray();
			
		Random rnd = new();
		int randomPicker = rnd.Next(listAnimations.Length);
		String animation = listAnimations[randomPicker];
		_animationObject.Play(animation);
	}
}
