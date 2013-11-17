using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using HomeBrewComp.Domain;
using HomeBrewComp.Reflection;

namespace HomeBrewComp.Persistence
{
    internal class HomeBrewCompModelMapper
    {
        public HbmMapping CreateMappings()
        {
            ConventionModelMapper mapper = new ConventionModelMapper();

            mapper.IsEntity((type, declared) =>
            {
                bool isEntity = type.IsEntity() || type.IsValueObject();
                return isEntity;
            });

            mapper.IsRootEntity((type, declared) => type.IsRootEntity());

            mapper.BeforeMapClass += BeforeMapClass;

            mapper.IsList(IsList);
            mapper.BeforeMapList += BeforeMapList;

            mapper.IsSet(IsSet);
            mapper.BeforeMapSet += BeforeMapSet;

            mapper.BeforeMapProperty += BeforeMapProperty;

            mapper.BeforeMapManyToOne += BeforeMapManyToOne;

            this.AddExplicitMappings(mapper);

            var mappedTypes = this.GetType().Assembly.GetTypes()
                .Where(t => t.IsEntity() || t.IsValueObject()).ToList();

            mapper.AddMappings(mappedTypes);

            var mappings = mapper.CompileMappingFor(mappedTypes);
            return mappings;
        }

        private void BeforeMapClass(IModelInspector insp, Type type, IClassAttributesMapper map)
        {
            Debug.Assert(type.Namespace != null, "type.Namespace != null");

            map.Id(id =>
            {
                id.Column(type.Name + "Id");
                id.Type(new NHibernate.Type.Int32Type());
                id.Generator(Generators.HighLow);
            });
        }

        private bool IsList(MemberInfo memberInfo, bool declared)
        {
            var type = memberInfo.GetPropertyOrFieldType();
            return type.IsGenericType && typeof(IList<>).IsAssignableFrom(type.GetGenericTypeDefinition());
        }

        private void BeforeMapList(IModelInspector insp, PropertyPath member, IListPropertiesMapper map)
        {
            map.Index(idx =>
            {
                idx.Base(1);
                idx.Column("Version");
            });

            map.Key(key => SetupForeignKey(key, insp, member));
        }

        private bool IsSet(MemberInfo member, bool declared)
        {
            var type = member.GetPropertyOrFieldType();
            return type.IsGenericType && typeof(Iesi.Collections.Generic.ISet<>).IsAssignableFrom(type.GetGenericTypeDefinition());
        }

        private void BeforeMapSet(IModelInspector insp, PropertyPath member, ISetPropertiesMapper map)
        {
            map.Cascade(Cascade.All);
            map.Inverse(true);
            map.Key(key => SetupForeignKey(key, insp, member));
        }

        private void BeforeMapManyToOne(IModelInspector insp, PropertyPath member, IManyToOneMapper map)
        {
            map.Cascade(Cascade.All);

            map.Column(member.ToColumnName() + "Id");
            var fkName = GetForeignKeyName(insp, member);
            map.ForeignKey(fkName);
        }

        private void SetupForeignKey(IKeyMapper key, IModelInspector insp, PropertyPath member)
        {
            var containerType = member.GetContainerEntity(insp);
            var memberType = member.LocalMember.GetPropertyOrFieldType();
            key.Column(member.ToColumnName() + "Id");

            var fkName = GetForeignKeyName(insp, member);
            key.ForeignKey(fkName);
        }

        private string GetForeignKeyName(IModelInspector insp, PropertyPath member)
        {
            var containerType = member.GetContainerEntity(insp);
            return string.Format("FK_{0}_{1}", containerType.Name, member.ToColumnName());
        }

        private void AddExplicitMappings(ModelMapper modelMapper)
        {
            var explicitMappings = from t in this.GetType().Assembly.GetTypes()
                                   where t.BaseType != null && t.GetBaseTypes().Any(bt => bt.IsGenericType &&
                                       bt.GetGenericTypeDefinition().In(typeof(ClassMapping<>), typeof(SubclassMapping<>), typeof(JoinedSubclassMapping<>), typeof(UnionSubclassMapping<>)))
                                   select t;

            foreach (var mapping in explicitMappings)
            {
                modelMapper.AddMapping(mapping);
            }
        }

        private void BeforeMapProperty(IModelInspector modelInspector, PropertyPath member, IPropertyMapper map)
        {
            var propertyInfo = (PropertyInfo)member.LocalMember;

            if (propertyInfo.PropertyType.IsEnumeration())
            {
                map.Column(member.ToColumnName() + "Id");

                var userType = typeof(EnumerationUserType<>).MakeGenericType(propertyInfo.PropertyType);
                map.Type(userType, null);
            }
        }
    }
}