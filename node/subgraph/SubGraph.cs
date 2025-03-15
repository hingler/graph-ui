using System.Numerics;
using graphui.graph;
using graphui.graph.impl;
using graphui.node.impl;

namespace graphui.node.subgraph;

#nullable enable

public interface ISubGraph : INodeGraph, IGraphNode {}

public class SubGraph : ISubGraph {
  private readonly SimpleGraph subgraph;
  private readonly SimpleNode node;

  public Vector2 Position {
    get => node.Position;
    set => node.Position = value;
  }

  public SubGraph() {
    subgraph = new();
    node = new();
  }

  public void Connect(IGraphNode? target, Direction dir) => node.Connect(target, dir);
  public IGraphNode? Follow(Direction dir) => node.Follow(dir);
  public void AddNode(IGraphNode node) => subgraph.AddNode(node);
  public IEnumerable<IGraphNode> GetNodes() => subgraph.GetNodes();
  public IGraphNode? GetInitialNode() => subgraph.GetInitialNode();
  public void SetInitialNode(IGraphNode node) => subgraph.SetInitialNode(node);
}