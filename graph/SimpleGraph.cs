using graphui.node;

namespace graphui.graph.impl;

#nullable enable

public class SimpleGraph : INodeGraph {
  private readonly HashSet<IGraphNode> nodes = [];
  private IGraphNode? firstNode = null;
  public void AddNode(IGraphNode node) {
    nodes.Add(node);
    firstNode ??= node;
  }

  public void SetInitialNode(IGraphNode node) {
    if (nodes.Contains(node)) {
      firstNode = node;
    }
  }

  public IEnumerable<IGraphNode> GetNodes() {
    return nodes;
  }

  public IGraphNode? GetInitialNode() {
    return firstNode ?? nodes.FirstOrDefault();
  }
}