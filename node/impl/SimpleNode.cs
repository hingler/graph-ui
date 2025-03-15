using System.Numerics;

namespace graphui.node.impl;

#nullable enable

public class SimpleNode : IGraphNode {
  public Vector2 Position { get; set; }
  private readonly Dictionary<Direction, IGraphNode> nodes = [];

  public SimpleNode() {}

  public void Connect(IGraphNode? target, Direction dir) {
    if (target != null) {
      nodes[dir] = target;
    } else {
      nodes.Remove(dir);
    }
  }

  public IGraphNode? Follow(Direction dir) {
    return nodes.GetValueOrDefault(dir);
  }
}