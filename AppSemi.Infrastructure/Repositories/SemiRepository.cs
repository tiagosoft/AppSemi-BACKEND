using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSemi.Domain.Entities;
using AppSemi.Domain.Interfaces;
using AppSemi.Infrastructure.Data;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace AppSemi.Infrastructure.Repositories
{
    public class SemiRepository : ISemiRepository
    {
        private readonly DbConnectionFactory _dbConnectionFactory;

        public SemiRepository(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Patients>> GetPatients()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QueryAsync<Patients>("sp_GetPatients", commandType: CommandType.StoredProcedure);
        }


    }
}
