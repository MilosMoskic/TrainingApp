using TrainingApp.Aplication.Interfaces;

namespace TrainingApp.Aplication.Validation
{
    public class RunningSessionValidationService : IRunningSessionValidationService
    {
        public bool ValidateWodForm(string timeTextBox, string distanceTextBox, string locationTextBox, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(timeTextBox))
            {
                errorMessage = "Please enter time.";
                return false;
            }

            if (!decimal.TryParse(timeTextBox, out decimal time) || time <= 0)
            {
                errorMessage = "Please enter a valid time greater than 0.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(distanceTextBox))
            {
                errorMessage = "Please enter distance.";
                return false;
            }

            if (!decimal.TryParse(distanceTextBox, out decimal distance) || distance <= 0)
            {
                errorMessage = "Please enter a valid distance greater than 0.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(locationTextBox))
            {
                errorMessage = "Please enter location.";
                return false;
            }

            if (locationTextBox.Length > 100)
            {
                errorMessage = "Location cannot be longer than 100 characters.";
                return false;
            }

            return true;
        }
    }
}
