using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseAccess;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Data;
namespace MvcTest.Models
{
    public class Logic : ILogic
    {
        private CommonDatabaseAccessFactory factory = null;
        private string sqlStr = string.Empty;

        private static string factoryName = WebConfigurationManager.AppSettings["factoryName"];
        private static string connectStr = WebConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        public Logic()
        {
            factory = CommonDatabaseAccessFactory.GetInstance(factoryName, connectStr);
        }
        public IList<Person> GetPersonInfos()
        {

            #region
            //using (IDataReader reader = factory.GetDataReaderByProc("GetPersonProc", new object[] { "@name" }, new object[] { "cosenlee" }))
            //{
            //    IList<Person> pers = new List<Person>();
            //    while (reader.Read())
            //    {
            //        pers.Add(new Person()
            //        {
            //            id = reader.GetInt32(0),
            //            name = reader.GetString(1),
            //            age = reader.GetInt32(2),
            //            email = reader.GetString(3)
            //        });
            //    }
            //    return pers;
            //} 
            #endregion

            using (IDataReader reader = factory.GetDataReaderBySql("select * from person"))
            {
                IList<Person> pers = new List<Person>();
                while (reader.Read())
                {
                    pers.Add(new Person()
                    {
                        id = reader.GetInt32(0),
                        name = reader.GetString(1),
                        age = reader.GetInt32(2),
                        email = reader.GetString(3)
                    });
                }
                return pers;
            }
        }


        public void AddPersonInfo(Person person)
        {
            sqlStr = string.Format("insert into person(name,age,email) values('{0}',{1},'{2}')", person.name, person.age, person.email);
            //sqlStr = string.Format("insert into person(id,name,age,email) values({0},'{1}',{2},'{3}')", 1,person.name, person.age, person.email);
            factory.ExcuteNoneQueryByTran(sqlStr);
        }


        public void EditPersonInfo(Person person)
        {
            sqlStr = string.Format("update person set name='{0}',age={1},email='{2}' where id={3}", person.name, person.age, person.email, person.id);
            factory.ExcuteNoneQueryByTran(sqlStr);
        }


        public Person GetPersonInfo(int? id)
        {
            using (IDataReader reader = factory.GetDataReaderBySql(string.Format("select * from person where id={0}", id)))
            {
                Person person = new Person();
                if (reader.Read())
                {

                    person.id = reader.GetInt32(0);
                    person.name = reader.GetString(1);
                    person.age = reader.GetInt32(2);
                    person.email = reader.GetString(3);

                }
                return person;
            }
        }


        public void DeletePersonInfo(Person person)
        {
            sqlStr = string.Format("delete from person  where id={0}", person.id);
            factory.ExcuteNoneQueryByTran(sqlStr);
        }


        public IEnumerable<DataRow> GetMobile()
        {
            sqlStr = string.Format("select * from mobile");
            return factory.GetDataTableBySql(sqlStr).AsEnumerable();
        }
    }
}