using System.Numerics;

namespace graphui.node;

#nullable enable

public interface IGraphNode {
  Vector2 Position { get; }

  // connects some target node along directions
  void Connect(IGraphNode? target, Direction dir);
  IGraphNode? Follow(Direction dir);
}