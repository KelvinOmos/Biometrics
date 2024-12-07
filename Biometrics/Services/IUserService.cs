using System;
using Biometrics.Models;

namespace Biometrics.Services
{
	public interface IUserService
	{
        Task<Response<bool>> RegisterUserAsync(RegisterModel model);
	}
}

