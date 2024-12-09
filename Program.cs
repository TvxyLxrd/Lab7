using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        // Чтение данных из файла
        var polygon = new List<(double, double)>();
        string[] lines = File.ReadAllLines("input.txt");

        // Заполнение многоугольника
        for (int i = 0; i < lines.Length - 1; i++)
        {
            var coords = lines[i].Split(',');
            double x = double.Parse(coords[0]);
            double y = double.Parse(coords[1]);
            polygon.Add((x, y));
        }

        // Координаты точки
        var pointCoords = lines[lines.Length - 1].Split(',');
        double a_x = double.Parse(pointCoords[0]);
        double a_y = double.Parse(pointCoords[1]);
        var point = (a_x, a_y);

        // Проверка принадлежности точки многоугольнику
        string result = IsPointInPolygon(polygon, point);
        Console.WriteLine($"Результат: {result}");
    }

    static string IsPointInPolygon(List<(double, double)> polygon, (double, double) point)
    {
        double x = point.Item1;
        double y = point.Item2;
        int intersections = 0;
        int n = polygon.Count;

        for (int i = 0; i < n; i++)
        {
            var (x1, y1) = polygon[i];
            var (x2, y2) = polygon[(i + 1) % n];

            // Проверка, пересекает ли луч сторону многоугольника
            if ((y1 > y) != (y2 > y) && (x < (x2 - x1) * (y - y1) / (y2 - y1) + x1))
            {
                intersections++;
            }

            // Проверка, лежит ли точка на границе
            if ((x1 == x && y1 == y) || (x2 == x && y2 == y))
            {
                return "0"; // Точка на границе
            }
        }

        return (intersections % 2 == 1) ? "1" : "-1"; // 1 - внутри, -1 - снаружи
    }
}
