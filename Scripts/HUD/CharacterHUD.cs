using Godot;
using System;

public partial class CharacterHUD : Control
{
	[Export]
	public BaseCharacter Character;
	
	private HealthBar _healthBar;
	
	public override void _Ready()
	{
		if (Character == null) {
			GD.Print("character not loaded");
			return;
		}
		
		_healthBar = GetNode<HealthBar>("HealthBar");
		_healthBar.CurrentHealth = Character.CurrentHealth;
		_healthBar.MaxHealth = Character.MaxHealth;
	}
}
