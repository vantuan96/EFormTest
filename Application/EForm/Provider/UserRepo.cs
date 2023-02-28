using DataAccess.Models;
using DataAccess.Repository;
using System;

namespace EForm.Provider
{
    public class UserRepo : IDisposable
    {
        private IUnitOfWork unitOfWork = new EfUnitOfWork();
        public User ValidateUser(string username, string password)
        {
            return unitOfWork.UserRepository.FirstOrDefault(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        public void Dispose()
        {
            unitOfWork.Dispose();
        }
    }
}