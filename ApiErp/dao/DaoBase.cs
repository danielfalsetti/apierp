using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Npgsql;

using Dapper.Contrib.Extensions;
using Dapper;

namespace ApiErp.dao
{
    public class DAOBase<T> : DAOBase where T : class
    {

        public T GetById(long id)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Get<T>(id);
            }
        }


        public List<T> Query(string str, dynamic param = null)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Query<T>(str, param: (object)param).ToList();
            }
        }

        public T Get(Func<T, bool> filter)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.GetAll<T>().Where(filter).ToList().FirstOrDefault();
            }
        }

        public List<T> GetAll()
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.GetAll<T>().ToList();
            }
        }

        public List<T> Find(Func<T, bool> filter)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.GetAll<T>().Where(filter).ToList();
            }
        }

        public long Insert(T entity)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Insert(entity);
            }
        }




        public long Insert(List<T> entities)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Insert(entities);
            }
        }



        public bool Update(T entity)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Update(entity);
            }
        }



        public bool Update(List<T> entities)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Update(entities);
            }
        }


        public bool Delete(T entity)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Delete(entity);
            }
        }



        public bool Delete(List<T> entities)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Delete(entities);
            }
        }

    }

    public class DAOBase
    {
        //protected string ConnectionString = @"Data Source=sql5008.site4now.net;Initial Catalog=DB_9B0417_aluguel;User ID=DB_9B0417_aluguel_admin;Password=D@aniel01;Connection Timeout=1000";
        protected string ConnectionString = "Host=localhost;Username=postgres;Password=admin;Database=apirp";

        public NpgsqlConnection AbrirConexao()
        {
            NpgsqlConnection conn = new NpgsqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        public void FecharConexao(NpgsqlConnection connection)
        {
            connection.Close();
        }


        public IEnumerable<dynamic> ExecuteReader(string sql, dynamic param = null, CommandType commandType = CommandType.Text)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Query(sql, param: (object)param, commandType: commandType);
            }
        }

        public dynamic ExecuteScalar(string sql, dynamic param = null, CommandType commandType = CommandType.Text)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.ExecuteScalar(sql, param: (object)param, commandType: commandType);
            }
        }

        public int ExecuteNonQuery(string sql, dynamic param = null, CommandType commandType = CommandType.Text)
        {
            using (var connection = new NpgsqlConnection(ConnectionString))
            {
                return connection.Execute(sql, param: (object)param,
                            commandTimeout: 120, commandType: commandType);
            }
        }

    }
}