using AutoMapper;
using AutoMapper.QueryableExtensions;
using OnlineShopMvc.App.DTOs;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
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
        private readonly IClientRepo _clientRepo;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        public OrderServices(IOrderRepo orderRepo, IMapper mapper, IClientRepo clientRepo, IProductRepo productRepo)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
            _clientRepo = clientRepo;
            _productRepo = productRepo;
        }
        public bool AddOrder(int id, List<Product> orderProducts)
        {
            if (id == null || id <= 0)
            {
                return false;
            }
            else if (orderProducts.Count == 0)
            {
                return false;
            }
           else return _orderRepo.AddOrder(id, orderProducts);
        }

        public OrdersForListDTO GetAllClientOrders(int id, int? pageSize, int? pageNo, DateTime? orderDate, decimal? min, decimal? max, int? value)
        {
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
                var orders = _orderRepo.GetOrdersByOrderDate(id)
                .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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
            else if (!min.HasValue && !max.HasValue && !value.HasValue)
            {
                var orders = _orderRepo.GetOrdersFromDate(orderDate,id).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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
            else if (min.HasValue || max.HasValue)
            {
                var orders = _orderRepo.GetOrdersFromValue(id, min.Value, max.Value).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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
            else if (value.HasValue)
            {
                var orders = _orderRepo.GetOrdersByValue(id).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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
            else return null;
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
            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }
            if (orderDate == null && !min.HasValue && !max.HasValue && !value.HasValue)
            {
                var orders = _orderRepo.GetOrdersByOrderDate()
                .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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
            else if (!min.HasValue && !max.HasValue && !value.HasValue)
            {
                var orders = _orderRepo.GetAllOrdersFromDate(orderDate).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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
            else if (min.HasValue || max.HasValue)
            {
                var orders = _orderRepo.GetOrdersFromValue(min.Value, max.Value).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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
            else if (value.HasValue)
            {
                var orders = _orderRepo.GetOrdersByValue().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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
            else return null;
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
