using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogicLayer.Modules.PhoneBook;
using DataAccessLayer;
using FastMember;

namespace BusinessLogicLayer
{
    public class Test
    {
        public int MyID{ get; set; }
        public string Name{ get; set; }


        public Test()
        {
            //this.PersonList = new List<Person>();
            //this.PersonList.Add(new Person(){PersonID = 1, FirstName="a"});
            //this.PersonList.Add(new Person(){PersonID = 2, FirstName="b"});
            //this.PersonList.Add(new Person(){PersonID = 3, FirstName="c"});
            //this.PersonList.Add(new Person(){PersonID = 4, FirstName="d"});

            //this.PersonTable = new DataTable();
            //using (IDataReader reader =ObjectReader.Create<Person>(this.PersonList))
            //{
            //    this.PersonTable.Load(reader);
            //}

            //this.PersonTable.Columns["FirstName"].ColumnName = "نام";
        }


        public async void MyMethod1(string path)
        {
            string databaseFilePath = path + @"\PhoneBookDatabase.mdf";

            int id = 22;
            string name = "مد";
            string lastName = "";
            long phoneNumber = 761554;
            string nationalCode = "3";

            
            using (RepositoryFactory<Person> repositoryFactory = new RepositoryFactory<Person>(databaseFilePath))
            {
                IRepository<Person> repository = repositoryFactory.GetRepository();
                IEnumerable<Person> personEntities = repository.GetAll();

                DataTable dataTable = new DataTable();
                using (ObjectReader variable1 = ObjectReader.Create<Person>(personEntities))
                {
                    dataTable.Load(variable1);
                }
                


                //IEnumerable<PersonEntity> myEntities = await repository.GetAllAsync();
                //IEnumerable<PersonEntity> myEntities_5 = repository.SearchByAllFields(name, lastName, "", nationalCode);
                //IEnumerable<PersonEntity> myEntities_2 = repository.SearchByID(3);

                //bool isExecuted = repository.Insert(name, lastName, phoneNumber, nationalCode);
                //bool isExecuted = repository.Update(id, name, lastName, phoneNumber, nationalCode);
                //bool isExecuted = repository.Delete(id);


                //List<PersonEntity> myEntities = (await repository.GetAllAsync()).ToList();

                //////List<PersonEntity> myEntities = (await repository.SearchByAllFieldsAsync(
                //////    name, lastName, "", nationalCode)).ToList();
                //foreach (var personEntity in myEntities)
                //{
                //    Debug.WriteLine(personEntity.FirstName);
                //}

            }


            //return myEntities;
        }


        List<Person> PersonList { get; set; }

        DataTable PersonTable { get; set; }

        public void LoadDataTable()
        {
            this.PersonList.Add(new Person()
                {PersonID = 42, FirstName = "امیر مهدی", LastName = "ضیایی", PhoneNumber = 4742});


            using (IDataReader dataReader = ObjectReader.Create(this.PersonList))
            {
                this.PersonTable.Load(dataReader);
            }
        }


    }
}
