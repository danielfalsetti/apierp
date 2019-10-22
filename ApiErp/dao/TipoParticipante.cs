using Dapper.Contrib.Extensions;
using System;

[Table("tb_tipo_participante")]
public class TipoParticipante
{
    public long id { get; set; }
    public string s_descricao { get; set; }
}