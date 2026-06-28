using Godot;
using System;
using System.Reflection.Metadata;
using static Godot.TextServer;

public partial class Player : Node
{
	[Export] public CharacterBody3D PlayerBody3D { set; get; } 
	private Vector3 _playerVelocity {  get; set; } = Vector3.Zero;
	private float _moveFrontValue { get; set; }
	private float _moveBackValue {  get; set; }
	private float _moveLeftValue {  get; set; }
	private float _moveRightValue { get; set; }
    private float _playerMoveForce { get; set; } = 20f;
    private float _acceleration { get; set; } =	80f;

	private Vector2 _mousePosition {  get; set; }

	//states
    private StateMachine stateMachine { get; set; } = new StateMachine();
	private IdleState _idleState;
	private RunState _runState;
	private AirborneState _airborneState;
	private JumpState _jumpState;

	public override void _Ready()
	{
		_moveFrontValue = 0f;
		_moveBackValue = 0f;
		_moveLeftValue = 0f;
        _moveRightValue = 0f;

		_mousePosition = Vector2.Zero;

		_idleState = new IdleState(this);
		_runState = new RunState(this);
		_airborneState = new AirborneState(this);
		_jumpState = new JumpState(this);

        stateMachine.AddState("Idle", _idleState);
        stateMachine.AddState("Run", _runState);
		stateMachine.AddState("Jump", _jumpState);
		stateMachine.AddState("Airborne", _airborneState);
		stateMachine.ChangeState("Idle");

		//handle sloped floor
        this.PlayerBody3D.FloorSnapLength = 0.5f;
        this.PlayerBody3D.FloorMaxAngle = Mathf.DegToRad(45f);
    }
	
	public override void _Process(double delta)
	{
        _moveFrontValue = Input.IsActionPressed("move_forward") ? -1.0f : 0.0f;
        _moveBackValue = Input.IsActionPressed("move_back") ? 1.0f : 0.0f;
        _moveLeftValue = Input.IsActionPressed("move_left")? -1.0f : 0.0f;
        _moveRightValue = Input.IsActionPressed("move_right") ? 1.0f : 0.0f;

		stateMachine.Update(delta);


		// get mouse position
		
	}

    public override void _PhysicsProcess(double delta) {
        stateMachine.PhysicsUpdate(delta);

		//get mousePostion
		_mousePosition = GetViewport().GetMousePosition().Normalized();

		GD.Print(_mousePosition);	
    }

	public override void _Input(InputEvent input) {
		stateMachine.HandleInput(input);
	}

	public float GetFrontValue() {
		return _moveFrontValue;
	}

	public float GetBackValue() {
		return _moveBackValue;
	}

	public float GetLeftValue() {
		return _moveLeftValue;
	}
	public float GetRightValue() {
	return _moveRightValue;
	}
	public Vector3 GetPlayerVeloctiy() {
		return _playerVelocity;
	}

	public float GetPlayerMoveForce() { 
		return _playerMoveForce;
	}

	public StateMachine GetStateMachine() {
		return stateMachine;
	}

	public float GetAcceleration() {
		return _acceleration;
	}

}
                          