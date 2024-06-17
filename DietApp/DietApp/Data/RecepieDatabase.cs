using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using DietApp.Models;
using System.Diagnostics;
using System;

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
            _database.CreateTableAsync<UserMacros>().Wait();
            _database.CreateTableAsync<Meal>().Wait();
            _database.CreateTableAsync<RecepieIngredients>().Wait();

        }



        public Task<List<Recepie>> GetNotesAsync()
        {
            //Get all notes.
            return _database.Table<Recepie>().ToListAsync();
        }
        public Task<List<Meal>> GetMealsAsync()
        {
            //Get all meals.
            return _database.Table<Meal>().ToListAsync();
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
        public Task<int> SaveMealAsync(Meal meal)
        {
            if (meal.ID != 0)
            {
                // Update an existing note.
                return _database.UpdateAsync(meal);
            }
            else
            {
                // Save a new note.
                return _database.InsertAsync(meal);
            }
        }
        public Task<int> SaveRecepieIngredientAsync(RecepieIngredients ingredient)
        {
            if (ingredient.ID != 0)
            {
                // Update an existing note.
                return _database.UpdateAsync(ingredient);
            }
            else
            {
                // Save a new note.
                return _database.InsertAsync(ingredient);
            }
        }
        public Task<int> DeleteNoteAsync(Recepie recepie)
        {
            // Delete a note.
            return _database.DeleteAsync(recepie);
        }
        public Task<int> DeleteMealAsync(Meal meal)
        {
            // Delete a note.
            return _database.DeleteAsync(meal);
        }


        public Task<UserData> GetUserDataAsync()
        {
            return _database.Table<UserData>().FirstOrDefaultAsync();
        }

        public Task<UserMacros> GetUserMacrosAsync()
        {
            return _database.Table<UserMacros>().FirstOrDefaultAsync();
        }

        public async Task<List<RecepieIngredients>> GetIngredientsForRecipeAsync(int recipeId)
        {
            var recipe = await _database.Table<Recepie>().Where(r => r.ID == recipeId).FirstOrDefaultAsync();
            return recipe?.Ingredients ?? new List<RecepieIngredients>();
        }
        public async Task SaveUserMacrosAsync(UserMacros userMacros)
        {
            try
            {
                var existingUserMacros = await GetUserMacrosAsync();
                if (existingUserMacros != null)
                {
                    userMacros.ID = existingUserMacros.ID; // Zakładając, że Id jest kluczem głównym
                    await _database.UpdateAsync(userMacros);
                }
                else
                {
                    await _database.InsertAsync(userMacros);
                }

                //Debug.WriteLine("User macros saved successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving user macros: {ex.Message}");
            }
        }

        public Task<int> SaveUserDataAsync(UserData userData)
        {
            if (userData.ID == 1)
            {
                return _database.InsertAsync(userData);
            }
            else
            {
                return _database.UpdateAsync(userData);

            }
        }


        public Task<List<RecepieIngredients>> GetMealIngredientsAsync(int recipeId)
        {
            return _database.Table<RecepieIngredients>().Where(i => i.ID == recipeId).ToListAsync();
        }

    }
}
