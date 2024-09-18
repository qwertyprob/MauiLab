using Microsoft.Maui;

namespace Lab1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        
        public double CalculateBMI(double weight, double height)
        {
            if (height <= 0)
                throw new ArgumentException("Height must be greater than zero.");

            return weight / (height * height);
        }

       
        void OnCalculateClicked(object sender, EventArgs e)
        {
            try
            {
                
                double weight = double.Parse(weightEntry.Text);
                double height = double.Parse(heightEntry.Text);

                
                double bmi = CalculateBMI(weight, height);

                
                bmiLabel.Text = $"ИМТ: {bmi:F2}";
            }
            catch (Exception ex)
            {
                
                bmiLabel.Text = "Ошибка в расчёте ИМТ: Введите правильные числа";
            }
        }
    }


}
