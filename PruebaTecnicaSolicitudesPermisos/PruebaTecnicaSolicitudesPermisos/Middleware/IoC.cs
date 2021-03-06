﻿using System;
using DataAccess.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace PruebaTecnicaSolicitudesPermisos.Middleware
{
    public static class IoC
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            //Inyectar los servicios del repositorio génerico:
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
