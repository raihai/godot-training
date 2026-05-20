using Godot;
using System;

public partial class FirstCSharpScript : Node
{
	// Called when the node enters the scene tree for the first time.

	float time_elasped;
	float duration;
    public override void _Ready()
	{
		 time_elasped = 0.0f;
		 duration = 5.0f;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) {
		time_elasped += (float)delta;
		if (time_elasped >= duration) {
			
            GetTree().ChangeSceneToFile("res://testGround.tscn");
        }
	}
}
