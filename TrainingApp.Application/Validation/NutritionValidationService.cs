using TrainingApp.Aplication.Interfaces;

namespace TrainingApp.Aplication.Validation
{
    public class NutritionValidationService : INutritionValidationService
    {
        public bool ValidateWodForm(string ageTextBox, string heightTextBox, string activityLevelComboBox, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(ageTextBox))
            {
                errorMessage = "Please enter age.";
                return false;
            }

            if (!int.TryParse(ageTextBox, out int age) || age < 1 || age > 120)
            {
                errorMessage = "Please enter a valid age between 1 and 120.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(heightTextBox))
            {
                errorMessage = "Please enter height.";
                return false;
            }

            if (!decimal.TryParse(heightTextBox, out decimal height) || height < 1 || height > 300)
            {
                errorMessage = "Please enter a valid height between 1 and 300.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(activityLevelComboBox))
            {
                errorMessage = "Please select an activity level.";
                return false;
            }

            return true;
        }
    }
}