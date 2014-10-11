using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabaseAccess
{
    /// <summary>
    /// 通用的访问数据库类
    /// 支持数据库 
    /// 1.sql server
    /// 2.mysql
    /// 3.oracle
    /// 4.sqlite
    /// </summary>
    public abstract class CommonDatabaseAccessFactory
    {
        /// <summary>
        /// 就是根据提供的类名创建该类的实例
        /// </summary>
        /// <param name="factoryName">类名</param>
        /// <param name="connectStr">连接字符串</param>
        /// <returns></returns>
        public static CommonDatabaseAccessFactory GetInstance(string factoryName, string connectStr)
        {
            if (!string.IsNullOrEmpty(factoryName) && !string.IsNullOrEmpty(connectStr))
            {
                ConnectStr = connectStr;
                //注意load中的参数程序集的名称  createinstance中参数是类名(注意带上命名空间)
                return (CommonDatabaseAccessFactory)Assembly.Load("CommonDatabaseAccess").CreateInstance("CommonDatabaseAccess." + factoryName);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        protected static string ConnectStr { get; set; }





        #region 数据的获取
        /// <summary>
        /// 获取哦数据库的连接
        /// </summary>
        /// <param name="connectStr">数据库连接字符串</param>
        /// <returns></returns>
        public abstract IDbConnection GetConnection();
        /// <summary>
        /// 获取数据库操作命令
        /// </summary>
        /// <returns></returns>
        public abstract IDbCommand GetCommand();
        /// <summary>
        /// 获取数据库操作命令
        /// </summary>
        /// <param name="sqlStr">sql操作语句</param>
        /// <param name="conn">数据库连接</param>
        /// <returns></returns>
        public abstract IDbCommand GetCommand(string sqlStr, string cmdType);

        /// <summary>
        /// 打开数据库的连接
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public abstract void OpenConnection(IDbConnection conn);
        /// <summary>
        /// 关闭数据库的连接
        /// </summary>
        /// <param name="conn">数据库连接</param>
        public abstract void CloseConnection(IDbConnection conn);

        /// <summary>
        /// 根据sql语句获取表数据
        /// </summary>
        /// <param name="sqlStr">sql操作语句</param>
        /// <returns></returns>
        public abstract DataTable GetDataTableBySql(string sqlStr);
        /// <summary>
        /// 根据sql操作语句获取数据集
        /// </summary>
        /// <param name="sqlStr">sql操作语句</param>
        /// <returns>数据集</returns>
        public abstract DataSet GetDataSetBySql(string sqlStr);


        /// <summary>
        /// 根据存储过程名获取数据表
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns>数据表</returns>
        public abstract DataTable GetDaTatableByProc(string procName, object[] names, object[] values);
        /// <summary>
        /// 根据存储过程名获取数据集
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <returns>数据集</returns>
        public abstract DataSet GetDataSetByProc(string procName, object[] names, object[] values);
        /// <summary>
        /// 返回结果集中的第一行第一列
        /// </summary>
        /// <param name="sqlStr">sql操作语句</param>
        /// <returns></returns>
        public abstract object ExcuteScalar(string sqlStr);
        /// <summary>
        /// 根据sql操作语句生成只读向前的游标
        /// </summary>
        /// <param name="sqlStr">sql操作语句</param>
        /// <returns></returns>
        public abstract IDataReader GetDataReaderBySql(string sqlStr);
        /// <summary>
        /// 根据存储过程生成只读向前的游标
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="names">参数名称</param>
        /// <param name="values">参数值</param>
        /// <returns></returns>
        public abstract IDataReader GetDataReaderByProc(string procName, object[] names, object[] values);
        #endregion


        #region 数据添加和更新
        /// <summary>
        /// 没有使用事务的数据的更新或者添加
        /// </summary>
        /// <param name="sqlStr">sql操作语句</param>
        /// <returns></returns>
        public abstract int ExcuteNoneQueryNoTran(string sqlStr);
        /// <summary>
        /// 使用事务的数据的更新或者添加
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public abstract bool ExcuteNoneQueryByTran(string sqlStr);
        #endregion






    }
}
