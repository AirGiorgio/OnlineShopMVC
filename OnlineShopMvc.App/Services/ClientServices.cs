using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.IdentityModel.Tokens;
using OnlineShopMvc.App.DTOs.ClientDTOs;
using OnlineShopMvc.App.Interfaces;
using OnlineShopMvc.Inf.Interfaces;
using OnlineShopMVC.Domain.Model;

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
        public string UpdateClientAndAddress(ClientDetailsDTO client)
        {
            if (client.Id <= 0 || client.Id == null)
            {
                return null;
            }
            if (client.Address.Street.IsNullOrEmpty() || client.Address.BuildingNumber.IsNullOrEmpty() || client.Address.FlatNumber.IsNullOrEmpty()|| client.Address.City.IsNullOrEmpty() || client.Address.ZipCode.IsNullOrEmpty())
            {
                return "Dane adresowe są niepoprawne";
            }
            else if (client.Name.IsNullOrEmpty() || client.Surname.IsNullOrEmpty() || client.EmailAdress.IsNullOrEmpty() || client.Telephone.IsNullOrEmpty())
            {
                return "Dane klienta są niepoprawne";
            }
            else return _clientRepo.UpdateClientAndAddress(_mapper.Map<Client>(client));       
        }
        public string AddClientAndAddress(ClientDetailsDTO client)
        {
            if (client.Address.Street.IsNullOrEmpty() || client.Address.BuildingNumber.IsNullOrEmpty() || client.Address.FlatNumber.IsNullOrEmpty() || client.Address.City.IsNullOrEmpty() || client.Address.ZipCode.IsNullOrEmpty())
            {
                return "Dane adresowe są niepoprawne";
            }
            else if (client.Name.IsNullOrEmpty() || client.Surname.IsNullOrEmpty() || client.EmailAdress.IsNullOrEmpty() || client.Telephone.IsNullOrEmpty())
            {
                return "Dane klienta są niepoprawne";
            }
            else return _clientRepo.AddClientAndAddress(_mapper.Map<Client>(client));
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
