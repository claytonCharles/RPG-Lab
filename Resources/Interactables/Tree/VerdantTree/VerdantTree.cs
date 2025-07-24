using Godot;
using System;
using System.Linq;

public partial class VerdantTree : StaticBody2D
{
	private AnimationPlayer _animationObject;
	
	[Export]
	public int Health;
	
	private Boolean _isDead = false;
	
	public override void _Ready()
	{
		_animationObject = GetNode<AnimationPlayer>("AnimationObject");
		String[] listAnimations = _animationObject.GetAnimationList()
			.Where(name => name != "Dead" && name != "REST")
			.ToArray();
	
		Random rnd = new();
		int randomPicker = rnd.Next(listAnimations.Length);
		String animation = listAnimations[randomPicker];
		Health = 3 * randomPicker;
		_animationObject.Play(animation);
	}
	
	public void UpdateHealth(int damage)
	{
		if (_isDead) return;
		Health -= damage;
		DamageHit.ShowDamage(this, GlobalPosition, damage);
		
		if (Health <= 0) {
			_isDead = true;
			QueueFree();
		}
	}
}
