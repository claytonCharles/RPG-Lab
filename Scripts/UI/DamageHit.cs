using Godot;
using System;

public partial class DamageHit : Node2D
{
	private float _floatSpeed = 30f;
	
	private float _lifeTime = 1.2f;
	
	private Label _label;
	
	private float _timeElapsed;
	
	public override void _Ready()
	{
		_label = GetNode<Label>("DamageLabel");
	}
	
	public void SetDamage(string damage, Color color)
	{		
		_label.Text = damage;
		_label.Modulate = color;
	}
	
	public override void _Process(double delta)
	{
		Position += Vector2.Up * _floatSpeed * (float)delta;
		_timeElapsed += (float)delta;
		
		if (_timeElapsed >= _lifeTime) {
			QueueFree();
		}
	}
	
	public static void ShowDamage(Node context, Vector2 position, int damage)
	{
		PackedScene scene = GD.Load<PackedScene>("res://Prefabs/UI/damage_hit.tscn");
		DamageHit sceneInstance = scene.Instantiate<DamageHit>();
		sceneInstance.Position = position;
		context.GetTree().CurrentScene.AddChild(sceneInstance);
		sceneInstance.SetDamage($"-{damage}", Colors.White);
	}
}
