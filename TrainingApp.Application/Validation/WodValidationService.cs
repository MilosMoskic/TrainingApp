using TrainingApp.Aplication.Interfaces;

namespace TrainingApp.Aplication.Validation
{
    public class WodValidationService : IWodValidationService
    {
        public bool ValidateWodForm(string dayComboBox, string typeComboBox, string datePicker, string wodTextBox, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(dayComboBox))
            {
                errorMessage = "Please select a day.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(typeComboBox))
            {
                errorMessage = "Please select a type.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(datePicker) || !DateTime.TryParse(datePicker, out _))
            {
                errorMessage = "Please enter a valid date.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(wodTextBox))
            {
                errorMessage = "Please enter the WOD.";
                return false;
            }

            return true;
        }
    }
}
