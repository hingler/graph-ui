using graphui.graph;
using graphui.node;
using graphui.node.subgraph;

namespace graphui.visitor;

#nullable enable

public class GraphState {
  public IGraphNode? CurrentNode;
  public Stack<SubGraph> GraphStack;

  public GraphState() {
    CurrentNode = null;
    GraphStack = new();
  }
  public GraphState(GraphState other) {
    CurrentNode = other.CurrentNode;
    GraphStack = new(other.GraphStack);
  }
}