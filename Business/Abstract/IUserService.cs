using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        /// <summary>
        /// Gets operation claims of the given user.
        /// </summary>
        /// <param name="user">User must be given in type of User object.</param>
        /// <returns>Returns SuccessDataResult with the data of the user's claims.</returns>
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IResult Add(User user);
        /// <summary>
        /// Checks whether the given email has belongs to any user in the database.
        /// </summary>
        /// <param name="email">User's email must be given in type of string.</param>
        /// <returns>Returns success if the email has no matching, else returns ErrorDataResult with the User data that has the given email.</returns>
        IDataResult<User> GetByMail(string email);
    }
}
