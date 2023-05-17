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
        public bool AddorUpdateClientAdress(Client? client, string? street, string? buildingNumber, string? flatNumber, string? city, string? zipCode)
        {
            if (street.IsNullOrEmpty() || buildingNumber.IsNullOrEmpty() ||flatNumber.IsNullOrEmpty()|| city.IsNullOrEmpty() || zipCode.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
            else if(client==null)
            {
                throw new ArgumentException("Brak klienta");
            }
            
            Address address = new Address();
            address.ClientId = client.Id;
            address.Street = street;
            address.BuildingNumber = buildingNumber;
            address.FlatNumber = flatNumber;
            address.City = city;
            address.ZipCode = zipCode;
               
            _clientRepo.AddAdress(address, client);

            return true;
        }

        public string AddClient(string? name, string? surname,  string? email, string? telephone)
        {
            if (name.IsNullOrEmpty() || surname.IsNullOrEmpty() || email.IsNullOrEmpty() || telephone.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
           
            Client client = new Client();
            client.Name = name;
            client.Surname = surname;
            client.EmailAdress = email;
            client.Telephone = telephone;

            _clientRepo.AddClient(client);

            return client.ToString();
        }

        public ClientDetailsDTO GetClientById(int id)
        {
            if (id <= 0 || id == null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator klienta");
            }
            else
            {
                var client = _clientRepo.GetClientById(id);
                ClientDetailsDTO clientDTO = new ClientDetailsDTO()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Surname = client.Surname,
                    Telephone = client.Telephone,
                    EmailAdress = client.EmailAdress,
                   
                };
                clientDTO.Adress = new AddressDTO()
                {
                    Id = client.Address.Id,
                    Street = client.Address.Street,
                    City = client.Address.City,
                    BuildingNumber = client.Address.BuildingNumber,
                    ZipCode = client.Address.ZipCode,
                    FlatNumber = client.Address.FlatNumber
                };
                return clientDTO;
            }
        }

        public ClientsForListDTO GetClientsBySurname(string? surname)
        {
            if (surname.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowe nazwisko klienta");
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
                throw new ArgumentException("Nieprawidłowy identyfikator klienta");
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

        public bool UpdateClient(Client? client, string? name, string? surname, string? email, string? telephone)
        {
            if (name.IsNullOrEmpty() || surname.IsNullOrEmpty() || email.IsNullOrEmpty() || telephone.IsNullOrEmpty())
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
            else if (client == null)
            {
                throw new ArgumentException("Nieprawidłowe dane klienta");
            }
            return _clientRepo.UpdateClient(client);
        }

    }
}
