using graphui.graph;
using graphui.node;

namespace graphui.visitor;

#nullable enable

public interface IGraphNavigator {
  // do we want to recurse, or what?
  public IGraphNode? ActiveNode { get; }
  public GraphState Initialize(INodeGraph graph);
}