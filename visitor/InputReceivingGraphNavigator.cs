using graphui.node;

namespace graphui.graph;

#nullable enable
public class InputReceivingGraphNavigator<T> : IDataMappedGraphNavigator<T> where T : IInputReceiver {
  private readonly IDataMappedGraphNavigator<T> graph_nav;

  public InputReceivingGraphNavigator(
    IDataMappedGraphNavigator<T> input
  ) {
    graph_nav = input;
  }

  public int Count => graph_nav.Count;

  public bool Step(Direction dir) {
    T? data = graph_nav.GetActiveData();

    if (data == null) {
      // invalid state
      return false;
    }

    if (!data.OnInput(dir)) {
      // input rejected - try to ForceStep
      return ForceStep(dir);
    }

    // move was consumed by the present node
    return true;
  }

  public bool ForceStep(Direction dir) {
    T? data = graph_nav.GetActiveData();
    bool escaped = graph_nav.Step(dir);
    if (escaped) {
      data?.OnExit();
      graph_nav.GetActiveData()!.OnEnter();
    }

    return escaped;
  }

  public void Select() {
    graph_nav.GetActiveData()!.OnSelect();
  }

  public T? GetActiveData() => graph_nav.GetActiveData();
  public T? GetDataAtNode(IGraphNode? node) => graph_nav.GetDataAtNode(node);
  public INodeGraph GetNodeGraph() => graph_nav.GetNodeGraph();
  public IEnumerable<T> GetNodes() => graph_nav.GetNodes();
}