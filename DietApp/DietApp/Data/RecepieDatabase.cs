using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using DietApp.Models;

namespace DietApp.Data
{
    public class RecepieDatabase
    {
        readonly SQLiteAsyncConnection database;

        public RecepieDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Recepie>().Wait();
        }

        public Task<List<Recepie>> GetNotesAsync()
        {
            //Get all notes.
            return database.Table<Recepie>().ToListAsync();
        }

        public Task<Recepie> GetNoteAsync(int id)
        {
            // Get a specific note.
            return database.Table<Recepie>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }

        public Task<int> SaveNoteAsync(Recepie recepie)
        {
            if (recepie.ID != 0)
            {
                // Update an existing note.
                return database.UpdateAsync(recepie);
            }
            else
            {
                // Save a new note.
                return database.InsertAsync(recepie);
            }
        }

        public Task<int> DeleteNoteAsync(Recepie recepie)
        {
            // Delete a note.
            return database.DeleteAsync(recepie);
        }
    }
}
