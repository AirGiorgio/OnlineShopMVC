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
        private readonly IMapper _mapper;
        public OrderServices(IOrderRepo orderRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _mapper = mapper;
        }
        public bool AddOrder(Client? client, List<Product> orderProducts)
        {
            if (client==null)
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
            else if (orderProducts.Count == 0)
            {
                throw new ArgumentException("Nie ma produktów na liście zakupów");
            }
           else return _orderRepo.AddOrder(client, orderProducts);
        }

        public OrdersForListDTO GetAllOrdersFromDate(DateTime? orderDate)
        {
            if (orderDate == null)
            {
                throw new ArgumentException("Nieprawidłowa data zamówienia");
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
                throw new ArgumentException("Nieprawidłowy identyfikator zamówienia");
            }
            else 
            {
                var order = _orderRepo.GetOrderById(id);
                var orderDTO = _mapper.Map<OrderDetailsDTO>(order);

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

        public OrdersForListDTO GetOrdersByOrderDate(Client? client)
        {
            if (client == null)
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
            else
            {
                var orders = _orderRepo.GetOrdersByOrderDate(client).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
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

        public OrdersForListDTO GetOrdersByValue(Client? client)
        {
            if (client == null)
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
            else
            {
                var orders = _orderRepo.GetOrdersByValue(client).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
                return ordersDTO;
            }
        }

        public OrdersForListDTO GetOrdersFromDate(DateTime? orderDate, Client? client)
        {
            if (client == null )
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
            else if (orderDate == null)
            {
                throw new ArgumentException("Nieprawidłowe dane zamówienia");
            }
            else
            {
                var orders = _orderRepo.GetOrdersFromDate(orderDate, client).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
                return ordersDTO;
            }
        }

        public OrdersForListDTO GetOrdersFromValue(int? min, int? max)
        {
            if (min <= 0 || max <= 0)
            {
                throw new ArgumentException("Nieprawidłowe wartości zamówienia");
            }
            else
            {
                var orders = _orderRepo.GetOrdersFromValue(min, max).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
                return ordersDTO;
            }
        }

        public OrdersForListDTO GetOrdersFromValue(Client? client, int? min, int? max)
        {
            if (client == null )
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
            else if (min == null || max == null)
            {
                throw new ArgumentException("Nieprawidłowe dane zamówienia");
            }
            else if (min <= 0 || max <= 0)
            {
                throw new ArgumentException("Nieprawidłowe wartości zamówienia");
            }
            else
            {
                var orders = _orderRepo.GetOrdersFromValue(client, min, max).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).ToList();
                var ordersDTO = new OrdersForListDTO()
                {
                    Orders = orders,
                    Count = orders.Count
                };
                return ordersDTO;
            }
        }

        public bool RemoveOrder(int id)
        {
            if (id <= 0 || id == null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator zamówienia");
            }
            else return _orderRepo.RemoveOrder(id);
        }
    }
}
