using Dapper.Contrib.Extensions;
using System;

[Table("tb_grupo_participante")]
public class GrupoParticipante
{
    public long id { get; set; }
    public string s_descricao { get; set; }
}