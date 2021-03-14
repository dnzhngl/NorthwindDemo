using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;
        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        /// <summary>
        /// Gets operation claims of the given user.
        /// </summary>
        /// <param name="user">User must be given in type of User object.</param>
        /// <returns>Returns SuccessDataResult with the data of the user's claims.</returns>
        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        public IResult Add(User user)
        {
            _userDal.Add(user);
            return new SuccessResult(); 
        }

        /// <summary>
        /// Checks whether the given email has belongs to any user in the database.
        /// </summary>
        /// <param name="email">User's email must be given in type of string.</param>
        /// <returns>Returns success if the email has no matching, else returns ErrorDataResult with the User data that has the given email.</returns>
        public IDataResult<User> GetByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            if (result != null)
            {
                return new ErrorDataResult<User>(result);
            }
            return new SuccessDataResult<User>();
        }
    }
}
