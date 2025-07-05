using Godot;
using System;

public partial class Menu : Control
{
	private VBoxContainer mainMenu;
	private VBoxContainer aboutMenu;
	private VBoxContainer controlMenu;
	
	public override void _Ready()
	{
		 mainMenu = GetNode<VBoxContainer>("MainMenu");
		 aboutMenu = GetNode<VBoxContainer>("AboutMenu");
		 controlMenu = GetNode<VBoxContainer>("ControlMenu");
	
		GetNode<Button>("MainMenu/ButtonPlay").Pressed += OnButtonPlayPressed;
		GetNode<Button>("MainMenu/ButtonControls").Pressed += OnButtonControlsPressed;
		GetNode<Button>("MainMenu/ButtonAbout").Pressed += OnButtonAboutPressed;
		GetNode<Button>("MainMenu/ButtonLeave").Pressed += OnButtonLeavePressed;
		GetNode<Button>("AboutMenu/ButtonBack").Pressed += OnButtonBackPressed;
		GetNode<Button>("ControlMenu/ButtonBack").Pressed += OnButtonBackPressed;
		
		mainMenu.Visible = true;
		aboutMenu.Visible = false;
		controlMenu.Visible = false;
		
	}

	private void OnButtonPlayPressed()
	{
		GetTree().ChangeSceneToFile("res://mapTwo/scenes/MapTwo.tscn");
	}

	private void OnButtonControlsPressed()
	{
		mainMenu.Visible = false;
		controlMenu.Visible = true;
	}
	
	private void OnButtonAboutPressed()
	{
		mainMenu.Visible = false;
		aboutMenu.Visible = true;
	}
	
	private void OnButtonBackPressed()
	{
		mainMenu.Visible = true;
		controlMenu.Visible = false;
		aboutMenu.Visible = false;
	}

	private void OnButtonLeavePressed()
	{
		GetTree().Quit();
	}
}
