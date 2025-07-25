using Godot;
using System;

public partial class BaseCharacter : CharacterBody2D
{

	private AnimationPlayer _characterAnimations;
	
	private Sprite2D _characterSprite;
	
	private Boolean _canAction = true;
	
	private string _animationAction = "";
	
	private enum Positions {
		Up,
		Down,
		Side
	};
	
	private Positions _lastPosition = Positions.Side;
	
	[Export]
	public CollisionShape2D CollisionShape;
	
	[ExportGroup("Status")]
	[Export]
	public int Speed { get; private set; } = 200;
	
	[Export]	
	public int MaxHealth { get; private set; } = 100;
	
	[Export]	
	public int CurrentHealth { get; private set; } = 100;
	
	public override void _Ready()
	{
		_characterSprite = GetNode<Sprite2D>("Texture");
		_characterAnimations = GetNode<AnimationPlayer>("CharacterAnimation");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
		GetInteractions();
		SetMovimentAnimations();
	}
	
	public void GetInput()
	{
		Vector2 directions = Input.GetVector("mvLeft", "mvRight", "mvUp", "mvDown");
		Velocity = directions * Speed;
	}
	
	public void GetInteractions()
	{
		if (Input.IsActionJustPressed("interact") && _canAction) {
			_canAction = false;
			_animationAction = "AxeTool";
		}
	}
	
	public void SetMovimentAnimations()
	{
		if (Velocity.X != 0) {
			_characterSprite.FlipH = Velocity.X < 0;
			int newPositionX = Velocity.X < 0 ? -21 : 21;
			CollisionShape.Position = new Vector2(newPositionX, CollisionShape.Position.Y);
		}
		
		if (!_canAction) {
			SetPhysicsProcess(false);
			_characterAnimations.Play(_animationAction);
			return;
		}
		
		if (Velocity.Y != 0) {
			string animation = Velocity.Y > 0 ? "RunDown" : "RunUp";
			_lastPosition = Velocity.Y > 0 ? Positions.Down : Positions.Up;
			_characterAnimations.Play(animation);
			return;
		}
		
		if (Velocity.X != 0) {
			_lastPosition = Positions.Side;
			_characterAnimations.Play("RunSide");
			return;
		}
		
		if (_lastPosition != Positions.Side) {
			string idlePosition = _lastPosition == Positions.Up ? "IdleUp" : "IdleDown";
			_characterAnimations.Play(idlePosition);
			return;
		}
		
		_characterAnimations.Play("IdleSide");
	}
	
	public void OnAnimationFinished(string animationName) 
	{
		if (animationName == _animationAction) {
			SetPhysicsProcess(true);
			_canAction = true;
			
			if (_lastPosition != Positions.Side) {
				string idlePosition = _lastPosition == Positions.Up ? "IdleUp" : "IdleDown";
				_characterAnimations.Play(idlePosition);
				return;
			}
			
			_characterAnimations.Play("IdleSide");
		}
	}
	
	public void OnInteractionsBodyEntered(Node2D body)
	{
		if (body is VerdantTree tree) {
			Random rnd = new();
			int damage = rnd.Next(1, 5);
			tree.UpdateHealth(damage);
		}
	}
}
