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

        public OrdersForListDTO GetAllOrdersFromDate(DateTime? orderDate)
        {
            if (orderDate == null)
            {
                return null;
            }
            else  
            {
                var orders = _orderRepo.GetAllOrdersFromDate(orderDate)
                    .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
               
                return ordersDTO;
            }   
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
                var client = _clientRepo.GetClientById(order.ClientId);
                var clientDTO = _mapper.Map<ClientDTO>(client);
                //var product = _productRepo.GetProductsByOrderId(id);
                orderDTO.Client = clientDTO;
                return orderDTO;
            }  
        }

        public OrdersForListDTO GetOrdersByOrderDate()
        { 
            var orders = _orderRepo.GetOrdersByOrderDate()
           .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
            var ordersDTO = new OrdersForListDTO()
            {
                Orders = orders,
                Count = orders.Count
            };
            return ordersDTO;
        }

        public OrdersForListDTO GetOrdersByOrderDate(int id)
        {
            if (id<=0 || id == null)
            {
                return null;
            }
            else
            {
                var orders = _orderRepo.GetOrdersByOrderDate(id).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
                return ordersDTO;
            }
        }

        public OrdersForListDTO GetOrdersByValue()
        {
            var orders = _orderRepo.GetOrdersByValue().ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();  
            var ordersDTO = new OrdersForListDTO()
            {
                Orders = orders,
                Count = orders.Count
            };
            return ordersDTO;
        }

        public OrdersForListDTO GetOrdersByValue(int id)
        {
            if (id <= 0 || id == null)
            {
                return null;
            }
            else
            {
                var orders = _orderRepo.GetOrdersByValue(id).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
                return ordersDTO;
            }
        }

        public OrdersForListDTO GetOrdersFromDate(DateTime? orderDate, int id)
        {
            if (id <= 0 || id == null)
            {
                return null;
            }
            else if (orderDate == null)
            {
                return null;
            }
            else
            {
                var orders = _orderRepo.GetOrdersFromDate(orderDate, id).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
                return ordersDTO;
            }
        }

        public OrdersForListDTO GetOrdersFromValue(decimal? min, decimal? max)
        {
            if (!min.HasValue || !max.HasValue || min<=0 || max<=0)
            {
                min = 0;
                max = 0;
            }
                var orders = _orderRepo.GetOrdersFromValue(min.Value, max.Value).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
                return ordersDTO;
        }

        public OrdersForListDTO GetOrdersFromValue(int id, decimal? min, decimal? max)
        {
            if (id<=0 || id == null )
            {
                return null;
            }
            else if (!min.HasValue || !max.HasValue || min <= 0 || max <= 0)
            {
                min = 0;
                max = 0;
            }
                var orders = _orderRepo.GetOrdersFromValue(id, min.Value, max.Value).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
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
