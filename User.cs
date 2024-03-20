using System;

namespace FFMI_Calculator
{
    public class User
    {
        public string UserName { get; set; }
        public int UserAge { get; set; }
        public int UserHeight { get; set; }
        public double UserWeight { get; set; }
        public double UserBodyFatPercent { get; set; }

        public User()
        {

        }

        public User(string userName, int userAge, int userHeight, double userWeight, double userBodyFatPercent)
        {
            UserName = userName;
            UserAge = userAge;
            UserHeight = userHeight;
            UserWeight = userWeight;
            UserBodyFatPercent = userBodyFatPercent;
        }

        public double getBodyFat(double bodyWeight, double bodyFatPercent)
        {
            double bodyFat = bodyWeight * (bodyFatPercent / 100);
            return bodyFat;
        }

        public double getFatFreeMass(double bodyWeight, double bodyFatPercent)
        {
            double bodyFatFreeMass = bodyWeight * (1 - (bodyFatPercent / 100));
            return bodyFatFreeMass;
        }

        public double getFFMI(double bodyFat, double bodyHeight)
        {
            double FFMI = bodyFat / (Math.Pow(bodyHeight, 2));
            return FFMI;
        }
    }
}