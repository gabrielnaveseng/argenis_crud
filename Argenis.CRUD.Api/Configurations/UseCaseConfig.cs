﻿using Microsoft.Extensions.DependencyInjection;
using Argenis.CRUD.Borders.UseCases.Activity;
using Argenis.CRUD.Shared.Configurations;
using Argenis.CRUD.UseCases.Acitivity;

namespace Argenis.CRUD.Configurations
{
    public static class UseCaseConfig
    {
        public static void ConfigureServices(IServiceCollection services, ApplicationConfig applicationConfig)
        {
            services.AddSingleton<IGetActivitiesUseCase, GetActivitiesUseCase>();
        }
    }
}