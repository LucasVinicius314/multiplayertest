using Godot;

public partial class PlayerScript : CharacterBody3D
{
  public override void _EnterTree()
  {
    SetMultiplayerAuthority(int.Parse(Name));
  }

  public override void _Process(double delta)
  {
    if (!IsMultiplayerAuthority())
    {
      GD.Print("a");
      return;
    }

    var input = Input.GetVector("game_left", "game_right", "game_up", "game_down") * 500;

    Velocity = new Vector3
    {
      X = input.X,
      Y = -ProjectSettings.GetSetting("physics/2d/default_gravity").As<int>(),
      Z = input.Y,
    } * (float)delta;

    MoveAndSlide();
  }
}
