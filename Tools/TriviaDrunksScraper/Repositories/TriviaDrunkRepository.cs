using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using TriviaDrunksScraper.DAOs;

namespace TriviaDrunksScraper.Repositories
{
    public class TriviaDrunkRepository
    {
        private IConfiguration _config;

        private string conn { get => _config.GetConnectionString("DefaultConnection"); }

        public TriviaDrunkRepository(IConfiguration config)
        {
            _config = config;
        }
        public async void UpdateHappyHours(IEnumerable<string> happyHourDesc, int cityId)
        {
            try
            {
                var dayOfWeekId = new DayOfWeek();
                var testing = (int)dayOfWeekId;
                //Need to get the name of the bar with the descriptions and not completely parsed out like they are now
                //List<HappyHourDAO> happyHoursToUpdate = happyHourDesc.Select(desc => new HappyHourDAO { BarId });
                using (IDbConnection db = new SqlConnection(conn))
                {

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async void UpdateBars(IEnumerable<string> barNames, int cityId)
        {
            try
            {
                List<BarDAO> barsToUpdate = barNames.Select(bar => new BarDAO { Name = bar, CityId = cityId }).ToList();

                using (IDbConnection db = new SqlConnection(conn))
                {
                    var updateBars = @"MERGE Bar As Target
                                       USING (VALUES(@Name, @CityId)) As Source(Name, CityId)
                                       ON Target.Name = Source.Name AND Target.CityId = Source.CityId
                                       WHEN NOT MATCHED BY TARGET
                                            THEN 
                                       INSERT (Name, CityId)
                                       VALUES (Source.Name, Source.CityId);";

                    var success = await db.ExecuteAsync(updateBars, barsToUpdate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
