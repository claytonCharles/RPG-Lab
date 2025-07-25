using Godot;
using System;

public partial class HealthBar : ProgressBar
{
	[Export]
	public int CurrentHealth {get; set;}
	
	[Export]
	public int MaxHealth;
	
	private ProgressBar _damageBar;
	
	private Timer _timerDamage;
	
	public override void _Ready()
	{
		this.MaxValue = MaxHealth;
		this.Value = CurrentHealth;
	}
	
	public void UpdateHealth(int damage)
	{
		this.Value -= damage;
		GD.Print(damage);
	}
}
