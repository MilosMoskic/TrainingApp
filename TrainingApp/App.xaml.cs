﻿using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TrainingApp.Aplication.Interfaces;
using TrainingApp.Aplication.Services;
using TrainingApp.Domain.Interfaces;
using TrainingApp.Infastructure.Repositories;
using Application = System.Windows.Application;
using Microsoft.EntityFrameworkCore;
using TrainingApp.Infastructure.Context;
using TrainingApp.Aplication.Validation;

namespace TrainingApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<Dashboard>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TrainingContext>(options => 

                options.UseSqlServer("Server=DESKTOP-ID96S2G\\SQLEXPRESS;Database=TrainingApp;Trusted_Connection=True;Encrypt=false;TrustServerCertificate=true;"));
            
            services.AddScoped<IWodRepository, WodRepository>();
            services.AddScoped<IWodService, WodService>();
            services.AddScoped<IRunningSessionRepository, RunningSessionRepository>();
            services.AddScoped<IRunningSessionService, RunningSessionService>();
            services.AddScoped<IWodValidationService, WodValidationService>();
            services.AddScoped<IRunningSessionValidationService, RunningSessionValidationService>();
            services.AddScoped<INutritionValidationService, NutritionValidationService>();
            services.AddScoped<IWeightRepository, WeightRepository>();
            services.AddScoped<IWeightService, WeightService>();
            services.AddScoped<IStreakRepository, StreakRepository>();
            services.AddScoped<IStreakService, StreakService>();
            services.AddScoped<INutritionService, NutritionService>();
            services.AddScoped<INutritionRepository, NutritionRepository>();
            services.AddTransient<Dashboard>();
            services.AddTransient<AddWodPage>();
        }
    }
}
