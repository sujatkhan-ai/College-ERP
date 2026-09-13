using Microsoft.EntityFrameworkCore;
using CRT_Data.Interface;
using CRT_Data;
using CRT_Core.Interface;
using CRT_Core;
using Microsoft.Extensions.DependencyInjection;

namespace ChayanikaRecruitTech.Models
{
    internal static class ServiceDependentInjection
    {
        public static void ServiceDependent(IServiceCollection services)
        {
            services.AddScoped(typeof(IRecords), typeof(Records));
            services.AddScoped(typeof(ICoreRecords), typeof(CoreRecords));
        }
    }
}
