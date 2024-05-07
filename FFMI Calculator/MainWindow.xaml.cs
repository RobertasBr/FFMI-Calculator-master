using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using FFMI_Calculator;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FFMI_Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private userDBEntities dbContext;
        public MainWindow()
        {
            InitializeComponent();
            dbContext = new userDBEntities();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get Variables
            string userName = userNameBox.Text;
            int userHeight;
            int userAge;
            double userWeight;
            double userBodyFatPercent;
            // Validate Errors
            if (!int.TryParse(userAgeBox.Text, out userAge))
            {
                MessageBox.Show("Please enter a valid age.");
                return;
            }
            if (!int.TryParse(userHeightBox.Text, out userHeight))
            {
                MessageBox.Show("Please enter height in centimeters");
                return;
            }
            if (!double.TryParse(userWeightBox.Text, out userWeight))
            {
                MessageBox.Show("Please enter your weight in kilograms");
                return;
            }
            if (!double.TryParse(userBodyfatEstimateBox.Text, out userBodyFatPercent))
            {
                MessageBox.Show("Please enter an estimate of your bodyfat percentage");
                return;
            }
            // Setup User
            userIdentity user = new userIdentity();
            user.userName = userName;
            user.userAge = userAge;
            user.userHeight = userHeight;
            user.userWeight = userWeight;

            // Calculate Formulas
            double bodyFat = GetBodyFat();
            double fatFreeMass = GetFatFreeMass();
            double ffmi = GetFFMI();
            double leanWeight = GetLeanWeight();

            double GetBodyFat()
            {
                return userWeight * (userBodyFatPercent / 100);
            }

            double GetFatFreeMass()
            {
                return userWeight - GetBodyFat();
            }

            double GetLeanWeight()
            {
                return userWeight * (1 - (GetBodyFat() / 100));
            }

            double GetFFMI()
            {
                return GetLeanWeight() / Math.Pow(userHeight * 0.01, 2);
            }

            // Interpretation of FFMI
            string interpretation;
            if (ffmi >= 16 && ffmi <= 19)
            {
                interpretation = "Average Man";
            }
            else if (ffmi >= 20 && ffmi <= 21)
            {
                interpretation = "Intermediate";
            }
            else if (ffmi >= 22 && ffmi <= 23)
            {
                interpretation = "Advanced";
            }
            else if (ffmi >= 24 && ffmi <= 26)
            {
                interpretation = "Bodybuilder / Powerlifter / Weightlifter";
            }
            else
            {
                interpretation = "Anomaly Detected";
            }

            // Display Results
            interpretationLabel.Content = $"Index Interpretation: {interpretation}";
            bodyFatLabel.Text = $"Total Body Fat: {bodyFat:F2}%";
            leanWeightLabel.Text = $"Lean Weight: {leanWeight:F2}KG";
            fatFreeMassLabel.Text = $"Fat-Free Mass: {fatFreeMass:F2}kg";
            ffmiLabel.Text = $"FFMI: {ffmi:F2}";
        }

        // Takes all the inputs from the user and saves them to the DB
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            userIdentity user = new userIdentity
            {
                userName = userNameBox.Text,
                userAge = int.Parse(userAgeBox.Text),
                userHeight = int.Parse(userHeightBox.Text),
                userWeight = double.Parse(userWeightBox.Text),
                userBodyFat = int.Parse(userBodyfatEstimateBox.Text)
            };

            dbContext.userIdentities.Add(user);
            dbContext.SaveChanges();
        }
        // Displays the DB results
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            using (var dbContext = new userDBEntities())
            {
                var users = dbContext.userIdentities.ToList();
                userDataGrid.ItemsSource = users;
            }
        }
    }
}