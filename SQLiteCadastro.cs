using controle_financeiro.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace controle_financeiro
{
    public class SQLiteCadastro
    {
        SQLiteAsyncConnection db;

        public SQLiteCadastro(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Cadastro>().Wait();
        }


        public Task<int> CreateElemento(Cadastro registro)
        {
            return db.InsertAsync(registro);
        }


        public Task<List<Cadastro>> ReadElementos()
        {
            return db.Table<Cadastro>().ToListAsync();
        }
        public Task<int> ExcluirElemento(Cadastro elemento)
        {
            return db.DeleteAsync(elemento);
        }

        public Task<Cadastro> GetItemAsync(int personId)
        {
            return db.Table<Cadastro>().Where(i => i.ID == personId).FirstOrDefaultAsync();
        }

        public Task<int> DeleteElementoAsync(Cadastro registro)
        {
            return db.DeleteAsync(registro);
        }
        public Task<int> UpdateElementoAsync(Cadastro item)
        {
            return db.UpdateAsync(item);
        }
    }
}
