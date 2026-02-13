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

        public async Task<IEnumerable<Exams>> GetExams()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QueryAsync<Exams>("sp_GetExams", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Orders>> GetOrders()
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            return await connection.QueryAsync<Orders>("sp_GetAllOrders", commandType: CommandType.StoredProcedure);
        }
        public async Task<int> CreateOrder(OrdersDTO request)
        {
         
                using var connection = _dbConnectionFactory.CreateConnection();

                var table = new DataTable();
                table.Columns.Add("ExamId", typeof(int));

                foreach (var id in request.ExamsId)
                {
                    table.Rows.Add(id);
                }

                var parameters = new DynamicParameters();

                parameters.Add("@PatientName", request.PatientName);

                parameters.Add(
                    "@ExamsId",
                    table.AsTableValuedParameter("dbo.ExamIdList")
                );

                parameters.Add("@AttentionDate", request.AttentionDate);

                parameters.Add(
                    "@OrderId",
                    dbType: DbType.Int32,
                    direction: ParameterDirection.Output
                );

                await connection.ExecuteAsync(
                    "sp_CreateOrder",
                    parameters,
                    commandType: CommandType.StoredProcedure
                );

                int orderId = parameters.Get<int>("@OrderId");

                return orderId;
       
        }

        public async Task<IEnumerable<OrdersDetail>> GetOrdersDetail(OrdersDetailDTO request)
        {
            using var connection = _dbConnectionFactory.CreateConnection();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("Id", request.Id);

            var ordersDetail = await connection.QueryAsync<OrdersDetail>("sp_GetAllOrders", parameters, commandType: CommandType.StoredProcedure);

            return ordersDetail;

        }


    }
}
