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
        private readonly IAddressRepo _adressRepo;
        private readonly IClientRepo _clientRepo;
        private readonly IMapper _mapper;
        public ClientServices(IClientRepo clientRepo, IMapper mapper, IAddressRepo addressRepo)
        {
            _clientRepo = clientRepo;
            _mapper = mapper;
            _adressRepo = addressRepo;
        }
        public bool UpdateClientAndAddress(int id, string? name, string? surname, string? email, string? telephone, string? street, string? buildingNumber,
            string? flatNumber, string? city, string? zipCode)
        {
            if (street.IsNullOrEmpty() || buildingNumber.IsNullOrEmpty() ||flatNumber.IsNullOrEmpty()|| city.IsNullOrEmpty() || zipCode.IsNullOrEmpty())
            {
                return false;
            }
            else if(id<=0)
            {
                return false;
            }
            Client client = new Client();
            client.Id = id;
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
               
            _clientRepo.UpdateClientAndAddress(address, client, id);

            return true;
        }

        public string AddClientAndAddress(string? name, string? surname, string? email, string? telephone, string? street, string? buildingNumber,
            string? flatNumber, string? city, string? zipCode)
        {
            if (name.IsNullOrEmpty() || surname.IsNullOrEmpty() || email.IsNullOrEmpty() || telephone.IsNullOrEmpty())
            {
                return null;
            }
           
            Client client = new Client();
            client.Name = name;
            client.Surname = surname;
            client.EmailAdress = email;
            client.Telephone = telephone;
            Address adr = new Address();
            adr.Street = street;
            adr.BuildingNumber = buildingNumber;
            adr.FlatNumber = flatNumber;
            adr.City = city;
            adr.ZipCode = zipCode;

            _clientRepo.AddClientAndAddress(adr, client);

            return client.ToString();
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
                var adres = _adressRepo.GetAddressByClientId(id);
                var adresDTO = _mapper.Map<AddressDTO>(adres);
                clientDTO.Adress = adresDTO;
                return clientDTO;
            }
        }

        public ClientsForListDTO GetClientsBySurname(string? surname)
        {
            if (surname.IsNullOrEmpty())
            {
                return ShowAllClients();
            }
            else
            {
                var clients = _clientRepo.GetClientsBySurname(surname)
                    .ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).ToList(); 
             
                var clientsDTO = new ClientsForListDTO()
                {
                    Clients = clients,
                    Count = clients.Count
                };
                return clientsDTO;
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

        public ClientsForListDTO ShowAllClients()
        {
            var clients = _clientRepo.ShowAllClients()
             .ProjectTo<ClientDTO>(_mapper.ConfigurationProvider).ToList();

            var clientsDTO = new ClientsForListDTO()
            {
                Clients = clients,
                Count = clients.Count
            };
            return clientsDTO;
        }

    }
}
