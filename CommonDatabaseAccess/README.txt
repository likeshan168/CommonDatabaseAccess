/*
作者：cosen
邮箱：likeshan168@sina.com
创建时间：2014-10-10
版本：1.0.0.1
*/

功能说明：支持数据库sql server、mysql、oracle、progesql、mongodb等常用数据库的操作
方法说明：
一、获取
1.DataTable GetDataTableBySql(string sqlStr);
功能：通过给定的sql语句获取表数据，返回类型DataTable

2.Dataset GetDataSetBySql(string sqlStr);
功能：通过给定的sql语句获取数据集数据，返回类型Dataset

3.DataTable GetDaTatableByProc(string procName);
功能：通过给定的存储过程名称获取数据表数据，返回类型Datatable

4.DataSet GetDataSetByProc(string procName);
功能：通过给定的存储过程名称获取数据集数据，返回类型Dataset

5.object ExcuteScalre(string procName);
功能：通过给定的存储过程名称获取数据集数据，返回类型Dataset