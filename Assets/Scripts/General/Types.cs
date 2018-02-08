public class Vector2I {

	public int x;
	public int y;

	public Vector2I() {
		x = y = 0;
	}

	public Vector2I(int x_, int y_) {
		x = x_;
		y = y_;
	}

	public Vector2I(Vector2I copy) {
		x = copy.x;
		y = copy.y;
	}

	public static Vector2I operator+ (Vector2I first, Vector2I second) {
		int nX = first.x + second.x;
		int nY = first.y + second.y;
		return new Vector2I (nX, nY);
	}

	public static Vector2I operator- (Vector2I first, Vector2I second) {
		return first + new Vector2I (-second.x, -second.y);
	}

	override public string ToString() {
		return "x: " + x.ToString () + " y: " + y.ToString ();	
	}
}
