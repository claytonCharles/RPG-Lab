using Godot;
using System;

public partial class BaseCharacter : CharacterBody2D
{

	private AnimationPlayer _characterAnimations;
	
	private Sprite2D _characterSprite;
	
	private enum Positions {
		Up,
		Down,
		Side
	};
	
	private Positions _lastPosition = Positions.Side;
	
	[ExportGroup("Attributes")]
	[Export]
	public int Speed { get; set; } = 200;
	
	public override void _Ready()
	{
		_characterSprite = GetNode<Sprite2D>("Texture");
		_characterAnimations = GetNode<AnimationPlayer>("CharacterAnimation");
	}
	
	public void GetInput()
	{
		Vector2 directions = Input.GetVector("mvLeft", "mvRight", "mvUp", "mvDown");
		Velocity = directions * Speed;
	}
	
	public void SetAnimations()
	{
		if (Velocity.Y != 0) {
			String animation = Velocity.Y > 0 ? "RunDown" : "RunUp";
			_lastPosition = Velocity.Y > 0 ? Positions.Down : Positions.Up;
			_characterAnimations.Play(animation);
			return;
		}
		
		if (Velocity.X != 0) {
			_lastPosition = Positions.Side;
			_characterSprite.FlipH = Velocity.X < 0;
			_characterAnimations.Play("RunSide");
			return;
		}
		
		if (_lastPosition != Positions.Side) {
			String idlePosition = _lastPosition == Positions.Up ? "IdleUp" : "IdleDown";
			_characterAnimations.Play(idlePosition);
			return;
		}
		
		_characterAnimations.Play("IdleSide");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
		SetAnimations();
	}
}
