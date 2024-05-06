using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using DietApp.Models;

namespace DietApp.Data
{
    public class RecepieDatabase
    {
        readonly SQLiteAsyncConnection _database;

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


        public Task<List<UserData>> GetUserDataAsync()
        {

                            
            return _database.Table<UserData>().ToListAsync();

        }

        //public async Task<UserData> GetSettingAsync()
        //{
        //    return await _database.Table<UserData>().Where(x=>x.ID==1)
        //}

        public async Task SaveUserDataAsync(UserData userData)
        {
            // Sprawdź, czy istnieje już rekord dla danego Id
            var existingUser = await _database.FindAsync<UserData>(userData.ID);

            if (existingUser != null)
            {
                // Jeśli istnieje, zaktualizuj rekord
                await _database.UpdateAsync(userData);
            }
            else
            {
                // Jeśli nie istnieje, wstaw nowy rekord
                await _database.InsertAsync(userData);
            }
        }


    }
}
