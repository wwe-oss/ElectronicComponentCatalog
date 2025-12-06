using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Services;
using System.Threading.Tasks;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Helpers
{
    public static class NavigationHelper
    {
        private static NavigationService? _navService;

        public static void Initialize(NavigationService navService)
        {
            _navService = navService;
        }

        public static async Task NavigateAsync(string route)
        {
            if (_navService != null)
                await _navService.NavigateToAsync(route);
        }
    }
}
