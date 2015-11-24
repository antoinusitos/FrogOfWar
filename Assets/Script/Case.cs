using UnityEngine;
using System.Collections;

public class Case : MonoBehaviour {

    public int _x;
    public int _y;
    private int _index;
    private bool _occcupe;

    public struct coordonees
    {
        public int _x;
        public int _y;
        public int _index;
        public bool _occcupe;
        public coordonees(int X, int Y, int Index, bool occ)
        {
            _x = X;
            _y = Y;
            _index = Index;
            _occcupe = occ;
        }
    };

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    public void Init(int X, int Y, int Index)
    {
        _x = X;
        _y = Y;
        _index = Index;
        _occcupe = false;
    }

    public coordonees GetCoordonnees()
    {
        return new coordonees(_x, _y, _index, _occcupe);
    }

    public void SetOccupe(bool newState)
    {
        _occcupe = newState;
    }
}
