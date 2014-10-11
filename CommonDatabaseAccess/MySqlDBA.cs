using System;
using MySql.Data.MySqlClient;
using System.Data;
namespace CommonDatabaseAccess
{
    class MySqlDBA : CommonDatabaseAccessFactory
    {
        private MySqlConnection conn = null;
        private MySqlCommand cmd = null;
        
        private DataSet ds = null;
        
        private MySqlDataAdapter da = null;
        private MySqlTransaction tran = null;

        public MySqlDBA()
        {
            this.conn = new MySqlConnection(ConnectStr);
        }
        public override IDbConnection GetConnection()
        {
            return new MySqlConnection(ConnectStr);
        }

        public override IDbCommand GetCommand()
        {
            return new MySqlCommand();
        }

        public override void OpenConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public override void CloseConnection(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
        }

        public override DataTable GetDataTableBySql(string sqlStr)
        {
            ds = this.GetDataSetBySql(sqlStr);
            return ds == null ? null : ds.Tables[0];
        }

        public override DataSet GetDataSetBySql(string sqlStr)
        {
            cmd = (MySqlCommand)this.GetCommand(sqlStr, "text");
            da = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public override DataTable GetDaTatableByProc(string procName, object[] names, object[] values)
        {
            ds = this.GetDataSetByProc(procName, names, values);
            return ds == null ? null : ds.Tables[0];
        }

        public override DataSet GetDataSetByProc(string procName, object[] names, object[] values)
        {
            cmd = (MySqlCommand)this.GetCommand(procName, "storeprocedure");
            cmd.CommandTimeout = 180;
            Array paras = Array.CreateInstance(typeof(MySqlParameter), names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                paras.SetValue(new MySqlParameter(names[i].ToString(), values[i]), i);
            }

            cmd.Parameters.AddRange(paras);

            da = new MySqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public override object ExcuteScalar(string sqlStr)
        {
            cmd = (MySqlCommand)this.GetCommand(sqlStr, "text");

            return cmd.ExecuteScalar();
        }

        public override IDataReader GetDataReaderBySql(string sqlStr)
        {
            cmd = (MySqlCommand)this.GetCommand(sqlStr, "text");
            return cmd.ExecuteReader();
        }

        public override IDataReader GetDataReaderByProc(string procName, object[] names, object[] values)
        {
            cmd = (MySqlCommand)this.GetCommand(procName, "storeprocedure");
            Array paras = Array.CreateInstance(typeof(MySqlParameter), names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                paras.SetValue(new MySqlParameter(names[i].ToString(), values[i]), i);
            }

            cmd.Parameters.AddRange(paras);

            return cmd.ExecuteReader();
        }

        public override IDbCommand GetCommand(string sqlStr, string cmdType)
        {
            this.OpenConnection(conn);
            cmd = new MySqlCommand(sqlStr, conn);
            cmd.CommandType = cmdType == "text" ? CommandType.Text : CommandType.StoredProcedure;

            cmd.CommandTimeout = 180;
            return cmd;
        }



        public override int ExcuteNoneQueryNoTran(string sqlStr)
        {
            cmd = (MySqlCommand)this.GetCommand(sqlStr, "text");
            return cmd.ExecuteNonQuery();
        }

        public override bool ExcuteNoneQueryByTran(string sqlStr)
        {
            cmd = (MySqlCommand)this.GetCommand(sqlStr, "text");
            tran = conn.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                cmd.ExecuteNonQuery();
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                this.CloseConnection(conn);
            }
        }
    }
}
