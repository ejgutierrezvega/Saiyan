using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using System.Reflection;

namespace Saiyan.Repository.Conventions
{
    internal sealed class CustomConvention<T> : ConventionBase, IMemberMapConvention where T : struct
    {
        private readonly BsonType toType;

        internal CustomConvention(BsonType _toType)
        {
            toType = _toType;
        }

        public void Apply(BsonMemberMap memberMap)
        {
            var info = memberMap.MemberType.GetTypeInfo();
            if (info != typeof(T) && info != typeof(T?)) return;
            var serializer = memberMap.GetSerializer();
            var serializerConfig = serializer as IRepresentationConfigurable;
            if (serializerConfig != null)
                memberMap.SetSerializer(serializerConfig.WithRepresentation(toType));
            else
            {
                var childSerializer = serializer as IChildSerializerConfigurable;
                if (childSerializer == null) return;

                serializerConfig = childSerializer.ChildSerializer as IRepresentationConfigurable;
                if (serializerConfig == null) return;

                memberMap.SetSerializer(childSerializer.WithChildSerializer(serializerConfig.WithRepresentation(toType)));
            }
        }
    }
}
