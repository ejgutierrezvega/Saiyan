using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using Saiyan.Repository.Conventions;
using System;

namespace Saiyan.Repository
{
    public class RepositoryConfig
    {
		public static void Initialize()
        {
            var conventionPack = new ConventionPack()
            {
				new CamelCaseElementNameConvention(),
				new IgnoreIfDefaultConvention(false),
				new IgnoreIfNullConvention(true),
				new IgnoreExtraElementsConvention(true),
				new CustomConvention<Guid>(BsonType.String),
				new CustomConvention<decimal>(BsonType.Double)				
            };

            ConventionRegistry.Register("Saiyan Repository", conventionPack, t => true);
        }
    }
}
