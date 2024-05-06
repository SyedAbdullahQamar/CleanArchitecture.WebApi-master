using Application.DTOs.Account;
using Application.Wrappers;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<Response<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress);
        Task<Response<string>> RegisterAsync(RegisterRequest request);
        Task<UserList> GetUserById(string userId);
        Task<Response<List<UserList>>> UserList();
        Task<Response<string>> UpdateUser(string userId, UpdateUser model);
        Task<Response<string>> DeleteUser(string userId);
    }
}
