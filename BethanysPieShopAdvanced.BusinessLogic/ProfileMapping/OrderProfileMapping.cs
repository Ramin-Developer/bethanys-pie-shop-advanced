namespace BethanysPieShop.BusinessLogic.ProfileMapping;

public class OrderProfileMapping : Profile
{
    public OrderProfileMapping()
    {
        // Mapping from Order to OrderDto
        CreateMap<Order, OrderDto>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderLineDtoList, opts => opts.MapFrom(src => src.OrderLines))
            .ForMember(dest => dest.OrderStatus, opts => opts.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.AddressLine1, opts => opts.MapFrom(src => src.AddressLine1))
            .ForMember(dest => dest.AddressLine2, opts => opts.MapFrom(src => src.AddressLine1))
            .ForMember(dest => dest.ZipCode, opts => opts.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City))
            .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.State))
            .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country))
            .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.OrderTotal, opts => opts.MapFrom(src => src.OrderTotal))
            .ForMember(dest => dest.OrderPlaced, opts => opts.MapFrom(src => src.OrderPlaced));

        // Mapping from OrderDto to Order
        CreateMap<OrderDto, Order>()
            .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
            .ForMember(dest => dest.OrderLines, opts => opts.MapFrom(src => src.OrderLineDtoList))
            .ForMember(dest => dest.OrderStatus, opts => opts.MapFrom(src => src.OrderStatus))
            .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.LastName))
            .ForMember(dest => dest.AddressLine1, opts => opts.MapFrom(src => src.AddressLine1))
            .ForMember(dest => dest.AddressLine2, opts => opts.MapFrom(src => src.AddressLine2))
            .ForMember(dest => dest.ZipCode, opts => opts.MapFrom(src => src.ZipCode))
            .ForMember(dest => dest.City, opts => opts.MapFrom(src => src.City))
            .ForMember(dest => dest.State, opts => opts.MapFrom(src => src.State))
            .ForMember(dest => dest.Country, opts => opts.MapFrom(src => src.Country))
            .ForMember(dest => dest.PhoneNumber, opts => opts.MapFrom(src => src.PhoneNumber))
            .ForMember(dest => dest.Email, opts => opts.MapFrom(src => src.Email))
            .ForMember(dest => dest.OrderTotal, opts => opts.MapFrom(src => src.OrderTotal))
            .ForMember(dest => dest.OrderPlaced, opts => opts.MapFrom(src => src.OrderPlaced));
    }

}
