using System.ComponentModel;
using graphui.graph;
using graphui.graph.impl;
using graphui.node;
using graphui.node.impl;
using graphui.node.subgraph;
using graphui.visitor.impl;

namespace graphui.builder;

#nullable enable

public class SimpleGraphBuilder<T> where T : notnull {
  private readonly Dictionary<T, IGraphNode> nodes;
  private readonly INodeGraph graph;
  private readonly List<SimpleGraphBuilder<T>> subgraphs;

  public SimpleGraphBuilder() : this(new SimpleGraph()) {}

  public SimpleGraphBuilder(INodeGraph graph) {
    nodes = [];
    this.graph = graph;
    subgraphs = [];
  }

  public void Connect(T from, T to, Direction dir, bool bidirectional = true) {
    IGraphNode from_node = FetchNode(from);
    IGraphNode to_node = FetchNode(to);

    from_node.Connect(to_node, dir);
    if (bidirectional) {
      to_node.Connect(from_node, dir.Flip());
    }
  }

  public void SetInitialNode(T init_node) {
    IGraphNode node = FetchNode(init_node);
    graph.SetInitialNode(node);
  }

  // what functionality do we want
  // for now: not gonna worry about positioning

  public SimpleGraphBuilder<T>? CreateSubGraph(T descriptor) {
    if (nodes.ContainsKey(descriptor)) {
      // builder already exists - return null
      return null;
    }

    SubGraph sg = new();
    nodes[descriptor] = sg;
    SimpleGraphBuilder<T> subgraph = new(sg);

    subgraphs.Add(subgraph);
    return subgraph;
  }

  public IGraphNode FetchNode(T descriptor) {
    if (!nodes.TryGetValue(descriptor, out IGraphNode? value)) {
      value = new SimpleNode();
      graph.AddNode(value);
      nodes[descriptor] = value;
    }

    return value;
  }

  public Dictionary<T, IGraphNode>? GetMappedNodes() {
    Dictionary<T, IGraphNode> node_dict = new(nodes);
    foreach (SimpleGraphBuilder<T> subgraph in subgraphs) {
      Dictionary<T, IGraphNode>? subgraph_dict = subgraph.GetMappedNodes();
      if (subgraph_dict == null) {
        // bubble up
        return null;
      }

      foreach (KeyValuePair<T, IGraphNode> mapping in subgraph_dict) {
        if (!node_dict.TryAdd(mapping.Key, mapping.Value)) {
          // duplicate mapping in a subgraph
          return null;
        }
      }
    }

    return node_dict;
  }

  public IDataMappedGraphNavigator<T>? Build() {
    // collate all nodes from subgraphs
    Dictionary<T, IGraphNode>? mapping = GetMappedNodes();
    if (mapping == null) {
      // invalid
      return null;
    }

    return new SimpleMappedGraphNavigator<T>(graph, mapping);
  }
}