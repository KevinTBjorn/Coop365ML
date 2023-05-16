using Cooop365ML.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cooop365ML
{
    public static class ServicesManager
    {
        public static MauiAppBuilder UseCustomServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<RoboFlowService>();

            return builder;
        }

        public static MauiAppBuilder UseCustomViews(this MauiAppBuilder builder)
        {


            return builder;
        }

        public static MauiAppBuilder UseCustomViewModels(this MauiAppBuilder builder)
        {


            return builder;
        }
    }
}
