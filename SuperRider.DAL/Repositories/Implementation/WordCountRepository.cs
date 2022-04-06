using SuperRider.DAL.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using SuperRider.DAL.Repositories.Interfaces;

namespace SuperRider.DAL.Repositories.Implemantation
{
    public class WordCountRepository : IWordCountRepository
    {
        private const string _wordCountUpdateProcedure = "[dbo].[UpdateWords]";
        private const string _words_CountType = "[dbo].[Words_Count_Type]";
        protected IDbConnection _connection;

        public WordCountRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task UpdateTableAsync(List<WordCountDto> words)
        {
            var table = new DataTable();
            table.Columns.Add("Word");
            table.Columns.Add("Count");

            foreach (var word in words)
            {
                table.Rows.Add(word.Word, word.Count);
            }

            await _connection
                .ExecuteScalarAsync(
                _wordCountUpdateProcedure,
                new { tblWords_Count = table.AsTableValuedParameter(_words_CountType) },
                commandType: CommandType.StoredProcedure
                );
        }
    }
}