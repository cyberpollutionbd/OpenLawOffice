﻿using System;
using AutoMapper;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;
using ServiceStack.DesignPatterns.Model;

namespace OpenLawOffice.Server.Core.DBOs.Security
{
    [Common.Models.MapMe]
    public class SecuredResource: Core, IHasGuidId
    {
        public Guid Id { get; set; }

        public void BuildMappings()
        {
            Mapper.CreateMap<DBOs.Security.SecuredResource, Common.Models.Security.SecuredResource>()
                   .ForMember(dst => dst.IsStub, opt => opt.UseValue(false))
                   .ForMember(dst => dst.UtcCreated, opt => opt.MapFrom(src => src.UtcCreated))
                   .ForMember(dst => dst.UtcModified, opt => opt.MapFrom(src => src.UtcModified))
                   .ForMember(dst => dst.UtcDisabled, opt => opt.MapFrom(src => src.UtcDisabled))
                   .ForMember(dst => dst.CreatedBy, opt => opt.ResolveUsing(db =>
                   {
                       return new Common.Models.Security.User()
                       {
                           Id = db.CreatedByUserId,
                           IsStub = true
                       };
                   }))
                   .ForMember(dst => dst.ModifiedBy, opt => opt.ResolveUsing(db =>
                   {
                       return new Common.Models.Security.User()
                       {
                           Id = db.ModifiedByUserId,
                           IsStub = true
                       };
                   }))
                   .ForMember(dst => dst.DisabledBy, opt => opt.ResolveUsing(db =>
                   {
                       if (!db.DisabledByUserId.HasValue) return null;
                       return new Common.Models.Security.User()
                       {
                           Id = db.DisabledByUserId.Value,
                           IsStub = true
                       };
                   }))
                   .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));

            Mapper.CreateMap<Common.Models.Security.SecuredResource, DBOs.Security.SecuredResource>()
                .ForMember(dst => dst.UtcCreated, opt => opt.MapFrom(src => src.UtcCreated))
                .ForMember(dst => dst.UtcModified, opt => opt.MapFrom(src => src.UtcModified))
                .ForMember(dst => dst.UtcDisabled, opt => opt.MapFrom(src => src.UtcDisabled))
                .ForMember(dst => dst.CreatedByUserId, opt => opt.ResolveUsing(model =>
                {
                    return model.CreatedBy.Id;
                }))
                .ForMember(dst => dst.ModifiedByUserId, opt => opt.ResolveUsing(model =>
                {
                    return model.ModifiedBy.Id;
                }))
                .ForMember(dst => dst.DisabledByUserId, opt => opt.ResolveUsing(model =>
                {
                    if (model.DisabledBy == null) return null;
                    return model.DisabledBy.Id;
                }))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id));
        }
    }
}
