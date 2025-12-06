using Microsoft.Maui.Controls;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services
{
    /// <summary>
    /// Displays modal dialogs and alerts.
    /// </summary>
    public class DialogService
    {
        private Page RequireActivePage()
        {
            var page = Application.Current?.Windows?.FirstOrDefault()?.Page;
            if (page == null)
                throw new InvalidOperationException("No active window is available for dialog presentation.");

            return page;
        }

        public async Task ShowMessageAsync(string title, string message)
        {
            await RequireActivePage().DisplayAlertAsync(title, message, "OK");
        }

        public async Task<bool> ConfirmAsync(string title, string message)
        {
            return await RequireActivePage().DisplayAlertAsync(title, message, "Yes", "No");
        }
    }
}
