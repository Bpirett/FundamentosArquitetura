using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SOLID_02._1_SRP.SRP.Violacao
{
    class Anuncio
    {
        public int AnuncioId { get; set; }
        public string titulo { get; set; }
        public string Email { get; set; }
        public int dormitorio { get; set; }
        public string rua { get; set; }
        public DateTime DataCadastro { get; set; }


        public string AdicionarAnuncio()
        {
            if (!Email.Contains("@"))
                return "Cliente com e-mail inválido";

            using (var cn = new SqlConnection())
            {
                var cmd = new SqlCommand();

                cn.ConnectionString = "MinhaConnectionString";
                cmd.Connection = cn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO Anuncio (TITULO, EMAIL, DORMITORiO, RUA,DATACADASTRO) VALUES (@TITULO, @EMAIL, @DORMITORiO, @RUA, @dataCad))";

                cmd.Parameters.AddWithValue("TITULO", titulo);
                cmd.Parameters.AddWithValue("EMAIL", Email);
                cmd.Parameters.AddWithValue("DORMITORiO", dormitorio);
                cmd.Parameters.AddWithValue("RUA", rua);
                cmd.Parameters.AddWithValue("dataCad", DataCadastro);

                cn.Open();
                cmd.ExecuteNonQuery();
            }

            var mail = new MailMessage("empresa@empresa.com", Email);
            var client = new SmtpClient
            {
                Port = 25,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Host = "smtp.google.com"
            };

            mail.Subject = "Bem Vindo.";
            mail.Body = "Parabéns! Você está cadastrado.";
            client.Send(mail);

            return "Cliente cadastrado com sucesso!";

        }

    }
}
