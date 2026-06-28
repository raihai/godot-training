using Godot;
using System;

public partial class CameraSpringArm : Node3D
{

	private float _mouseSensibility = 0.005f;

    [Export(PropertyHint.Range, "-90,0,0.1,radians_as_degrees")]
     public float _minVerticalAngle = -MathF.PI / 2.0f;
    [Export(PropertyHint.Range, "0,90,0.1,radians_as_degrees")]
    public float _maxVerticalAngle = MathF.PI / 4.0f;

   
    [Export] public SpringArm3D SpringArm { set; get; }

    public override void _Ready()
	{
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}


	public override void _Process(double delta)
	{
	}

    public override void _UnhandledInput(InputEvent @event) {
        if (@event is InputEventMouseMotion mouseEvent) {

            float newRotationY = Rotation.Y - mouseEvent.Relative.X * _mouseSensibility;
            float newRotationX = Rotation.X - mouseEvent.Relative.Y * _mouseSensibility;

            newRotationY = Mathf.Clamp(newRotationY, 0.0f, MathF.Tau);
            newRotationX = Mathf.Clamp(newRotationX, _minVerticalAngle, _maxVerticalAngle);

            Rotation = new Vector3(newRotationX, newRotationY, Rotation.Z);


        }
    }
}
