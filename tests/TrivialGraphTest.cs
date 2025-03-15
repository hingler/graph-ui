using graphui.builder;
using graphui.graph;
using graphui.node;
using graphui.visitor.impl;

namespace graphui.test;

public class TrivialGraphTest {
  [Test]
  public void TestTrivialGraph() {
    // works!!
    SimpleGraphBuilder<int> builder = new();

    /// 1-2
    /// | |
    /// 3-5

    builder.Connect(1, 2, Direction.RIGHT);
    builder.Connect(2, 5, Direction.DOWN);
    builder.Connect(1, 3, Direction.DOWN);
    builder.Connect(3, 5, Direction.RIGHT);

    builder.SetInitialNode(1);

    IDataMappedGraphNavigator<int> graph = builder.Build()!;

    Assert.That(graph.Count, Is.EqualTo(4));

    Assert.That(graph.GetActiveData(), Is.EqualTo(1));

    graph.Step(Direction.DOWN);
    Assert.That(graph.GetActiveData(), Is.EqualTo(3));

    graph.Step(Direction.UP);
    graph.Step(Direction.RIGHT);
    Assert.That(graph.GetActiveData(), Is.EqualTo(2));

    graph.Step(Direction.LEFT);
    graph.Step(Direction.DOWN);
    graph.Step(Direction.RIGHT);

    Assert.That(graph.GetActiveData(), Is.EqualTo(5));

    // quick subgraph test
  }
  
  [Test]
  public void TestSubGraph() {
    SimpleGraphBuilder<int> builder = new();
    SimpleGraphBuilder<int> subgraph = builder.CreateSubGraph(2)!;

    builder.Connect(1, 2, Direction.RIGHT);
    builder.SetInitialNode(1);
    
    subgraph.SetInitialNode(5);
    subgraph.Connect(5, 3, Direction.DOWN);
    subgraph.Connect(3, 16, Direction.DOWN);

    /// 1 -- (5)
    ///       |
    ///       |
    ///       (3)
    ///       |
    ///       |
    ///       (16)
    IDataMappedGraphNavigator<int> graph = builder.Build()!;

    Assert.That(graph.GetActiveData(), Is.EqualTo(1));

    graph.Step(Direction.RIGHT);
    Assert.That(graph.GetActiveData(), Is.EqualTo(5));

    graph.Step(Direction.DOWN);
    graph.Step(Direction.DOWN);

    Assert.That(graph.GetActiveData(), Is.EqualTo(16));
    
    graph.Step(Direction.LEFT);
    Assert.That(graph.GetActiveData(), Is.EqualTo(1));
  }
}