using System;
using System.Data;

namespace bizapps_test.DAL.Repositories
{
    public interface IUnitOfWork: IDisposable
    {
        IDbCommand CreateCommand();

        void SaveChanges();
    }
}