using System.Threading.Tasks;
using Cabinet.Dtos.External.Request;
using Cabinet.Dtos.External.Response;

namespace Dab.Clients.PrivateEntity {
    public interface IPrivateEntityApiClientService {
        Task<ApplicationResponseDto> NewPrivateEntityAsync(int nameId);
        Task<ApplicationResponseDto> NewPrivateEntityOffice(NewPrivateEntityOfficeRequestDto dto);        
        Task<ApplicationResponseDto> NewDirectors(NewDirectorsRequestDto dto);     
        Task<ApplicationResponseDto> NewSecretary(NewSecretaryRequestDto dto);
    }
}