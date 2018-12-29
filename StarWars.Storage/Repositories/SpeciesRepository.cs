﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarWars.Storage.Clients;
using StarWars5e.Models;

namespace StarWars.Storage.Repositories
{
    public class SpeciesRepository : ISpeciesRepository
    {
        private readonly ITableStorageConnection _db;

        public SpeciesRepository(ITableStorageConnection db)
        {
            _db = db;
        }


        public async Task InsertSpecies(List<Species> speciesModels, string partitionKey)
        {
            foreach (var species in speciesModels)
            {
                species.PartitionKey = partitionKey;

                if (string.IsNullOrEmpty(species.RowKey))
                {
                    species.RowKey = Guid.NewGuid().ToString();
                }

                await this._db.AddSpecies(species);
            }
        }
    }

    public interface ISpeciesRepository
    {
        Task InsertSpecies(List<Species> speciesModels, string partitionKey);
    }
}