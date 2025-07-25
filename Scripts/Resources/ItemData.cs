using Godot;
using System;

[GlobalClass]
public partial class ItemData : Resource
{
	[Export]
	public string id = "";
	
	[Export]
	public string Name = "";
	
	[Export]
	public string Description = "";
	
	[Export]
	public int Value = 0;
}
