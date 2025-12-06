using BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App.Views;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using System;

namespace BrokenBrainSoftware.Utilities.ElectronicComponentCatalog.App
{
    /// <summary>
    /// Main application entry point.
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _services;

        public App(IServiceProvider services)
        {
            InitializeComponent();
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            ArgumentNullException.ThrowIfNull(activationState);
            var mainPage = _services.GetRequiredService<MainPage>();
            return new Window(page: mainPage);
        }
    }
}
