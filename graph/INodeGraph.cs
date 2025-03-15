using graphui.node;

namespace graphui.graph;

#nullable enable

public interface INodeGraph {
  void AddNode(IGraphNode node);
  IEnumerable<IGraphNode> GetNodes();
  void SetInitialNode(IGraphNode node);
  IGraphNode? GetInitialNode();

  // given some prev position, give us the node which is closest to it, in the provided direction
  // projections out to global space
  // treat (0, 0) as center for now
}