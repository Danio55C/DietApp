using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using DietApp.Models;
using System.Diagnostics;
using System;

namespace DietApp.Data
{
    public class RecipieDatabase
    {
        private readonly SQLiteAsyncConnection _database;

        public RecipieDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

            _database.CreateTableAsync<Recipe>().Wait();
            _database.CreateTableAsync<UserData>().Wait();
            _database.CreateTableAsync<UserMacros>().Wait();
            _database.CreateTableAsync<Meal>().Wait();
            _database.CreateTableAsync<RecipeIngredients>().Wait();

        }



        public Task<List<Recipe>> GetNotesAsync()
        {
            //Get all notes.
            return _database.Table<Recipe>().ToListAsync();
        }
        public Task<List<Meal>> GetMealsAsync()
        {
            //Get all meals.
            return _database.Table<Meal>().ToListAsync();
        }

        public Task<Recipe> GetNoteAsync(int id)
        {
            // Get a specific note.
            return _database.Table<Recipe>()
                            .Where(i => i.ID == id)
                            .FirstOrDefaultAsync();
        }


        public Task<int> SaveNoteAsync(Recipe Recipe)
        {
            if (Recipe.ID != 0)
            {
                // Update an existing note.
                return _database.UpdateAsync(Recipe);
            }
            else
            {
                // Save a new note.
                return _database.InsertAsync(Recipe);
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
        public Task<int> SaveRecipeIngredientAsync(RecipeIngredients ingredient)
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
        public Task<int> DeleteNoteAsync(Recipe Recipe)
        {
            // Delete a note.
            return _database.DeleteAsync(Recipe);
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

        public async Task<List<RecipeIngredients>> GetIngredientsForRecipeAsync(int recipeId)
        {
            var recipe = await _database.Table<Recipe>().Where(r => r.ID == recipeId).FirstOrDefaultAsync();
            return recipe?.Ingredients ?? new List<RecipeIngredients>();
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
            if (userData.ID == 0)
            {
                return _database.InsertAsync(userData);
            }
            else
            {
                return _database.UpdateAsync(userData);

            }
        }


        public Task<List<RecipeIngredients>> GetMealIngredientsAsync(int recipeId)
        {
            return _database.Table<RecipeIngredients>().Where(i => i.ID == recipeId).ToListAsync();
        }

    }
}
