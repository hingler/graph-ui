using graphui.graph;
using graphui.node;

namespace digipet.nav;

#nullable enable

public class GraphNavReceiver<T> : IInputReceiver, IDataMappedGraphNavigator<T> where T : notnull {
  private readonly IDataMappedGraphNavigator<T> data;

  public GraphNavReceiver(
    IDataMappedGraphNavigator<T> input
  ) {
    data = input;
  }

  public bool OnInput(Direction dir) {
    return data.Step(dir);
  }

  public void OnExit() {
    // maintain state but don't do anything
  }

  public void OnEnter() {
    // same deal - maintain state, don't change
  }

  public void OnSelect() {}

  public bool Step(Direction dir) => data.Step(dir);
  public T? GetActiveData() => data.GetActiveData();
  public T? GetDataAtNode(IGraphNode? node) => data.GetDataAtNode(node);
  public int Count => data.Count;
  public INodeGraph GetNodeGraph() => data.GetNodeGraph();
  public IEnumerable<T> GetNodes() => data.GetNodes();
}