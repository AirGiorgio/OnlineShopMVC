using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.CategoryDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMvc.Inf.Repo;
using OnlineShopMVC.Domain.Model;
using SteamLibraryMVC.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Services
{
    public class ClientServices : IClientService
    {
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;
        public ClientServices(IClientRepo clientRepo, IMapper mapper)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
        }
        public string UpdateClientAndAddress(int id, string? name, string? surname, string? email, string? telephone, string? street, string? buildingNumber,
         string? flatNumber, string? city, string? zipCode)
        {
            if (id <= 0 || id == null)
            {
                return null;
            }
            if (street.IsNullOrEmpty() || buildingNumber.IsNullOrEmpty() ||flatNumber.IsNullOrEmpty()|| city.IsNullOrEmpty() || zipCode.IsNullOrEmpty())
            {
                return "Dane adresowe są niepoprawne";
            }
            else if (name.IsNullOrEmpty() || surname.IsNullOrEmpty() || email.IsNullOrEmpty() || telephone.IsNullOrEmpty())
            {
                return "Dane klienta są niepoprawne";
            }
            Client client = new Client();
            client.Name = name;
            client.Surname = surname;
            client.EmailAdress = email;
            client.Telephone = telephone;

            Address address = new Address();
            address.ClientId = id;
            address.Street = street;
            address.BuildingNumber = buildingNumber;
            address.FlatNumber = flatNumber;
            address.City = city;
            address.ZipCode = zipCode;

            return _clientRepo.UpdateClientAndAddress(address, client, id);       
        }
       
        public ClientDetailsDTO GetClientById(int id)
        {
            if (id <= 0 || id == null)
            {
                return null;
            }
            else
            {
                var client = _clientRepo.GetClientById(id);
                var clientDTO = _mapper.Map<ClientDetailsDTO>(client);
               
                return clientDTO;
            }
        }
   
        public bool RemoveClient(int id)
        {
            if (id<=0 || id == null)
            {
                return false;
            }
            return _clientRepo.RemoveClient(id);
        }

        public ClientsForListDTO ShowAllClients(int? pageSize, int? pageNo, string? street, string? buildingNumber, string? city, string? surname)
        {
           
            if (!pageNo.HasValue || !pageSize.HasValue)
            {
                pageNo = 1;
                pageSize = 10;
            }

            List<ClientDTO> clients = new List<ClientDTO>();
            if (surname.IsNullOrEmpty() && street.IsNullOrEmpty() && buildingNumber.IsNullOrEmpty() && city.IsNullOrEmpty())
            {
                 clients = _clientRepo.ShowAllClients()
                .ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (street.IsNullOrEmpty() && buildingNumber.IsNullOrEmpty() && city.IsNullOrEmpty())
            {
                 clients = _clientRepo.GetClientsBySurname(surname)
                 .ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).ToList();
            }
            else if (surname.IsNullOrEmpty())
            {
                  clients = _clientRepo.GetClientByStreetName(street, buildingNumber, city)
                  .ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).ToList();
            }
            var clientsToShow = clients.Skip(pageSize.Value * (pageNo.Value - 1)).Take(pageSize.Value).ToList();
            var clientsDTO = new ClientsForListDTO()
            {
                PageNum = pageNo.Value,
                PageSize = pageSize.Value,
                SearchBuilding = buildingNumber,
                SearchCity = city,
                SearchStreet = street,
                SearchName = surname,
                Clients = clientsToShow,
                Count = clients.Count
            };
            return clientsDTO;
                 
        }

       
    }
}
