using Godot;
using System;

public partial class MainHud : CanvasLayer
{
	private CharacterHUD _characterHUD;
	
	[Export]
	public BaseCharacter Character;
	
	[Export]
	private Panel Inventory;
	
	public override void _Ready()
	{
		_characterHUD = GetNode<CharacterHUD>("CharacterHUD");
		_characterHUD.Character = Character;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		GetInteractions();
	}
	
	public void GetInteractions()
	{
		if (Input.IsActionJustPressed("inventory")) {
			Inventory.Visible = !Inventory.Visible;
		}
	}
}
