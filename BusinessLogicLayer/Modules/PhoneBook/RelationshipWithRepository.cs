using DataAccessLayer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Modules.PhoneBook
{
    internal class RelationshipWithRepository<Tp> where Tp : class
    {
        private string fullDatabaseFilePath;


        internal RelationshipWithRepository(string fullDatabaseFilePath)
        {
            if (string.IsNullOrEmpty(fullDatabaseFilePath) == true)
                throw new ArgumentNullException("fullDatabaseFilePath", "مسیر کامل فایل پایگاه داده ، نمیتواند خالی یا null باشد");

            this.fullDatabaseFilePath = fullDatabaseFilePath;
        }



        internal IEnumerable<Tp> GetAllDataFromRepository()
        {
            IEnumerable<Tp> typeEnumerable = null;
            using (RepositoryFactory<Tp> reposFactory = new RepositoryFactory<Tp>(this.fullDatabaseFilePath))
            {
                IRepository<Tp> repository = reposFactory.GetRepository();
                typeEnumerable = repository.GetAll();
            }

            return typeEnumerable;
        }


        internal async Task<IEnumerable<Tp>> GetAllDataFromRepositoryAsync()
        {
            IEnumerable<Tp> typeEnumerable = null;
            using (RepositoryFactory<Tp> reposFactory = new RepositoryFactory<Tp>(this.fullDatabaseFilePath))
            {
                IRepository<Tp> repository = reposFactory.GetRepository();
                typeEnumerable = await repository.GetAllAsync();
            }

            return typeEnumerable;
        }


        internal IEnumerable<Tp> SearchByAllFieldsFromRepository(
            string firstName = "", string lastName = "", string phoneNumber = "", string nationalCode = "")
        {
            IEnumerable<Tp> typeEnumerable = null;
            using (RepositoryFactory<Tp> reposFactory = new RepositoryFactory<Tp>(this.fullDatabaseFilePath))
            {
                IRepository<Tp> repository = reposFactory.GetRepository();
                typeEnumerable = repository.SearchByAllFields(firstName, lastName, phoneNumber, nationalCode);
            }

            return typeEnumerable;
        }


        internal Tp SearchByIdFromRepository(int id)
        {
            Tp intendedType = null;
            using (RepositoryFactory<Tp> reposFactory = new RepositoryFactory<Tp>(this.fullDatabaseFilePath))
            {
                IRepository<Tp> repository = reposFactory.GetRepository();
                IEnumerable<Tp> typeEnumerable = repository.SearchByID(id);

                if (typeEnumerable != null && typeEnumerable.Count() > 0)
                    intendedType = typeEnumerable.ToList()[0];
            }

            return intendedType;
        }


        internal bool InsertToRepository(string firstName, string lastName, long phoneNumber, string nationalCode = "")
        {
            bool isInserted = false;
            using (RepositoryFactory<Tp> reposFactory = new RepositoryFactory<Tp>(this.fullDatabaseFilePath))
            {
                IRepository<Tp> repository = reposFactory.GetRepository();
                isInserted = repository.Insert(firstName, lastName, phoneNumber, nationalCode);
            }

            return isInserted;
        }


        internal bool UpdateToRepository(int id, string firstName, string lastName, long phoneNumber, string nationalCode = "")
        {
            bool isUpdated = false;
            using (RepositoryFactory<Tp> reposFactory = new RepositoryFactory<Tp>(this.fullDatabaseFilePath))
            {
                IRepository<Tp> repository = reposFactory.GetRepository();
                isUpdated = repository.Update(id, firstName, lastName, phoneNumber, nationalCode);
            }

            return isUpdated;
        }


        internal bool DeleteFromRepository(int id)
        {
            bool isDeleted = false;
            using (RepositoryFactory<Tp> reposFactory = new RepositoryFactory<Tp>(this.fullDatabaseFilePath))
            {
                IRepository<Tp> repository = reposFactory.GetRepository();
                isDeleted = repository.Delete(id);
            }

            return isDeleted;
        }


    }
}
