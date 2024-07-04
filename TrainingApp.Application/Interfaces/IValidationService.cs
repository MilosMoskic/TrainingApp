namespace TrainingApp.Aplication.Interfaces
{
    public interface IValidationService
    {
        bool ValidateWodForm(string dayComboBox, string typeComboBox, string datePicker, string wodTextBox, out string errorMessage);
    }
}
