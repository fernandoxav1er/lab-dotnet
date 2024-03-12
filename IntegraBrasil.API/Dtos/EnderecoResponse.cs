using System.Text.Json.Serialization;

namespace IntegraBrasil.API.Dtos
{
    public class EnderecoResponse
    {
        public string? Cep { get; set; }
        public string? Estado { get; set; }
        public string? Cidade { get; set; }
        public string? Regiao { get; set; }
        public string? Rua { get; set; }
        
        [JsonIgnore]
        public string? Servico { get; set; }
    }
}
