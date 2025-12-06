using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;
using System.Threading.Tasks;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Helpers
{
    /// <summary>
    /// Static helper wrapper for simplified dialog usage.
    /// </summary>
    public static class DialogHelper
    {
        private static DialogService? _dialogService;

        public static void Initialize(DialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public static async Task ShowInfoAsync(string title, string message)
        {
            if (_dialogService != null)
                await _dialogService.ShowMessageAsync(title, message);
        }

        public static async Task<bool> ConfirmAsync(string title, string message)
        {
            return _dialogService != null && await _dialogService.ConfirmAsync(title, message);
        }
    }
}
