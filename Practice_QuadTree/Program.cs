using System;
using System.Collections.Generic;
using System.Windows;

namespace TrafficControlTest
{
	static class Program
	{
		static void Main()
		{
			List<Point> positions = new List<Point>
			{
				new Point(22,21),
				new Point(23,26),
				new Point(100,20),
				new Point(20,100),
				new Point(85,66),
				new Point(1,3),
				new Point(15,22)
			};

			int minX = 0, minY = 0, maxX = 100, maxY = 100;
			QuadTree quadTree = new QuadTree(new Rect(minX, minY, maxX, maxY)); // 請根據你的實際情況提供邊界範圍
			foreach (var point in positions)
			{
				quadTree.Insert(point);
			}

			List<(Point, Point)> closePair = FindClosePairs(quadTree);
			foreach (var branch in closePair)
			{
				Console.WriteLine($"Point1: {branch.Item1.X},{branch.Item1.Y} and Point2: {branch.Item2.X},{branch.Item2.Y} ");
			}
			Console.ReadKey();



		}
		private static List<(Point, Point)> FindClosePairs(QuadTree quadTree)
		{
			List<(Point, Point)> closePair = new List<(Point, Point)>();
			if (quadTree.IsLeafNode())
			{
				for (int i = 0; i < quadTree.Points.Count; i++)
				{
					for (int j = 0; j < quadTree.Points.Count; j++)
					{
						double distance = CalculateDistance(quadTree.Points[i], quadTree.Points[j]);
						if (distance < 10)
						{
							closePair.Add((quadTree.Points[i], quadTree.Points[j]));
						}

					}
				}
			}

			return closePair;
		}
		private static int CalculateDistance(Point p1, Point p2)
		{
			return (int)Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2));
		}

	}
}
