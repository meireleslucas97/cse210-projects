using System;
using System.Collections.Generic;

public class Activity
{
    private DateTime date;
    protected int durationInMinutes;

    public Activity(DateTime date, int durationInMinutes)
    {
        this.date = date;
        this.durationInMinutes = durationInMinutes;
    }

    public virtual double GetDistance()
    {
        return 0.0;
    }

    public virtual double GetSpeed()
    {
        return 0.0;
    }

    public virtual double GetPace()
    {
        return 0.0;
    }

    public virtual string GetSummary()
    {
        return $"{date:dd/MM/yyyy} - {GetType().Name} ({durationInMinutes} min)";
    }
}

public class Running : Activity
{
    private double distance;

    public Running(DateTime date, int durationInMinutes, double distance) : base(date, durationInMinutes)
    {
        this.distance = distance;
    }

    public override double GetDistance()
    {
        return distance;
    }

    public override double GetSpeed()
    {
        return distance > 0 ? (distance / durationInMinutes) * 60 : 0.0;
    }

    public override double GetPace()
    {
        return distance > 0 ? durationInMinutes / distance : 0.0;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distância {distance} milhas, Velocidade {GetSpeed()} mph, Ritmo: {GetPace()} min por milha";
    }
}

public class Cycling : Activity
{
    private double speed;

    public Cycling(DateTime date, int durationInMinutes, double speed) : base(date, durationInMinutes)
    {
        this.speed = speed;
    }

    public override double GetSpeed()
    {
        return speed;
    }

    public override double GetPace()
    {
        return speed > 0 ? 60 / speed : 0.0;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Velocidade {speed} km/h, Ritmo: {GetPace()} min por km";
    }
}

public class Swimming : Activity
{
    private int laps;

    public Swimming(DateTime date, int durationInMinutes, int laps) : base(date, durationInMinutes)
    {
        this.laps = laps;
    }

    public override double GetDistance()
    {
        return laps * 50 / 1000.0;
    }

    public override double GetSpeed()
    {
        return GetDistance() > 0 ? (GetDistance() / durationInMinutes) * 60 : 0.0;
    }

    public override double GetPace()
    {
        return GetDistance() > 0 ? durationInMinutes / GetDistance() : 0.0;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distância {GetDistance()} km, Velocidade {GetSpeed()} km/h, Ritmo: {GetPace()} min por km";
    }
}

class Program
{
    static void Main()
    {
        List<Activity> activities = new List<Activity>();

        activities.Add(new Running(new DateTime(2022, 11, 3), 30, 3.0));
        activities.Add(new Cycling(new DateTime(2022, 11, 3), 30, 15.0));
        activities.Add(new Swimming(new DateTime(2022, 11, 3), 30, 10));

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
