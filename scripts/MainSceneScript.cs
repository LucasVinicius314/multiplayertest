using Godot;

public partial class MainSceneScript : Node3D
{
  readonly ENetMultiplayerPeer peer = new ENetMultiplayerPeer();

  readonly PackedScene playerPrefab = GD.Load<PackedScene>("res://prefabs/player.tscn");

  void AddPlayer(long id)
  {
    var player = playerPrefab.Instantiate();
    player.Name = id.ToString();

    CallDeferred(Node.MethodName.AddChild, player);
  }

  void Host()
  {
    peer.CreateServer(5001);
    Multiplayer.MultiplayerPeer = peer;

    Multiplayer.PeerConnected += (id) =>
    {
      AddPlayer(id);
    };

    AddPlayer(1);
  }

  void Join()
  {
    peer.CreateClient("localhost", 5001);
    Multiplayer.MultiplayerPeer = peer;
  }

  public override void _Input(InputEvent @event)
  {
    if (@event is InputEventKey eventKey && eventKey.Pressed)
    {
      switch (eventKey.Keycode)
      {
        case Key.H:
          Host();
          break;
        case Key.J:
          Join();
          break;
      }
    }
  }
}
