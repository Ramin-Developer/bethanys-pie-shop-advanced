namespace BethanysPieShop.BusinessLogic.ProfileMappings;

public class OrderLineProfileMapping : Profile
{
    public OrderLineProfileMapping()
    {
        // OrderLine -> OrderLineDto:
        CreateMap<OrderLine, OrderLineDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderId, opts => opts.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.PieId, opts => opts.MapFrom(src => src.PieId))
            .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
            .ForMember(dest => dest.OrderDto, opts => opts.MapFrom(src => src.Order))
            .ForMember(dest => dest.PieDto, opts => opts.MapFrom(src => src.Pie));

        // OrderLineDto -> OrderLine:
        CreateMap<OrderLineDto, OrderLine>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderId, opts => opts.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.PieId, opts => opts.MapFrom(src => src.PieId))
            .ForMember(dest => dest.Amount, opts => opts.MapFrom(src => src.Amount))
            .ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
            .ForMember(dest => dest.Order, opts => opts.MapFrom(src => src.OrderDto))
            .ForMember(dest => dest.Pie, opts => opts.MapFrom(src => src.PieDto));
    }
}
