using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services
{
    /// <summary>
    /// Displays modal dialogs and alerts.
    /// </summary>
    public class DialogService
    {
        public async Task ShowMessageAsync(string title, string message)
        {
            await Application.Current!.MainPage!.DisplayAlertAsync(title, message, "OK");
        }

        public async Task<bool> ConfirmAsync(string title, string message)
        {
            return await Application.Current!.MainPage!.DisplayAlertAsync(title, message, "Yes", "No");
        }
    }
}
