﻿using AutoMapper;

namespace Basket.Application.Mappers
{
    public static class BasketMapper
    {
        private static Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                    cfg.AddProfile<BasketMappingProfile>();
                });

                var mapper = config.CreateMapper();
                return mapper;

        });

        public static IMapper Mapper => Lazy.Value;
    }
}

