using AutoMapper;
using SocialNetwork.Web.Infrastructure.Mapping;

namespace SocialNetwork.Tests.Common
{
    public static class Initializer
    {
        public static void IniializeAuttoMapper()
        {
            Mapper.Initialize(cfg => cfg.AddProfile<AutoMapperProfile>());
        }
    }
}