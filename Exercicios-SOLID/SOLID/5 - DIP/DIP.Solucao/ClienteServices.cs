using SOLID.DIP.Solucao.Interfaces;

namespace SOLID.DIP.Solucao
{
    public class ClienteServices : IClienteServices
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEmailServices _emailServices; // referencia de uma interface

        public ClienteServices( // a classe ClienteServices não precisa criar o objeto  emailServices e  clienteRepository, invertemos o controle, quem consumir essa classe pode diz o objeto a ser injetado
            IEmailServices emailServices, 
            IClienteRepository clienteRepository)
        {
            _emailServices = emailServices;
            _clienteRepository = clienteRepository;
        }

        public string AdicionarCliente(Cliente cliente)
        {
            if (!cliente.Validar())
                return "Dados inválidos";

            _clienteRepository.AdicionarCliente(cliente); // sigo o contrato

            _emailServices.Enviar("empresa@empresa.com", cliente.Email.Endereco, "Bem Vindo", "Parabéns está Cadastrado");

            return "Cliente cadastrado com sucesso";
        }
    }

    public class TesteDip
    {
        public TesteDip()
        {
            var cliService = new ClienteServices(new EmailServices(), new ClienteRepository2());
        }
    }
}