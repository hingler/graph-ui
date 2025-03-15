using graphui.graph;
using graphui.node;

namespace graphui.graph;

#nullable enable

public interface IDataMappedGraphNavigator<T> where T : notnull {
  public bool Step(Direction dir);
  public T? GetActiveData();

  public int Count { get; }

  // returns underlying node graph
  public INodeGraph GetNodeGraph();
  public IEnumerable<T> GetNodes();
}