namespace graphui.node;

public enum Direction {
  UP,
  DOWN,
  LEFT,
  RIGHT,
  NONE
}

public static class DirectionFuncs {
  public static Direction Flip(this Direction dir) {
    return dir switch {
      Direction.LEFT => Direction.RIGHT,
      Direction.RIGHT => Direction.LEFT,
      Direction.DOWN => Direction.UP,
      Direction.UP => Direction.DOWN,
      Direction.NONE => Direction.NONE,
      _ => Direction.UP
    };
  }
}