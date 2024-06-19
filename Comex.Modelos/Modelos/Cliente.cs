namespace Comex.Modelos;

public class Cliente : IIdentificavel
{
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string Email { get; set; }
    public string Profissao { get; set; }
    public string Telefone { get; set; }
    public Endereco Endereco { get; set; }

    public string Identificar()
    {
        return $"Cliente: {Nome}, CPF: {CPF}";
    }
}
