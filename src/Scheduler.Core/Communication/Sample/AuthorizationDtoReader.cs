using Scheduler.Core.Communication.ResponseReaders;

namespace Scheduler.Core.Communication.Sample
{
    public class AuthorizationDtoReader : DtoResponseReader<AuthorizationDto>
    {
        protected override string ReadToken(AuthorizationDto dto)
        {
            return dto.IsSuccessful
                ? dto.Token
                : null;
        }
    }
}