using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        
        Square square = new Square("Red", 5.0);
        Console.WriteLine($"Square Color: {square.GetColor()}, Area: {square.GetArea()}");

        
        Rectangle rectangle = new Rectangle("Blue", 4.0, 6.0);
        Console.WriteLine($"Rectangle Color: {rectangle.GetColor()}, Area: {rectangle.GetArea()}");

        
        Circle circle = new Circle("Green", 3.0);
        Console.WriteLine($"Circle Color: {circle.GetColor()}, Area: {circle.GetArea()}");

        
        List<Shape> shapes = new List<Shape>
        {
            square,
            rectangle,
            circle
        };

        
        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Shape Color: {shape.GetColor()}, Area: {shape.GetArea()}");
        }
    }
}
