using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using controle_financeiro.Models;
using SQLite;

namespace controle_financeiro
{
    public class SQLiteUsuario
    {
        SQLiteAsyncConnection db;
        public SQLiteUsuario(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Models.Usuario>().Wait();
        }

        public Task<int> SaveItemAsync(Models.Usuario registro)
        {
            if (registro.ID != 0)
            {
                return db.UpdateAsync(registro);
            }
            else
            {
                return db.InsertAsync(registro);
            }
        }

        //Delete
        public Task<int> DeleteItemAsync(Models.Usuario registro)
        {
            return db.DeleteAsync(registro);
        }

        //Delete all
        public void DeleteAllUsers()
        {
            db.DropTableAsync<Models.Usuario>().Wait();
            db.CreateTableAsync<Models.Usuario>().Wait();
        }

        //Read All Items  
        public Task<List<Models.Usuario>> GetItemsAsync()
        {
            return db.Table<Models.Usuario>().ToListAsync();
        }

        public Task<Models.Usuario> GetItemAsync(int personId)
        {
            return db.Table<Models.Usuario>().Where(i => i.ID == personId).FirstOrDefaultAsync();
        }


        public Task<Models.Usuario> GetItemAsync2(string login, string senha)
        {
            return db.Table<Models.Usuario>()
                .Where(r => r.Nome == login)
                .Where(r => r.Senha == senha)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsyncLogin(Models.Usuario registro)
        {


            if (registro.ID != 0)
            {
                return db.UpdateAsync(registro);
            }
            else
            {
                return db.InsertAsync(registro);
            }
        }

    }
}
