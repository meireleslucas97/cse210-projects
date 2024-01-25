using System;

class Fraction
{
    private int numerator;
    private int denominator;

    
    public Fraction()
    {
        numerator = 1;
        denominator = 1;
    }

    public Fraction(int top)
    {
        numerator = top;
        denominator = 1;
    }

    public Fraction(int top, int bottom)
    {
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }

        numerator = top;
        denominator = bottom;
    }

    
    public int GetNumerator()
    {
        return numerator;
    }

    public void SetNumerator(int top)
    {
        numerator = top;
    }

    public int GetDenominator()
    {
        return denominator;
    }

    public void SetDenominator(int bottom)
    {
        if (bottom == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.");
        }

        denominator = bottom;
    }

    
    public string GetFractionString()
    {
        return $"{numerator}/{denominator}";
    }

    public double GetDecimalValue()
    {
        return (double)numerator / denominator;
    }
}

class Program
{
    static void Main()
    {
        
        Fraction fraction1 = new Fraction();      
        Fraction fraction2 = new Fraction(6);     
        Fraction fraction3 = new Fraction(6, 7);  

        
        fraction1.SetNumerator(3);
        fraction1.SetDenominator(4);

        Console.WriteLine($"Fraction 1: {fraction1.GetFractionString()}"); 
        Console.WriteLine($"Decimal Value of Fraction 1: {fraction1.GetDecimalValue()}"); 

        Console.WriteLine($"Fraction 2: {fraction2.GetFractionString()}"); 
        Console.WriteLine($"Decimal Value of Fraction 2: {fraction2.GetDecimalValue()}"); 

        Console.WriteLine($"Fraction 3: {fraction3.GetFractionString()}"); 
        Console.WriteLine($"Decimal Value of Fraction 3: {fraction3.GetDecimalValue()}"); 

        Console.ReadLine();
    }
}
