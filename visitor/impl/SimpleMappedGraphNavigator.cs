using graphui.graph;
using graphui.node;

namespace graphui.visitor.impl;

#nullable enable

public class SimpleMappedGraphNavigator<T> : IDataMappedGraphNavigator<T> where T : notnull {
  private readonly Dictionary<IGraphNode, T> nodes;
  private readonly INodeGraph graph;
  private readonly SimpleGraphNavigator navigator;

  public SimpleMappedGraphNavigator(
    INodeGraph graph,
    Dictionary<T, IGraphNode> nodes
  ) {
    this.nodes = [];
    this.graph = graph;

    foreach (KeyValuePair<T, IGraphNode> pair in nodes) {
      this.nodes.Add(pair.Value, pair.Key);
    }

    navigator = new();
    navigator.Initialize(this.graph);
  }

  public int Count => nodes.Count;

  public bool Step(Direction dir) {
    if (dir == Direction.NONE) {
      return false;
    }

    // oops lol
    navigator.Step(dir);
    return !navigator.RollbackFlag;
  }

  public T? GetActiveData() {
    if (navigator.ActiveNode == null) {
      return default;
    }

    return nodes.GetValueOrDefault(navigator.ActiveNode);
  }

  public T? GetDataAtNode(IGraphNode? node) {
    if (node == null) {
      return default;
    }
    
    return nodes.GetValueOrDefault(node);
  }

  public INodeGraph GetNodeGraph() => graph;
  public IEnumerable<T> GetNodes() => nodes.Values;
}