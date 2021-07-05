using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace HHL.PostreSQL.Core
{
    public static class MapperExtensions
    {
        //public static void InheritMappingFromBaseType<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
        //{
        //    var sourceType = typeof(TSource);
        //    var desctinationType = typeof(TDestination);
        //    var sourceParentType = sourceType.BaseType;
        //    var destinationParentType = desctinationType.BaseType;

        //    mappingExpression
        //        .BeforeMap((x, y) => Mapper.Map(x, y, sourceParentType, destinationParentType))
        //        .ForAllMembers(x => x.Condition(r => r.);
        //}

        //private static bool NotAlreadyMapped(Type sourceType, Type desitnationType, ResolutionContext r)
        //{
        //    return !r. .IsSourceValueNull  &&
        //           r.Mapper.FindTypeMapFor(sourceType, desitnationType).GetPropertyMaps().Where(
        //               m => m.DestinationProperty.Name.Equals(r.MemberName)).Select(y => !y.IsMapped()).All(b => b);
        //}

        //private static bool NotAlreadyMapped(Type sourceType, Type desitnationType, ResolutionContext r)
        //{
        //    return true;
        //}
    }

}
