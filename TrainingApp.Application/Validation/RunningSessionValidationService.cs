using TrainingApp.Aplication.Interfaces;

namespace TrainingApp.Aplication.Validation
{
    public class RunningSessionValidationService : IRunningSessionValidationService
    {
        public bool ValidateWodForm(string timeTextBox, string distanceTextBox, string locationTextBoxout, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(timeTextBox))
            {
                errorMessage = "Please enter Time.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(distanceTextBox))
            {
                errorMessage = "Please enter Distance.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(locationTextBoxout))
            {
                errorMessage = "Please enter Location.";
                return false;
            }

            return true;
        }
    }
}
