namespace SOLID.DIP.Violacao
{
    public class ClienteService // essa classe não é obrigada a saber como instanciar a classe ClienteRepository
    {
        public string AdicionarCliente(Cliente cliente)
        {
            if (!cliente.Validar())
                return "Dados inválidos";

            // modulo de baixo nivel
            var repo = new ClienteRepository(); // isso é ima implementação 
            repo.AdicionarCliente(cliente);

            EmailServices.Enviar("empresa@empresa.com", cliente.Email.Endereco, "Bem Vindo", "Parabéns está Cadastrado");

            return "Cliente cadastrado com sucesso";
        }
    }
}