namespace TrainingApp.Aplication.Interfaces
{
    public interface IWodValidationService
    {
        bool ValidateWodForm(string dayComboBox, string typeComboBox, string datePicker, string wodTextBox, out string errorMessage);
    }
}
