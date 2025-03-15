namespace graphui.node;

public interface IInputReceiver {
  // if true, the input is consumed. if false, the input is bubbled.
  bool OnInput(Direction dir);

  // called when we enter this node
  void OnEnter();

  // called when we exit this node
  void OnExit();
}