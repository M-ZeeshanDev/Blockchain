using System;
using Blockchain.Persistence.Contracts;
using Blockchain.Persistence.Repositories;
using Blockchain.Services;
using Blockchain.Services.Contracts;

namespace Blockchain.Api
{
	public static class RegisterStartupServices
	{
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            //Repositories
            services.AddScoped<IBlockchainRepository, BlockchainRepository>();

            //Services
            services.AddScoped<IBlockchainService, BlockchainService>();
        }
    }
}

