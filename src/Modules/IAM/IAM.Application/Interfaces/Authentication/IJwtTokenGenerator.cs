using IAM.Domain.Entities;

namespace IAM.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(Usuario usuario);
}