using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CommonDatabaseAccess;
using System.Data;
namespace MvcTest.Models
{
    public interface ILogic
    {

        IList<Person> GetPersonInfos();

        void AddPersonInfo(Person person);


        void EditPersonInfo(Person person);


        Person GetPersonInfo(int? id);

        void DeletePersonInfo(Person person);

        IEnumerable<DataRow> GetMobile();


    }
}