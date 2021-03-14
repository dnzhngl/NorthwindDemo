using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        /// <summary>
        /// Checks whether the given email has any matching.
        /// </summary>
        /// <param name="email">Email in type of string must be given.</param>
        /// <returns>Returns SuccessResult if the email does not exist in the database, else returns ErrorResult with a user already exists message.</returns>
        IResult UserExists(string email);
        /// <summary>
        /// Creates access token for the given User that includes the user's claims.
        /// </summary>
        /// <param name="user">User object in type of User.</param>
        /// <returns>Returns SuccessDataResult with the data of created token in type of AccessToken and success message.</returns>
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
