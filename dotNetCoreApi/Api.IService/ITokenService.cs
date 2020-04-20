using Api.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.IService
{
    public interface ITokenService
    {
        string GetToken(UserEntity user);
    }
}
