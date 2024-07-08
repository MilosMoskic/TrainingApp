namespace TrainingApp.Aplication.Interfaces
{
    public interface IRunningSessionValidationService
    {
        bool ValidateWodForm(string timeTextBox, string distanceTextBox, string locationTextBoxout, out string errorMessage);
    }
}
