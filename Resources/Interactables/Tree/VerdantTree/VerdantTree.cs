using Godot;
using System;
using System.Linq;

public partial class VerdantTree : StaticBody2D
{
	private AnimationPlayer _animationObject;
	
	private Boolean _isDead = false;
	
	private String _prefabWood = "res://Prefabs/Items/wood.tscn";
	
	[Export]
	public int Health;
	
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
			DropSystem.SpawnLoot(this, GlobalPosition, _prefabWood);
			QueueFree();
		}
	}
}
