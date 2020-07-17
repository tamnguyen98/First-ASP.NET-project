using DataLibrary.Db;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Data
{
    public class FoodData : IFoodData
    {
        private IDataAccess _dataAccess;
        private ConnectionStringData _connectionString;

        public FoodData(IDataAccess dataAccess, ConnectionStringData connectionString)
        {
            _dataAccess = dataAccess;
            _connectionString = connectionString;
        }

        public Task<List<FoodModel>> GetFood()
        {
            // Call store procedure and pass in a (or non) parameter and expect a list of FoodModel.
            return _dataAccess.LoadData<FoodModel, dynamic>(
                "dbo.spFood_All",
                new { }, // annonymous obj
                _connectionString.SqlConnectionName);
        }
    }
}
