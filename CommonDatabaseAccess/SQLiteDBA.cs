﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
namespace CommonDatabaseAccess
{
    class SQLiteDBA : CommonDatabaseAccessFactory
    {
        private SQLiteConnection conn = null;
        private SQLiteCommand cmd = null;

        private DataSet ds = null;

        private SQLiteDataAdapter da = null;
        private SQLiteTransaction tran = null;

        public SQLiteDBA()
        {
            this.conn = new SQLiteConnection(ConnectStr);
        }
        public override IDbConnection GetConnection()
        {
            return new SQLiteConnection(ConnectStr);
        }

        public override IDbCommand GetCommand()
        {
            return new SQLiteCommand();
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
            cmd = (SQLiteCommand)this.GetCommand(sqlStr, "text");
            da = new SQLiteDataAdapter(cmd);
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
            cmd = (SQLiteCommand)this.GetCommand(procName, "storeprocedure");
            cmd.CommandTimeout = 180;
            Array paras = Array.CreateInstance(typeof(SQLiteParameter), names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                paras.SetValue(new SQLiteParameter(names[i].ToString(), values[i]), i);
            }

            cmd.Parameters.AddRange(paras);

            da = new SQLiteDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public override object ExcuteScalar(string sqlStr)
        {
            cmd = (SQLiteCommand)this.GetCommand(sqlStr, "text");

            return cmd.ExecuteScalar();
        }

        public override IDataReader GetDataReaderBySql(string sqlStr)
        {
            cmd = (SQLiteCommand)this.GetCommand(sqlStr, "text");
            return cmd.ExecuteReader();
        }

        public override IDataReader GetDataReaderByProc(string procName, object[] names, object[] values)
        {
            cmd = (SQLiteCommand)this.GetCommand(procName, "storeprocedure");
            Array paras = Array.CreateInstance(typeof(SQLiteParameter), names.Length);

            for (int i = 0; i < names.Length; i++)
            {
                paras.SetValue(new SQLiteParameter(names[i].ToString(), values[i]), i);
            }

            cmd.Parameters.AddRange(paras);

            return cmd.ExecuteReader();
        }

        public override IDbCommand GetCommand(string sqlStr, string cmdType)
        {
            this.OpenConnection(conn);
            cmd = new SQLiteCommand(sqlStr, conn);
            cmd.CommandType = cmdType == "text" ? CommandType.Text : CommandType.StoredProcedure;

            cmd.CommandTimeout = 180;
            return cmd;
        }



        public override int ExcuteNoneQueryNoTran(string sqlStr)
        {
            cmd = (SQLiteCommand)this.GetCommand(sqlStr, "text");
            return cmd.ExecuteNonQuery();
        }

        public override bool ExcuteNoneQueryByTran(string sqlStr)
        {
            cmd = (SQLiteCommand)this.GetCommand(sqlStr, "text");
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
