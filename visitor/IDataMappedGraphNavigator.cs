using graphui.graph;
using graphui.node;

namespace graphui.graph;

#nullable enable

public interface IDataMappedGraphNavigator<T> where T : notnull {
  // returns true if state changed - else, false
  public bool Step(Direction dir);
  public T? GetActiveData();
  public T? GetDataAtNode(IGraphNode? node);

  public int Count { get; }

  // returns underlying node graph
  public INodeGraph GetNodeGraph();
  public IEnumerable<T> GetNodes();
}