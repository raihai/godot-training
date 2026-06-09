using Godot;
using System;

public abstract class BaseState  {
	public virtual void OnEnterState() { }
	public virtual void Update(double delta) { }
	public virtual void PhysicsUpdate(double delta) { }
    public virtual void OnExitState() { }
}
