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

            if (string.IsNullOrWhiteSpace(heightTextBox))
            {
                errorMessage = "Please enter Height.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(activityLevelComboBox))
            {
                errorMessage = "Please select a Activity Level.";
                return false;
            }

            return true;
        }
    }
}
