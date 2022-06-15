using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using Swap.GithubTracker.Domain.Model;
using System;

namespace Swap.GithubTracker.Infra.Data.Mapping
{
    public static class MappingProfile
    {
        public static void Map()
        {
            ConventionRegistry.Register("SwapGithubTrackerConventions", new ConventionPack { new CamelCaseElementNameConvention() }, type => true);

            BsonSerializer.RegisterSerializer(typeof(DateTime), new DateTimeSerializer(DateTimeKind.Local));

            BsonClassMap.RegisterClassMap<GithubTrack>(cm =>
            {
                cm.AutoMap();
                cm.MapIdMember(c => c.Id).SetIgnoreIfDefault(true).SetSerializer(new StringSerializer(BsonType.ObjectId));
            }
           );
        }
    }
}
