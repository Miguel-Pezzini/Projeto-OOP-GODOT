using Godot;
using System;

public partial class Win : Control
{
	public override void _Ready()
	{
		int totalGemas = Global.GetTotalGemas();
		GD.Print(totalGemas);
		var label = GetNode<Label>("ControlMenu/TotalGemasLabel");
		label.Text = $"Total de gemas coletadas: {totalGemas}";
		
		var buttonBack = GetNode<Button>("ControlMenu/ButtonBack");
		buttonBack.Pressed += OnButtonBackPressed;
	}
	
	private void OnButtonBackPressed()
	{
		Global.gemasPorMapa.Clear();
		GetTree().ChangeSceneToFile("res://menu/scenes/Menu.tscn");
	}
}
