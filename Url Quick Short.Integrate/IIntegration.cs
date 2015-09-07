using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Url_Quick_Short.Integrate.Fields;

namespace Url_Quick_Short.Integrate
{
    public interface IIntegration
    {
        Guid Id { get; }
        string Name { get; }
        List<FieldBase> AuthenticationFields { get; }
        string SharedSecret { get; }
        Task<string> GetAuthenticationData(Dictionary<string, string> authenticationFields);
        Task<string> ShortenUrl(string url, string authenticationData);
    }
}
