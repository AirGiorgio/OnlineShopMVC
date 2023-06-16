using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineShopMvc.App.DTOs;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.DTOs.ProductDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMvc.Inf.Repo;
using OnlineShopMVC.Domain.Model;
using SteamLibraryMVC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Services
{
    public class OrderServices : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly IMapper _mapper;
        public OrderServices(IOrderRepo orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            
        }
        public bool AddOrder(int id, List<ProductDTO> orderProducts)
        {
            if (id == null || id <= 0)
            {
                return false;
            }
            else if (orderProducts.Count == 0)
            {
                return false;
            }
            else return false; ; 
        }

        public OrdersForListDTO GetAllClientOrders(int id, int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value)
        {
            List<OrderDTO> orders = new List<OrderDTO>();

            if (id == null || id <= 0)
            {
                return null;
            }
            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }
            if (orderDate == null && !min.HasValue && !max.HasValue && !value.HasValue)
            {
                 orders = _orderRepo.GetClientOrdersByOrderDate(id)
                .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
             
            }
            else if (!min.HasValue && !max.HasValue && !value.HasValue)
            {
                 orders = _orderRepo.GetClientOrdersFromDate(orderDate,id)
                    .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                 
            }
            else if (min.HasValue || max.HasValue)
            {
                orders = _orderRepo.GetClientOrdersFromValue(id, min, max)
                    .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                
            }
            else if (value.HasValue)
            {
                 orders = _orderRepo.GetClientOrdersByValue(id)
                    .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
            }
            var ordersToShow = orders.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();
            var ordersDTO = new OrdersForListDTO()
            {
                Orders = ordersToShow,
                PageNum = pageNo.Value,
                PageSize = pageSize.Value,
                Count = orders.Count
            };
            return ordersDTO;
        }

        public OrderDetailsDTO GetOrderById(int id)
        {
            if (id == null || id <= 0)
            {
                return null;
            }
            else 
            {
                var order = _orderRepo.GetOrderById(id);
                var orderDTO = _mapper.Map<OrderDetailsDTO>(order);
             
                return orderDTO;
            }  
        }

        public OrdersForListDTO GetOrders(int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value)
        {
            List<OrderDTO> orders = new List<OrderDTO>();
            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }
            if (orderDate == null && !min.HasValue && !max.HasValue && !value.HasValue)
            {
                orders = _orderRepo.GetOrdersByOrderDate()
                .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
              
            }
            else if (!min.HasValue && !max.HasValue && !value.HasValue)
            {
                 orders = _orderRepo.GetAllOrdersFromDate(orderDate)
                    .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
               
            }
            else if (min.HasValue || max.HasValue)
            {
               
                orders = _orderRepo.GetOrdersFromValue(min, max)
                    .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (value.HasValue)
            {
                orders = _orderRepo.GetOrdersByValue().
                    ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList(); 
            }
            var ordersToShow = orders.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();
             
            var ordersDTO = new OrdersForListDTO()
            {
                Orders = ordersToShow,
                PageNum = pageNo.Value,
                MinValue = min,
                MaxValue = max,
                SortByPrice = value,
                OrderDate = orderDate,
                PageSize = pageSize.Value,
                Count = orders.Count
            };
            return ordersDTO;
        }
 
        public bool RemoveOrder(int id)
        {
            if (id <= 0 || id == null)
            {
                return false;
            }
            else return _orderRepo.RemoveOrder(id);
        }
    }
}
