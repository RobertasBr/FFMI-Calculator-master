using System;

namespace FFMI_Calculator

public class User
{
    public string userName { get; set; }
    public int userAge { get; set; }
    public int userHeight { get; set; }
    public double userWeight { get; set; }
    public double userBodyFatPercent { get; set; }

    public User()
    {
        userName = string.Empty;
        userAge = 0;
        userHeight = 0;
        userWeight = 0;
        userBodyFatPercent = 0;
    }
    public double getBodyFat(double bodyWeight, double bodyFatPercent)
    {
        bodyFat = bodyWeight * (bodyFatPercent / 100);
        return bodyFat;
    }
    public double getFatFreeMass(double bodyWeight, double bodyFatPercent)
    {
        bodyFatFreeMass = bodyWeight * (1 - (bodyFatPercent / 100));
        return bodyFatFreeMass;
    }
    public double getFFMI(double bodyFat, double bodyHeight)
    {
        FFMI = bodyFat / (Math.Pow(bodyHeight, 2));
        return FFMI;
    }
}

