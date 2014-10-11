using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcTest.Models
{
    public partial class Person
    {
        public Person()
        { }

        #region Model
        private int _id;
        private string _name;
        private int? _age;
        private string _email;
        /// <summary>
        /// 序号
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 年龄
        /// </summary>
        public int? age
        {
            set { _age = value; }
            get { return _age; }
        }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }
        #endregion Model
    }
}