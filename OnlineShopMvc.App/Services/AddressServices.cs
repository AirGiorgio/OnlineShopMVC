using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.AdressDTOs;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.DTOs.OrderDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMvc.Inf.Repo;
using OnlineShopMVC.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopMvc.App.Services
{
    public class AddressServices : IAddressService
    {
        private readonly IAddressRepo _adressRepo;
        private readonly IMapper _mapper;

        public AddressServices(IAddressRepo adressRepo, IMapper mapper)
        {
            _adressRepo = adressRepo;
            _mapper = mapper;
        }

        public AddressDTO GetAddressByClientId(int id)
        {
            if (id <= 0 || id == null)
            {
                throw new ArgumentException("Nieprawidłowy identyfikator klienta");
            }
            else
            {
                var adres = _adressRepo.GetAddressByClientId(id);
                var adresDTO = _mapper.Map<AddressDTO>(adres);
                return adresDTO;
            }
        }

        public ClientsForListDTO GetClientByStreetDetails(string? street, string? buildingNumber, string? city)
        {
            var clients = _adressRepo.GetClientByStreetName(street, buildingNumber, city)
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
