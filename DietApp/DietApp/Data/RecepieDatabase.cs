using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using DietApp.Models;

namespace DietApp.Data
{
    public class RecepieDatabase
    {
       private readonly SQLiteAsyncConnection _database;

        public RecepieDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Recepie>().Wait();
            _database.CreateTableAsync<UserData>().Wait();
        }

        

        public Task<List<Recepie>> GetNotesAsync()
        {
            //Get all notes.
            return _database.Table<Recepie>().ToListAsync();
        }

        public Task<Recepie> GetNoteAsync(int id)
        {
            // Get a specific note.
            return _database.Table<Recepie>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }
        

        public Task<int> SaveNoteAsync(Recepie recepie)
        {
            if (recepie.ID != 0)
            {
                // Update an existing note.
                return _database.UpdateAsync(recepie);
            }
            else
            {
                // Save a new note.
                return _database.InsertAsync(recepie);
            }
        }
        public Task<int> DeleteNoteAsync(Recepie recepie)
        {
            // Delete a note.
            return _database.DeleteAsync(recepie);
        }


        public Task<UserData> GetUserDataAsync()
        {
            return _database.Table<UserData>().FirstOrDefaultAsync();
        }




        //public async Task<UserData> GetSettingAsync()
        //{
        //    return await _database.Table<UserData>().Where(x=>x.ID==1)
        //}

        public void UpdateUserData(UserData userData)
        {
            _database.UpdateAsync(userData);
        }

        public void SaveUserData(UserData userData)
        {
            _database.InsertAsync(userData);
        }

        public Task<int> SaveUserDataAsync(UserData userData)
        {
            if (userData.ID != 0)
            {
                return _database.UpdateAsync(userData);
            }
            else
            {
                return _database.InsertAsync(userData);
            }
        }
    }
}
