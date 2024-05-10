using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;

namespace AFinalProj
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<RECORDS>().Wait();
        }

        // ADD and UPDATE records
        public async Task<int> Save(string empnum, RECORDS records)
        {
            try
            {
                records.EMPNUM = empnum; // Set the inputted employee number
                return await db.InsertAsync(records);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving record: " + ex.Message);
                return 0; // Return 0 to indicate failure
            }
        }

        // DELETE
        public Task<int> Delete(RECORDS records)
        {
            return db.DeleteAsync(records);
        }

        // DISPLAY ALL or READ ALL 
        public Task<List<RECORDS>> DisplayAll()
        {
            return db.Table<RECORDS>().ToListAsync();
        }

        // SEARCH (specific)
        public Task<RECORDS> Search(string empnum)
        {
            return db.Table<RECORDS>().Where(i => i.EMPNUM == empnum).FirstOrDefaultAsync();
        }
    }
}
