namespace TrainingApp.Aplication.Interfaces
{
    public interface INutritionValidationService
    {
        bool ValidateWodForm(string ageTextBox, string heightTextBox, string activityLevelComboBox, out string errorMessage);
    }
}
