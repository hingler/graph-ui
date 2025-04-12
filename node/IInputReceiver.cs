namespace graphui.node;

public interface IInputReceiver {
  // if true, the input is consumed. if false, the input is bubbled.
  bool OnInput(Direction dir);

  // called when we enter this node
  void OnEnter();

  // called when we exit this node
  void OnExit();

  // called when this node receives a "select" event
  void OnSelect();

  // probably write separate receivers to handle this?

  // - add functionality to activate/deactivate a menu (prob just hide pointer)
  // - write IInputReceiver wrappers for the party view and the char picker
  // - connect via nodes, see how it works!
}