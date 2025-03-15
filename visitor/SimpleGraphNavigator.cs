using graphui.graph;
using graphui.node;
using graphui.node.subgraph;

namespace graphui.visitor;

#nullable enable

// tba: test this out
// - write a builder
public class SimpleGraphNavigator : IGraphNavigator {
  private Stack<SubGraph> GraphStack {
    get => state.GraphStack;
    set => state.GraphStack = value;
  }
  public IGraphNode? ActiveNode {
    get => state.CurrentNode;
    set => state.CurrentNode = value;
  }

  // true if the last step call caused a rollback (ie we stepped into an invalid state)
  private bool rollback_flag = false;
  public bool RollbackFlag => rollback_flag;

  private GraphState state;

  public SimpleGraphNavigator() {
    state = new();
  }

  public GraphState Initialize(INodeGraph graph) {
    state.GraphStack.Clear();
    state.CurrentNode = graph.GetInitialNode();
    
    return state;
  }

  public GraphState Step(Direction dir) {
    // invariant: if we're still within our graph, then active_node is non-null
    rollback_flag = false;
    
    if (ActiveNode == null) {
      // occurs in the event that the provided init graph was empty
      rollback_flag = true;
      return state;
    }

    // how to gauge whether last move was valid?
    // raise a flag on rollback :)
    GraphState rollback_state = new(state);

    ActiveNode = ActiveNode.Follow(dir);

    // if the next node was null, then step out and try to follow the subgraph above it
    while (ActiveNode == null && (GraphStack.Count > 0)) {
      // step failed, so try to go up a stack and do it again
      ActiveNode = GraphStack.Pop().Follow(dir);
    }

    // we either have a next node, or it's null
    // in the former case, check if it's a subgraph
    while (ActiveNode != null && (ActiveNode is SubGraph subgraph)) {
      // push this subgraph
      IGraphNode? node = subgraph.GetInitialNode();
      if (node == null) {
        // subgraph is empty - bail out and just return the subgraph node
        break;
      }

      // update node and push subgraph
      ActiveNode = node;
      GraphStack.Push(subgraph);
    }

    if (ActiveNode == null) {
      // rollback to last valid state
      rollback_flag = true;
      state = rollback_state;
    } else {
      rollback_flag = false;
    }

    return state;
  }
}