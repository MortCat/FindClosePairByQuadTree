using System.Collections.Generic;
using System.Windows;

public class QuadTree
{
    public Rect Boundary { get; private set; }
    public List<Point> Points { get; private set; }
    public QuadTree[] Children { get; private set; }

    public QuadTree(Rect boundary)
    {
        Boundary = boundary;
        Points = new List<Point>();
        Children = null;
    }

    // 將點插入QuadTree
    public void Insert(Point point)
    {
        if (!Boundary.Contains(point))
            return;

        if (IsLeafNode())
        {
            Points.Add(point);
            // 範圍容許數量的閥值，超過就分割
            if (Points.Count > 5)
                Subdivide();
        }
        else
        {
            foreach (var child in Children)
            {
                child.Insert(point);
            }
        }
    }

    public bool IsLeafNode()
    {
        return Children == null;
    }

    public void Subdivide()
    {
        double xMid = (Boundary.Left + Boundary.Right) / 2;
        double yMid = (Boundary.Top + Boundary.Bottom) / 2;

        Children = new QuadTree[4];
        Children[0] = new QuadTree(new Rect(xMid, yMid, Boundary.Right, Boundary.Top));
        Children[1] = new QuadTree(new Rect(Boundary.Left, yMid, xMid, Boundary.Top));
        Children[2] = new QuadTree(new Rect(Boundary.Left, Boundary.Bottom, xMid, yMid));
        Children[3] = new QuadTree(new Rect(xMid, Boundary.Bottom, Boundary.Right, yMid));
    }
}
