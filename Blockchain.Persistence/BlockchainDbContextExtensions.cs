using System;
using Blockchain.Entities;
using Blockchain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Blockchain.Persistence
{
	public static class BlockchainDbContextExtensions
	{
        public static bool AreAllMigrationsApplied(this DbContext context)
        {
            IEnumerable<string> applied = context.GetService<IHistoryRepository>()
                .GetAppliedMigrations()
                .Select(m => m.MigrationId);

            IEnumerable<string> total = context.GetService<IMigrationsAssembly>()
                .Migrations
                .Select(m => m.Key);

            return !total.Except(applied).Any();
        }

        public static async Task EnsureSeedData(this BlockchainDbContext context)
        {
            bool areAllMigrationsApplied = context.AreAllMigrationsApplied();
            if (!areAllMigrationsApplied)
            {
                return;
            }

            DateTime utcNow = DateTime.UtcNow;

            await SeedLookupTablesAsync<TransactionStatusEnum, TransactionStatusEntity>(context, utcNow);
        }

        private static async Task SeedLookupTablesAsync<TEnum, TLookupEntity>(BlockchainDbContext context, DateTime utcNow)
            where TLookupEntity : BaseLookupEntity<TEnum>
            where TEnum : struct, Enum
        {
            List<TLookupEntity> existingLookupEntities = await context.Set<TLookupEntity>().ToListAsync();
            List<TLookupEntity> lookupEntitiesToAdd = new List<TLookupEntity>();
            foreach (object enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if ((int)enumValue == 0)
                {
                    continue;
                }
                TLookupEntity lookupEntity = Activator.CreateInstance(typeof(TLookupEntity), new object[] { (int)enumValue, enumValue.ToString() }) as TLookupEntity;
                lookupEntitiesToAdd.Add(lookupEntity);
            }
            await AddLookupEntitiesIfNotExistsAsync<TEnum, TLookupEntity>(context, lookupEntitiesToAdd, existingLookupEntities, utcNow);
        }

        private static async Task AddLookupEntitiesIfNotExistsAsync<TEnum, TLookupEntity>(
                        BlockchainDbContext context,
                        List<TLookupEntity> lookupEntitiesToAdd,
                        List<TLookupEntity> existingLookupEntities,
                        DateTime utcNow)
            where TLookupEntity : BaseLookupEntity<TEnum>
            where TEnum : struct, Enum
        {
            foreach (TLookupEntity lookupEntityToAddOrUpdate in lookupEntitiesToAdd)
            {
                TLookupEntity existingEntity = existingLookupEntities.SingleOrDefault(existingConfirmationType => existingConfirmationType.Id == lookupEntityToAddOrUpdate.Id);
                if (existingEntity == null)
                {
                    lookupEntityToAddOrUpdate.CreatedOn = utcNow;
                    lookupEntityToAddOrUpdate.UpdatedOn = utcNow;
                    await context.AddAsync(lookupEntityToAddOrUpdate);
                }
                else if (existingEntity.Name != lookupEntityToAddOrUpdate.Name)
                {
                    existingEntity.Name = lookupEntityToAddOrUpdate.Name;
                    existingEntity.UpdatedOn = utcNow;
                }
            }
            await context.SaveChangesAsync();
        }
    }
}

